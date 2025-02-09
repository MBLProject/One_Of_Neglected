using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : BaseState<Monster>
{
    private float attackTimer = 0f;
    private const float ATTACK_COOLDOWN = 0.5f;

    public MonsterAttackState(StateHandler<Monster> handler) : base(handler) { }

    public override void Enter(Monster entity)
    {
        attackTimer = ATTACK_COOLDOWN; // 즉시 첫 공격
    }

    public override void Update(Monster entity)
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= ATTACK_COOLDOWN)
        {
            PerformAttack(entity);
            attackTimer = 0f;
        }
    }

    private void PerformAttack(Monster entity)
    {
        // 여기에 플레이어 데미지 주는 로직 구현
        // 예: player.TakeDamage(entity.attackDamage);
        Debug.Log("Monster attacks player!");
    }
}
