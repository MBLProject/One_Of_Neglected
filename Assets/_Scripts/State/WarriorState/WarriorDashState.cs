using UnityEngine;

public class WarriorDashState : BaseState<Player>
{
    private float dashSpeed = 3f;
    private Vector2 dashDirection;
    private float dashDistance = 1f;

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float lerpProgress;

    public WarriorDashState(StateHandler<Player> handler) : base(handler) { }

    public override void Enter(Player player)
    {
        if (!player.CanDash())
        {
            handler.ChangeState(typeof(WarriorIdleState));
            return;
        }

        player.ConsumeDash();
        player.SetDashing(true);  
        player.SetSkillInProgress(true, false);

        player.Animator?.SetBool("IsMoving", false);
        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("Dash");
        player.Animator?.ResetTrigger("IsMoving");
        
        player.Animator?.Update(0);
        player.Animator?.SetTrigger("Dash");
        
        startPosition = player.transform.position;
        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dashDirection = (mousePosition - (Vector2)player.transform.position).normalized;
        
        player.FlipModel(dashDirection.x < 0);
        
        if (player.DashEffect != null)
        {
            player.DashEffect.Play();
        }
        
        targetPosition = startPosition + (dashDirection * dashDistance);
        lerpProgress = 0f;
    }

    public override void Update(Player player)
    {
        lerpProgress += Time.deltaTime * dashSpeed;
        player.transform.position = Vector2.Lerp(startPosition, targetPosition, lerpProgress);

        if (lerpProgress >= 1f)
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
        player.SetDashing(false);
        player.SetSkillInProgress(false, false);
        
        player.SetCurrentPositionAsTarget();
        
        player.Animator?.ResetTrigger("Dash");
        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("IsMoving");
        
        player.Animator?.SetBool("IsMoving", false);
        player.Animator?.SetTrigger("Idle");

        if (player.DashEffect != null)
        {
            player.DashEffect.Stop();
        }
    }
}
