using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    protected StateHandler<MonsterBase> stateHandler;

    [Header("기본 컴포넌트")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    protected Transform playerTransform;

    protected MonsterStats stats;

    public MonsterStats Stats => stats;
    public Animator Animator => animator;

    protected virtual void Awake()
    {
        InitializeComponents();
        InitializeStats();
        InitializeStateHandler();
    }

    protected virtual void InitializeComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    protected abstract void InitializeStats();

    protected abstract void InitializeStateHandler();

    protected virtual void Update()
    {
        stateHandler?.Update();
        CheckStateTransitions();
    }

    public virtual void MoveTowardsPlayer()
    {
        if (playerTransform == null) return;

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * stats.moveSpeed;

        if (direction.x != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(direction.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    protected virtual void CheckStateTransitions()
    {
        if (stateHandler == null) return;

        // 플레이어가 공격 범위 내에 있는지만 체크
        if (IsPlayerInAttackRange() && !(stateHandler.CurrentState.GetType() == typeof(MonsterAttackState)))
        {
            stateHandler.ChangeState(typeof(MonsterAttackState));
            return;
        }

        // 공격 범위를 벗어나면 이동 상태로
        if (!IsPlayerInAttackRange() && stateHandler.CurrentState.GetType() == typeof(MonsterAttackState))
        {
            stateHandler.ChangeState(typeof(MonsterMoveState));
        }
    }

    protected virtual bool IsPlayerInAttackRange()
    {
        if (playerTransform == null) return false;
        return Vector2.Distance(transform.position, playerTransform.position) <= stats.attackRange;
    }
    public virtual void TakeDamage(float damage)
    {
        stats.currentHealth -= damage;
        if (stats.currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}