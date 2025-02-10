using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : MonsterStateBase
{
    private float attackTimer = 0f;
    private float attackDelay = 0.5f;
    private bool hasAttacked = false;

    public RangedAttackState(StateHandler<MonsterBase> handler) : base(handler) { }

    public override void Enter(MonsterBase monster)
    {
        monster.Animator?.SetTrigger("RangedAttack");
        attackTimer = 0f;
        hasAttacked = false;
    }

    public override void Update(MonsterBase monster)
    {
        attackTimer += Time.deltaTime;

        if (!hasAttacked && attackTimer >= attackDelay && monster is RangedMonster rangedMonster)
        {
            rangedMonster.RangedAttack();
            hasAttacked = true;
        }

        if (attackTimer >= monster.Stats.attackCooldown)
        {
            handler.ChangeState(typeof(MonsterMoveState));
        }
    }

    public override void Exit(MonsterBase monster)
    {
        monster.Animator?.ResetTrigger("RangedAttack");
    }
}