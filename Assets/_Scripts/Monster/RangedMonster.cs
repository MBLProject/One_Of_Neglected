using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedMonster : MonsterBase
{
    [Header("원거리 공격 설정")]
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float projectileSpeed = 5f;

    protected override void InitializeStateHandler()
    {
        stateHandler = new StateHandler<MonsterBase>(this);
        //stateHandler.RegisterState(new MonsterIdleState(stateHandler));
        stateHandler.RegisterState(new MonsterMoveState(stateHandler));
        stateHandler.RegisterState(new RangedAttackState(stateHandler));
        stateHandler.RegisterState(new MonsterDieState(stateHandler));
        stateHandler.ChangeState(typeof(MonsterMoveState));
    }

    /// <summary>
    /// 원거리 공격 실행
    /// </summary>
    public virtual void RangedAttack()
    {
        if (projectilePrefab != null && playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            if (projectileRb != null)
            {
                projectileRb.velocity = direction * projectileSpeed;
            }
        }
    }
}