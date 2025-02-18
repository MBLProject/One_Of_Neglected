using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : MonsterStateBase
{
    public BossMoveState(StateHandler<MonsterBase> handler) : base(handler)
    {
    }

    public override void Enter(MonsterBase entity)
    {
        entity.Animator?.SetBool("IsMoving", true);
    }

    public override void Update(MonsterBase entity)
    {
        entity.MoveTowardsPlayer();
    }

    public override void Exit(MonsterBase entity)
    {
        entity.Animator?.SetBool("IsMoving", false);
    }
}
