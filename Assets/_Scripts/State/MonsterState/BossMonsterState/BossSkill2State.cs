using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill2State : MonsterStateBase
{
    public BossSkill2State(StateHandler<MonsterBase> handler) : base(handler)
    {
    }

    private float duration = 2f;
    private float timer = 0f;

    public override void Enter(MonsterBase entity)
    {
        timer = 0f;
        entity.Animator?.SetTrigger("Skill2");
        Debug.Log("[Boss] 스킬2 시작");
    }

    public override void Update(MonsterBase entity)
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            handler.ChangeState(typeof(BossMoveState));
        }
    }

    public override void Exit(MonsterBase entity)
    {
        Debug.Log("[Boss] 스킬2 종료");
    }
}
