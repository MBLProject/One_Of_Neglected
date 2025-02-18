using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill1State : MonsterStateBase
{
    private float duration = 2f;
    private float timer = 0f;

    public BossSkill1State(StateHandler<MonsterBase> handler) : base(handler)
    {
    }

    public override void Enter(MonsterBase entity)
    {
        timer = 0f;
        entity.Animator?.SetTrigger("Skill1");
        Debug.Log("[Boss] 스킬1 시작");
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
        Debug.Log("[Boss] 스킬1 종료");
    }
}
