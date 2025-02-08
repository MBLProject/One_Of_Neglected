using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttackState : BaseState<Player>
{
    private float attackDuration = 0.5f;  // 공격 모션 지속 시간
    private float attackTimer;

    public WarriorAttackState(StateHandler<Player> handler) : base(handler) { }

    public override void Enter(Player player)
    {
        Debug.Log("공격엔터");
        // 모든 애니메이션 상태 초기화
        player.Animator?.SetBool("IsMoving", false);
        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("Attack");
        player.Animator?.ResetTrigger("IsMoving");
        
        // 애니메이터 업데이트로 이전 상태 초기화
        player.Animator?.Update(0);
        
        // 공격 애니메이션 시작
        player.Animator?.SetTrigger("Attack");
        
        // 공격 상태 초기화
        attackTimer = attackDuration;
        player.SetSkillInProgress(true);

        // 마우스 방향으로 캐릭터 회전
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)player.transform.position).normalized;
        if (direction.x < 0)
        {
            player.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            player.transform.localScale = new Vector3(1, 1, 1);
        }
        
    }

    public override void Update(Player player)
    {
        Debug.Log("공격업뎃");
        attackTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            handler.ChangeState(typeof(WarriorDashState));
            return;
        }

        if (attackTimer <= 0)
        {
            if (!player.IsAtDestination())
            {
                handler.ChangeState(typeof(WarriorMoveState));
            }
            else
            {
                handler.ChangeState(typeof(WarriorIdleState));
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

        if (!player.IsAtDestination())
        {
            player.Animator?.SetBool("IsMoving", true);
            player.Animator?.Update(0);
        }
        else
        {
            player.Animator?.SetBool("IsMoving", false);
            player.Animator?.SetTrigger("Idle");
            player.Animator?.Update(0);
        }

        player.SetSkillInProgress(false);
    }
}
