using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBasicAttackState : MonsterStateBase
{
    private float attackTimer = 0f;
    private float attackDuration = 1f;
    private bool hasDealtDamage = false;

    public BossBasicAttackState(StateHandler<MonsterBase> handler) : base(handler) { }

    public override void Enter(MonsterBase entity)
    {
        attackTimer = 0f;
        hasDealtDamage = false;

        // 애니메이션 설정
        entity.Animator?.SetBool("IsMoving", false);
        entity.Animator?.SetTrigger("Attack");
        Debug.Log("[Boss] 기본 공격 시작");
    }

    public override void Update(MonsterBase entity)
    {
        attackTimer += Time.deltaTime;

        if (!hasDealtDamage && attackTimer >= attackDuration * 0.5f)
        {
            PerformAttack(entity);
            hasDealtDamage = true;
            Debug.Log("[Boss] 공격 데미지 적용 완료");
        }

        // 공격 종료 조건
        if (attackTimer >= attackDuration)
        {
            handler.ChangeState(typeof(BossMoveState));
        }
    }

    public override void Exit(MonsterBase entity)
    {
        entity.Animator?.SetBool("IsMoving", true);
        entity.Animator?.ResetTrigger("Attack");
        Debug.Log("[Boss] 기본 공격 종료");
    }

    private void PerformAttack(MonsterBase entity)
    {
        BossMonsterBase boss = entity as BossMonsterBase;
        if (boss == null) return;

        if (!entity.IsPlayerInAttackRange()) return;

        var player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        if (player != null)
        {
            float damage = boss.Stats.attackDamage;
            player.TakeDamage(damage);
            Debug.Log($"[Boss] 기본 공격 데미지: {damage}");
        }
    }
}
