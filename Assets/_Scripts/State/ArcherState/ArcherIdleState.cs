using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : BaseState<Player>
{
    public ArcherIdleState(StateHandler<Player> handler) : base(handler) { }

    public override void Enter(Player player)
    {
        player.Animator?.SetTrigger("Idle");
    }

    public override void Update(Player player)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            player.targetPosition = new Vector2(worldPosition.x, worldPosition.y);
            handler.ChangeState(typeof(ArcherMoveState));
            return;
        }
        else if (horizontalInput != 0 || verticalInput != 0)
        {
            handler.ChangeState(typeof(ArcherMoveState));
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.CanDash())
            {
                handler.ChangeState(typeof(ArcherDashState));
                return;
            }
        }

        if (player.isAuto)
        {
            MonsterBase nearestMonster = UnitManager.Instance.GetNearestMonster();
            if (nearestMonster != null)
            {
                float distance = Vector2.Distance(player.transform.position, nearestMonster.transform.position);

                if (distance <= 5f)
                {
                    player.LookAtTarget(nearestMonster.transform.position);
                    handler.ChangeState(typeof(ArcherAttackState));
                    return;
                }
                else
                {
                    player.targetPosition = nearestMonster.transform.position;
                    handler.ChangeState(typeof(ArcherMoveState));
                    return;
                }
            }
        }
        else
        {
            MonsterBase nearestMonster = UnitManager.Instance.GetNearestMonster();
            if (nearestMonster != null)
            {
                player.LookAtTarget(nearestMonster.transform.position);
                handler.ChangeState(typeof(ArcherAttackState));
                return;
            }
        }

        if (!player.IsAtDestination())
        {
            handler.ChangeState(typeof(ArcherMoveState));
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
