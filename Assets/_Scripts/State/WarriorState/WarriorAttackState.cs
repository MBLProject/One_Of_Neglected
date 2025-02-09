using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttackState : BaseState<Player>
{
    private float attackDuration = 0.5f; 
    private float attackTimer;


    public WarriorAttackState(StateHandler<Player> handler) : base(handler) { }

    public override void Enter(Player player)
    {
        Debug.Log("공격엔터");
        player.Animator?.SetBool("IsMoving", false);
        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("Attack");
        player.Animator?.ResetTrigger("IsMoving");
        player.Animator?.Update(0);

        attackTimer = attackDuration;
        player.SetSkillInProgress(true);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)player.transform.position).normalized;
        
        player.FlipModel(direction.x < 0);
        player.Animator?.SetTrigger("Attack");
    }

    public override void Update(Player player)
    {
        Debug.Log("공격업뎃");
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            handler.ChangeState(typeof(WarriorIdleState));
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.CanDash())
            {
                handler.ChangeState(typeof(WarriorDashState));
                return;
            }
        }
    }

    public override void Exit(Player player)
    {
        Debug.Log("공격엑싯");
        player.Animator?.ResetTrigger("Attack");
        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("IsMoving");
        
        player.Animator?.Update(0);

        player.SetCurrentPositionAsTarget();
        player.Animator?.SetBool("IsMoving", false);
        player.Animator?.SetTrigger("Idle");
        player.Animator?.Update(0);

        player.SetSkillInProgress(false);
    }
}
