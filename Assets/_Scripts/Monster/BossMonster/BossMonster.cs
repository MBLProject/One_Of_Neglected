using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : BossMonsterBase
{
    private float skillCooldown = 3f;
    private float skillTimer = 0f;

    protected override void InitializeStateHandler()
    {
        stateHandler = new StateHandler<MonsterBase>(this);
        stateHandler.RegisterState(new BossMoveState(stateHandler));
        stateHandler.RegisterState(new BossBasicAttackState(stateHandler));
        stateHandler.RegisterState(new BossSkill1State(stateHandler));
        stateHandler.RegisterState(new BossSkill2State(stateHandler));
        stateHandler.RegisterState(new BossSkill3State(stateHandler));
        stateHandler.RegisterState(new MonsterDieState(stateHandler));

        stateHandler.ChangeState(typeof(BossMoveState));
        Debug.Log("[Boss] 상태 초기화 완료");
    }

    protected override void Update()
    {
        base.Update();

        if (!isInvulnerable)
        {
            skillTimer += Time.deltaTime;

            if (skillTimer >= skillCooldown &&
                stateHandler.CurrentState is BossMoveState)
            {
                UseRandomSkill();
                skillTimer = 0f;
            }
        }
    }

    private void UseRandomSkill()
    {
        float healthPercent = stats.currentHealth / stats.maxHealth;
        int randomSkill = UnityEngine.Random.Range(0, 3); // 0, 1, 2 중 랜덤

        switch (randomSkill)
        {
            case 0:
                stateHandler.ChangeState(typeof(BossSkill1State));
                break;
            case 1:
                stateHandler.ChangeState(typeof(BossSkill2State));
                break;
            case 2:
                stateHandler.ChangeState(typeof(BossSkill3State));
                break;
        }
    }

    protected override void InitializeStats()
    {
        // BossMonsterBase의 SetupBoss에서 처리
    }

}