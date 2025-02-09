using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveState : BaseState<Monster>
{
    public MonsterMoveState(StateHandler<Monster> handler) : base(handler) { }

    public override void Enter(Monster entity)
    {
        // 이동 상태 시작 시 필요한 초기화가 있다면 여기에 구현
    }

    public override void Update(Monster entity)
    {
        entity.MoveTowardsPlayer();
    }

    public override void Exit(Monster entity)
    {
        // 이동 상태 종료 시 필요한 정리가 있다면 여기에 구현
    }
}
