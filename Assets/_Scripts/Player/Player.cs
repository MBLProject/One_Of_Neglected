using System;
using System.Collections;
using UnityEngine;
using static Enums;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Afterimage afterimage;


    protected StateHandler<Player> stateHandler;
    protected bool isSkillInProgress = false;
    protected bool isDashing = false;  

    protected PlayerStats stats;

    public Vector2 targetPosition;
    private Vector2 savedTargetPosition; 

    protected float moveThreshold = 0.01f; // 클릭 위치와 얼마나 가까이 가야 도착하는가


    public Animator Animator => animator;
    public PlayerStats Stats
    {
        get { return stats; }
        protected set { stats = value; }
    }

    public ClassType ClassType { get; protected set; }

    public Afterimage Afterimage => afterimage;

    protected virtual void Awake()
    {
        InitializeComponents();
        InitializeStateHandler();
        InitializeStats();
        InitializeClassType();
    }
    private void Update()
    {
        HandleSkillInput();
        
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

    protected abstract void InitializeStats();
    protected abstract void InitializeStateHandler();
    protected abstract void InitializeClassType();
    protected abstract void HandleSkillInput();

    private void InitializeComponents()
    {
        animator = GetComponent<Animator>();
        afterimage = GetComponent<Afterimage>();
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
        stats.currentHealth -= Mathf.RoundToInt(finalDamage);
    }

    public void ApplyAttackBuff(float amount, float duration)
    {
        StartCoroutine(ApplyAttackBuffCoroutine(amount, duration));
    }

    private IEnumerator ApplyAttackBuffCoroutine(float amount, float duration)
    {
        stats.attackPower += amount;

        yield return new WaitForSeconds(duration);

        stats.attackPower -= amount;
    }

    public void Heal(float amount)
    {
        stats.currentHealth = Mathf.Min(stats.currentHealth + amount, stats.maxHealth);
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
            Vector2 newPosition = Vector2.MoveTowards(transform.position, destination, stats.moveSpeed * Time.deltaTime);
            
            transform.SetPositionAndRotation(new Vector3(newPosition.x, newPosition.y, transform.position.z), transform.rotation);

            if (direction.x != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
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
}
