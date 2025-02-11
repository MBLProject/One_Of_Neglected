using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterStateBase
{
    private float attackTimer = 0f;

    public MonsterAttackState(StateHandler<MonsterBase> handler) : base(handler) { }

    public override void Enter(MonsterBase entity)
    {
        attackTimer = 0f;
        // 이동 애니메이션 중지
        entity.Animator?.SetBool("IsMoving", false);
        // 공격 애니메이션 시작
        entity.Animator?.SetTrigger("Attack");
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

    public override void Exit(MonsterBase entity)
    {
        // 공격 애니메이션 리셋
        entity.Animator?.ResetTrigger("Attack");
    }

    private void PerformAttack(MonsterBase entity)
    {
        //entity.Animator?.SetTrigger("Attack");
        // 여기에 플레이어 데미지 주는 로직 구현
        // 예: player.TakeDamage(entity.attackDamage);
        Debug.Log("Monster attacks player!");
    }
}
