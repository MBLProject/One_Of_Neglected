using System;
using System.Collections.Generic;
using UnityEngine;
using static Enums;
using Random = UnityEngine.Random;

[Serializable]
public class StatViewer
{
    [Tooltip("레벨")] public int Level;
    [Tooltip("최대 경험치")] public int MaxExp;
    [Tooltip("경험치")] public float Exp;
    [Tooltip("최대체력")] public int MaxHp;
    [Tooltip("체력")] public float Hp;
    [Tooltip("체력회복량")] public float Recovery;
    [Tooltip("방어력")] public int Armor;
    [Tooltip("이동속도")] public float Mspd;
    [Tooltip("공격력")] public float ATK;
    [Tooltip("공격속도")] public float Aspd;
    [Tooltip("치명타 확률")] public float Critical;
    [Tooltip("치명타 데미지")] public float CATK;
    [Tooltip("투사체 개수")] public int Amount;
    [Tooltip("공격범위")] public float Area;
    [Tooltip("지속시간")] public float Duration;
    [Tooltip("쿨타임")] public float Cooldown;
    [Tooltip("부활 횟수")] public int Revival;
    [Tooltip("재화 습득범위")] public float Magnet;
    [Tooltip("성장")] public float Growth;
    [Tooltip("탐욕")] public float Greed;
    [Tooltip("저주")] public float Curse;
    [Tooltip("새로고침 횟수")] public int Reroll;
    [Tooltip("스탯 지우기")] public int Banish;
}

public abstract class Player : MonoBehaviour
{
    //오토 관련 필드 -> UnitList 쓰면 안쓸듯??
    public const float AUTO_DETECTION_RANGE = 5f;
    public const float ATTACK_RANGE = 0.3f;

    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dashEffect;
    [SerializeField] public StatViewer statViewer;
    [SerializeField] private SpriteRenderer modelRenderer;

    public bool isAuto = false; 

    protected StateHandler<Player> stateHandler;
    protected bool isSkillInProgress = false;
    protected bool isDashing = false;  

    protected PlayerStats stats;

    public Vector2 targetPosition;
    private Vector2 savedTargetPosition; 

    protected float moveThreshold = 0.1f;

    #region DashSettings
    protected int maxDashCount = 3;
    protected int currentDashCount;
    protected float dashRechargeTime = 5f;
    protected float dashRechargeTimer = 0f;

    public float DashRechargeTimer => dashRechargeTimer;
    public float DashRechargeTime => dashRechargeTime;
    public int CurrentDashCount => currentDashCount;
    public int MaxDashCount => maxDashCount;
    #endregion

    public ClassType ClassType { get; protected set; }
    public Animator Animator => animator;
    public PlayerStats Stats
    {
        get { return stats; }
        protected set { stats = value; }
    }

    public ParticleSystem DashEffect => dashEffect;

    protected virtual void Awake()
    {
        InitializeComponents();
        InitializeStateHandler();
        InitializeStats();
        InitializeClassType();
        InitializeStatViewer();
        
        currentDashCount = maxDashCount;

        if (dashEffect != null)
        {
            dashEffect.Stop();
        }
    }

    private void Update()
    {
        stateHandler.Update();
        
        UpdateDashRecharge();
        stats.UpadateHpRegen(Time.deltaTime);
        Moncheck();
    }

    protected abstract void InitializeStats();
    protected abstract void InitializeStateHandler();
    protected abstract void InitializeClassType();
    protected abstract void InitializeStatViewer();
    private void InitializeComponents()
    {
        if (modelRenderer == null)
        {
            modelRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    #region Dash
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
    public void SetDashing(bool dashing)
    {
        isDashing = dashing;
    }
    #endregion

    #region move
    public void MoveTo(Vector2 destination)
    {
        if (isDashing) return;
        
        if (Vector2.Distance(transform.position, destination) > moveThreshold)
        {
            Vector2 direction = (destination - (Vector2)transform.position).normalized;
            Vector2 newPosition = Vector2.MoveTowards(transform.position, destination, stats.CurrentMspd * Time.deltaTime);
            
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

    public void SetCurrentPositionAsTarget()
    {
        targetPosition = transform.position;
        savedTargetPosition = targetPosition;
    }

    #endregion

    #region 임시 전투로직
    public List<MonsterBase> monCheckList = new List<MonsterBase>();
    public void Moncheck()
    {
        monCheckList.Clear();

        int monsterLayer = LayerMask.NameToLayer("Monster");
        int layerMask = 1 << monsterLayer;

        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, .3f, layerMask);
        foreach (var coll in colls)
        {
            if (coll.CompareTag("Monster"))
            {
                MonsterBase monster = coll.GetComponent<MonsterBase>();
                if (monster != null)
                {
                    monCheckList.Add(monster);
                }
            }
        }
    }
    public MonsterBase GetNearestMonster()
    {
        if (monCheckList.Count == 0) return null;
        
        MonsterBase nearest = null;
        float minDistance = float.MaxValue;
        
        foreach (var monster in monCheckList)
        {
            float distance = Vector2.Distance(transform.position, monster.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = monster;
            }
        }
        
        return nearest;
    }
    public MonsterBase FindNearestMonsterInRange(float range)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Monster"));
        MonsterBase nearest = null;
        float minDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            MonsterBase monster = collider.GetComponent<MonsterBase>();
            if (monster != null)
            {
                float distance = Vector2.Distance(transform.position, monster.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = monster;
                }
            }
        }

        return nearest;
    }
    #endregion

    #region Utils
    public void SetSkillInProgress(bool inProgress, bool savePrevPosition = true)
    {
        isSkillInProgress = inProgress;
        if (inProgress && savePrevPosition)
        {
            savedTargetPosition = targetPosition;
        }
    }
    public void LookAtTarget(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        FlipModel(direction.x < 0);
    }
    public void FlipModel(bool isLeft)
    {
        if (modelRenderer != null)
        {
            modelRenderer.flipX = isLeft;
        }

        if (dashEffect != null)
        {
            Vector3 localScale = dashEffect.transform.localScale;
            localScale.x = isLeft ? -Mathf.Abs(localScale.x) : Mathf.Abs(localScale.x);
            dashEffect.transform.localScale = localScale;
        }
    }
    public virtual void TakeDamage(float damage)
    {
        bool isCritical = false;
        //몬스터도 크리확률이 있나?? 몰루??
        if (Random.Range(0f, 100f) <= stats.CurrentCritical)
        {
            damage = Mathf.RoundToInt(damage * stats.CurrentCATK);
            isCritical = true;
        }

        stats.currentHp -= damage;
        ShowDamageFont(transform.position, damage, transform, isCritical);
        if (stats.currentHp <= 0)
        {
            //ToDO 플레이어 사망처리
            Debug.Log("플레이어 주금");
        }
    }
    public void ShowDamageFont(Vector2 pos, float damage, Transform parent, bool isCritical = false)
    {
        GameObject go = Resources.Load<GameObject>("DamageText");
        if (go != null)
        {
            Vector2 spawnPosition = (Vector2)transform.position + Vector2.up * 0.2f;
            
            GameObject instance = Instantiate(go, spawnPosition, Quaternion.identity);
            ShowDamage damageText = instance.GetComponent<ShowDamage>();
            if (damageText != null)
            {
                damageText.SetInfo(spawnPosition, damage, parent, isCritical);
            }
        }
    }
    #endregion

    #region stat
    /// <summary>
    /// 기존 스탯에 value만큼 더함
    /// </summary>
    /// <param name="statType">스탯 타입</param>
    /// <param name="value">더해지는 값</param>
    public void ModifyStat(StatType statType, float value)
    {
        stats.ModifyStatValue(statType, value);
        UpdateStatViewer();
    }
    /// <summary>
    /// 기존 스탯을 value값으로 바꿈
    /// </summary>
    /// <param name="statType">스탯 타입</param>
    /// <param name="value">바꾸는 값</param>
    public void SetStat(StatType statType, float value)
    {
        stats.SetStatValue(statType, value);
        UpdateStatViewer();
    }
    private void UpdateStatViewer()
    {
        statViewer.Level = stats.CurrentLevel;
        statViewer.MaxHp = stats.CurrentMaxHp;
        statViewer.Hp = stats.currentHp;
        statViewer.MaxExp = stats.CurrentMaxExp;
        statViewer.Exp = stats.currentExp;
        statViewer.MaxHp = stats.CurrentMaxHp;
        statViewer.Recovery = stats.CurrentRecovery;
        statViewer.Armor = stats.CurrentArmor;
        statViewer.Mspd = stats.CurrentMspd;
        statViewer.ATK = stats.CurrentATK;
        statViewer.Aspd = stats.CurrentAspd;
        statViewer.Critical = stats.CurrentCritical;
        statViewer.CATK = stats.CurrentCATK;
        statViewer.Amount = stats.CurrentAmount;
        statViewer.Area = stats.CurrentArea;
        statViewer.Cooldown = stats.CurrentCooldown;
        statViewer.Revival = stats.CurrentRevival;
        statViewer.Magnet = stats.CurrentMagnet;
        statViewer.Growth = stats.CurrentGrowth;
        statViewer.Greed = stats.CurrentGreed;
        statViewer.Curse = stats.CurrentCurse;
        statViewer.Reroll = stats.CurrentReroll;
        statViewer.Banish = stats.CurrentBanish;
    }

    protected void UpdateStats()
    {
        if (stats != null)
        {
            stats.CurrentMaxExp = statViewer.MaxExp;
            stats.currentExp = statViewer.Exp;
            stats.CurrentMaxHp = statViewer.MaxHp;
            stats.currentHp = statViewer.Hp;
            stats.CurrentRecovery = statViewer.Recovery;
            stats.CurrentArmor = statViewer.Armor;
            stats.CurrentMspd = statViewer.Mspd;
            stats.CurrentATK = statViewer.ATK;
            stats.CurrentAspd = statViewer.Aspd;
            stats.CurrentCritical = statViewer.Critical;
            stats.CurrentCATK = statViewer.CATK;
            stats.CurrentAmount = statViewer.Amount;
            stats.CurrentArea = statViewer.Area;
            stats.CurrentCooldown = statViewer.Cooldown;
            stats.CurrentRevival = statViewer.Revival;
            stats.CurrentMagnet = statViewer.Magnet;
            stats.CurrentGrowth = statViewer.Growth;
            stats.CurrentGreed = statViewer.Greed;
            stats.CurrentCurse = statViewer.Curse;
            stats.CurrentReroll = statViewer.Reroll;
            stats.CurrentBanish = statViewer.Banish;
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exp"))
        {
            ExpObject expObject = collision.gameObject.GetComponent<ExpObject>();
            if(expObject.expType == ExpType.White)
            {
                stats.currentExp += 10;
            }
            else if (expObject.expType == ExpType.Green)
            {
                stats.currentExp += 20;
            }
            else if (expObject.expType == ExpType.Blue)
            {
                stats.currentExp += 30;
            }
            else if (expObject.expType == ExpType.Red)
            {
                stats.currentExp += 40;
            }
            else if (expObject.expType == ExpType.Purple)
            {
                stats.currentExp += 50;
            }
            expObject.selfDestroy();
        }
    }
    private void OnValidate()
    {
        if (stats != null)
        {
            UpdateStats();
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .3f);
        
        if (stateHandler?.CurrentState is WarriorAttackState attackState)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackState.AttackRange);
        }
    }
}
