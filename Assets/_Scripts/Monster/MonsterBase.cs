 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public abstract class MonsterBase : MonoBehaviour
{
    protected StateHandler<MonsterBase> stateHandler;

    [Header("기본 컴포넌트")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    [SerializeField] private float dieAnimationLength = 1f;
    private bool isDying = false;
    protected Transform playerTransform;

    protected MonsterStats stats;

    public event Action<MonsterBase> OnDeath;

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

        // 이동 방향 계산
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Transform을 사용한 이동
        transform.position += (Vector3)(direction * stats.moveSpeed * Time.deltaTime);

        // 스프라이트 방향 설정
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

        bool playerInRange = IsPlayerInAttackRange();

        // 현재 상태가 공격 상태일 때
        if (stateHandler.CurrentState is MonsterAttackState || stateHandler.CurrentState is RangedAttackState)
        {
            // 플레이어가 범위를 벗어나면 즉시 이동 상태로 전환
            if (!playerInRange)
            {
                stateHandler.ChangeState(typeof(MonsterMoveState));
            }
        }
        // 현재 상태가 이동 상태일 때
        else if (stateHandler.CurrentState is MonsterMoveState)
        {
            // 플레이어가 범위 안에 있으면 공격 상태로 전환
            if (playerInRange)
            {
                Type attackStateType = this is RangedMonster
                    ? typeof(RangedAttackState)
                    : typeof(MonsterAttackState);
                stateHandler.ChangeState(attackStateType);
            }
        }
    }

    public virtual bool IsPlayerInAttackRange()
    {
        if (playerTransform == null) return false;
        return Vector2.Distance(transform.position, playerTransform.position) <= stats.attackRange;
    }
    public virtual void TakeDamage(float damage)
    {
        stats.currentHealth -= damage;
        ShowDamageFont(transform.position, damage, transform);
        animator?.SetTrigger("Hit");
        if (stats.currentHealth <= 0)
        {
            Die();
        }
    }
    public void ShowDamageFont(Vector2 pos, float damage, Transform parent)
    {
        GameObject go = Resources.Load<GameObject>("DamageText");
        if (go != null)
        {
            Vector2 spawnPosition = (Vector2)transform.position + Vector2.up * 0.2f;

            GameObject instance = Instantiate(go, spawnPosition, Quaternion.identity);
            ShowDamage damageText = instance.GetComponent<ShowDamage>();
            if (damageText != null)
            {
                damageText.SetInfo(spawnPosition, damage, parent);
            }
        }
    }

    protected virtual async void Die()
    {
        if (isDying) return;
        isDying = true;

        stateHandler.ChangeState(typeof(MonsterDieState));
        try
        {
            await UniTask.Delay((int)(dieAnimationLength * 1000));

            OnMonsterDestroy();

            //UnitManager.Instance.RemoveMonster(this);
        }
        finally
        {
            // 항상 오브젝트 제거 실행
            if (this != null && gameObject != null)
            {
                OnDeath?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnMonsterDestroy()
    {
        Debug.Log("경험치 드롭");
    }
}  