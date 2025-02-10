using UnityEngine;

public class WarriorMoveState : BaseState<Player>
{
    public WarriorMoveState(StateHandler<Player> handler) : base(handler) { }

    public override void Enter(Player player)
    {
        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("Dash");
        player.Animator?.ResetTrigger("IsMoving");
        
        player.Animator?.SetBool("IsMoving", true);
    }

    public override void Update(Player player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.CanDash())
            {
                handler.ChangeState(typeof(WarriorDashState));
                return;
            }
        }

        // 자동 전투 모드일 때 목적지에 도달하면 바로 공격
        if (player.isAuto && player.IsAtDestination())
        {
            handler.ChangeState(typeof(WarriorAttackState));
            return;
        }
        else if (player.IsAtDestination())
        {
            handler.ChangeState(typeof(WarriorIdleState));
            return;
        }

        player.MoveTo(player.targetPosition);
    }

    public override void Exit(Player player)
    {
        player.Animator?.SetBool("IsMoving", false);
        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("Dash");
        player.Animator?.ResetTrigger("IsMoving");
        
        player.Animator?.Update(0);
    }
} 