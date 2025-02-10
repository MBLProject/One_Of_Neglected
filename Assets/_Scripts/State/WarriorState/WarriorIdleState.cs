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
        if (!player.IsAtDestination())
        {
            handler.ChangeState(typeof(WarriorMoveState));
            return;
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)player.transform.position).normalized;
        
        player.FlipModel(direction.x < 0);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (player.CanDash())
        //    {
        //        handler.ChangeState(typeof(WarriorDashState));
        //    }
        //    return;
        //}

        if (Input.GetMouseButtonDown(0))
        {
            handler.ChangeState(typeof(WarriorAttackState));
            return;
        }
    }

    public override void Exit(Player player)
    {
        player.Animator?.ResetTrigger("Idle");
    }
}
