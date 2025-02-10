using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorIdleState : BaseState<Player>
{
    public WarriorIdleState(StateHandler<Player> handler) : base(handler) { }

    public override void Enter(Player player)
    {
        player.Animator?.SetBool("IsMoving", false);
        player.Animator?.SetTrigger("Idle");
    }

    public override void Update(Player player)
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.CanDash())
        {
            handler.ChangeState(typeof(WarriorDashState));
            return;
        }

        // 자동 전투 모드일 때
        if (player.isAuto)
        {
            MonsterBase nearestMonster = player.FindNearestMonsterInRange(5f);
            if (nearestMonster != null)
            {
                float distance = Vector2.Distance(player.transform.position, nearestMonster.transform.position);
                
                // 공격 범위 내에 있으면 공격
                if (distance <= 0.3f)
                {
                    player.LookAtTarget(nearestMonster.transform.position);
                    handler.ChangeState(typeof(WarriorAttackState));
                }
                // 범위 밖이면 이동
                else
                {
                    player.targetPosition = nearestMonster.transform.position;
                    handler.ChangeState(typeof(WarriorMoveState));
                }
                return;
            }
        }
        else
        {
            // 기존의 일반 전투 로직
            MonsterBase nearestMonster = player.GetNearestMonster();
            if (nearestMonster != null)
            {
                player.LookAtTarget(nearestMonster.transform.position);
                handler.ChangeState(typeof(WarriorAttackState));
                return;
            }
        }

        if (!player.IsAtDestination())
        {
            handler.ChangeState(typeof(WarriorMoveState));
        }
    }

    public override void Exit(Player player)
    {
        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("Attack");
        player.Animator?.ResetTrigger("Dash");
        player.Animator?.ResetTrigger("IsMoving");
        player.Animator?.Update(0);
    }

    /// <summary>
    /// 마우스 위치에 따라 플레이어 모델 방향 플립
    /// </summary>
    /// <param name="player"></param>
    public void SetModelFlip(Player player)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)player.transform.position).normalized;
        player.FlipModel(direction.x < 0);
    }
}
