using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterStateBase
{
    private float attackTimer = 0f;

    public MonsterAttackState(StateHandler<MonsterBase> handler) : base(handler) { }

    public override void Enter(MonsterBase entity)
    {
    }

    public override void Update(MonsterBase entity)
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= entity.Stats.attackCooldown)
        {
            PerformAttack(entity);
            attackTimer = 0f;
        }
    }

    private void PerformAttack(MonsterBase entity)
    {
        entity.Animator?.SetTrigger("Attack");
        // 여기에 플레이어 데미지 주는 로직 구현
        // 예: player.TakeDamage(entity.attackDamage);
        Debug.Log("Monster attacks player!");
    }
}
