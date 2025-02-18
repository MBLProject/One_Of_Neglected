using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBasicAttackState : MonsterStateBase
{
    private float attackDuration = 1f;
    private float timer = 0f;

    public BossBasicAttackState(StateHandler<MonsterBase> handler) : base(handler)
    {
    }

    public override void Enter(MonsterBase entity)
    {
        timer = 0f;
        entity.Animator?.SetTrigger("Attack");
    }

    public override void Update(MonsterBase entity)
    {
        timer += Time.deltaTime;
        if (timer >= attackDuration)
        {
            handler.ChangeState(typeof(BossMoveState));
        }
    }
}
