using System;
using System.Collections;
using UnityEngine;
using static Enums;

[Serializable]
public class StatViewer
{
    [Tooltip("레벨")] public int Level;
    [Tooltip("최대 경험치")] public int MaxExp;
    [Tooltip("경험치")] public int Exp;
    [Tooltip("최대체력")] public int MaxHp;
    [Tooltip("체력")] public int Hp;
    [Tooltip("체력회복량")] public int Recovery;
    [Tooltip("방어력")] public int Armor;
    [Tooltip("이동속도")] public int Mspd;
    [Tooltip("공격력")] public int ATK;
    [Tooltip("공격속도")] public int Aspd;
    [Tooltip("치명타 확률")] public int Critical;
    [Tooltip("치명타 데미지")] public int CATK;
    [Tooltip("투사체 개수")] public int Amount;
    [Tooltip("공격범위")] public int Area;
    [Tooltip("지속시간")] public int Duration;
    [Tooltip("쿨타임")] public int Cooldown;
    [Tooltip("부활 횟수")] public int Revival;
    [Tooltip("재화 습득범위")] public int Magnet;
    [Tooltip("성장")] public int Growth;
    [Tooltip("탐욕")] public int Greed;
    [Tooltip("저주")] public int Curse;
    [Tooltip("새로고침 횟수")] public int Reroll;
    [Tooltip("스탯 지우기")] public int Banish;
}

public abstract class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Afterimage afterimage;
    [SerializeField] public StatViewer statViewer;
    [SerializeField] private SpriteRenderer modelRenderer;  

    protected StateHandler<Player> stateHandler;
    protected bool isSkillInProgress = false;
    protected bool isDashing = false;  

    protected PlayerStats stats;

    public Vector2 targetPosition;
    private Vector2 savedTargetPosition; 

    protected float moveThreshold = 0.1f;

    [Header("Dash Settings")]
    protected int maxDashCount = 3;
    protected int currentDashCount;
    protected float dashRechargeTime = 5f;
    protected float dashRechargeTimer = 0f;

    public float DashRechargeTimer => dashRechargeTimer;
    public float DashRechargeTime => dashRechargeTime;

    public Animator Animator => animator;
    public PlayerStats Stats
    {
        get { return stats; }
        protected set { stats = value; }
    }

    public ClassType ClassType { get; protected set; }

    public Afterimage Afterimage => afterimage;
    public int CurrentDashCount => currentDashCount;
    public int MaxDashCount => maxDashCount;


    protected virtual void Awake()
    {
        InitializeComponents();
        InitializeStateHandler();
        InitializeStats();
        InitializeClassType();
        InitializeStatViewer();
        
        currentDashCount = maxDashCount;
    }

    private void Update()
    {
        HandleSkillInput();
        UpdateStats();
        UpdateDashRecharge();
        
        if (!isSkillInProgress && Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            targetPosition = new Vector2(worldPosition.x, worldPosition.y);
            savedTargetPosition = targetPosition;
            
            if (stateHandler.IsInState<WarriorIdleState>())
            {
                stateHandler.ChangeState(typeof(WarriorMoveState));
            }
        }
        stateHandler.Update();
    }

    private void UpdateDashRecharge()
    {
        if (currentDashCount < maxDashCount)
        {
            dashRechargeTimer += Time.deltaTime;
            if (dashRechargeTimer >= dashRechargeTime)
            {
                dashRechargeTimer = 0f;
                currentDashCount++;
            }
        }
    }

    public bool CanDash()
    {
        return currentDashCount > 0;
    }

    public void ConsumeDash()
    {
        if (currentDashCount > 0)
        {
            currentDashCount--;
        }
    }

    protected abstract void InitializeStats();
    protected abstract void InitializeStateHandler();
    protected abstract void InitializeClassType();
    protected abstract void HandleSkillInput();
    protected abstract void InitializeStatViewer();

    private void InitializeComponents()
    {
        afterimage = GetComponent<Afterimage>();
        if (modelRenderer == null)
        {
            modelRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    public void SetSkillInProgress(bool inProgress)
    {
        isSkillInProgress = inProgress;
        if (inProgress)
        {
            savedTargetPosition = targetPosition;
        }
    }

    public void SetDashing(bool dashing)
    {
        isDashing = dashing;
        if (dashing)
        {
            isSkillInProgress = true;
        }
    }

    public virtual void Takedamage(float damage)
    {
        float finalDamage = stats.CalculateDamage(damage);
        stats.currentHp -= Mathf.RoundToInt(finalDamage);
    }

    public void ApplyAttackBuff(float amount, float duration)
    {
        StartCoroutine(ApplyAttackBuffCoroutine(amount, duration));
    }

    private IEnumerator ApplyAttackBuffCoroutine(float amount, float duration)
    {
        //stats.attackPower += amount;

        yield return new WaitForSeconds(duration);

        //stats.attackPower -= amount;
    }

    public void Heal(float amount)
    {
        stats.currentHp = Mathf.Min(stats.currentHp + amount, stats.MaxHp);
    }

    public void SyncStateChange(string stateName)
    {
        Type stateType = Type.GetType(stateName);
        if (stateType != null)
        {
            stateHandler.ChangeState(stateType);
        }
    }

    public void MoveTo(Vector2 destination)
    {
        if (isSkillInProgress) return;
        
        if (Vector2.Distance(transform.position, destination) > moveThreshold)
        {
            Vector2 direction = (destination - (Vector2)transform.position).normalized;
            Vector2 newPosition = Vector2.MoveTowards(transform.position, destination, stats.Mspd * Time.deltaTime);
            
            transform.SetPositionAndRotation(new Vector3(newPosition.x, newPosition.y, transform.position.z), transform.rotation);

            if (direction.x != 0)
            {
                FlipModel(direction.x < 0);
            }
        }
    }

    public bool IsAtDestination()
    {
        return Vector2.Distance(transform.position, targetPosition) < moveThreshold;
    }

    public void RestoreTargetPosition()
    {
        targetPosition = savedTargetPosition; 
    }

    public void SetCurrentPositionAsTarget()
    {
        targetPosition = transform.position;
        savedTargetPosition = targetPosition;
    }

    protected void UpdateStats()
    {
        if (stats != null)
        {
            stats.Level = statViewer.Level;
            stats.MaxExp = statViewer.MaxExp;
            stats.Exp = statViewer.Exp;
            stats.MaxHp = statViewer.MaxHp;
            stats.Hp = statViewer.Hp;
            stats.Recovery = statViewer.Recovery;
            stats.Armor = statViewer.Armor;
            stats.Mspd = statViewer.Mspd;
            stats.ATK = statViewer.ATK;
            stats.Aspd = statViewer.Aspd;
            stats.Critical = statViewer.Critical;
            stats.CATK = statViewer.CATK;
            stats.Amount = statViewer.Amount;
            stats.Area = statViewer.Area;
            stats.Cooldown = statViewer.Cooldown;
            stats.Revival = statViewer.Revival;
            stats.Magnet = statViewer.Magnet;
            stats.Growth = statViewer.Growth;
            stats.Greed = statViewer.Greed;
            stats.Curse = statViewer.Curse;
            stats.Reroll = statViewer.Reroll;
            stats.Banish = statViewer.Banish;
        }
    }
    private void OnValidate()
    {
        if (stats != null)
        {
            UpdateStats();
        }
    }

    public void FlipModel(bool isLeft)
    {
        if (modelRenderer != null)
        {
            modelRenderer.flipX = isLeft;  
        }
    }
}
