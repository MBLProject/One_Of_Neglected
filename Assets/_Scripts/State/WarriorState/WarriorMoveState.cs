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
        if (player.IsAtDestination())
        {
            handler.ChangeState(typeof(WarriorIdleState));
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            handler.ChangeState(typeof(WarriorDashState));
            return;
        }

        if (Input.GetMouseButton(0))
        {
            handler.ChangeState(typeof(WarriorAttackState));
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