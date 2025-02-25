using UnityEngine;

public class WarriorIdleState : BaseState<Player>
{
    public WarriorIdleState(StateHandler<Player> handler) : base(handler) { }

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
            handler.ChangeState(typeof(WarriorMoveState));
            return;
        }
        else if (horizontalInput != 0 || verticalInput != 0)
        {
            handler.ChangeState(typeof(WarriorMoveState));
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

        if (player.isAuto)
        {
            MonsterBase nearestMonster = UnitManager.Instance.GetNearestMonster();
            if (nearestMonster != null)
            {
                Vector2 playerPos = player.transform.position;
                Vector2 monsterPos = nearestMonster.transform.position;
                float distance = Vector2.Distance(playerPos, monsterPos);
                float optimalRange = player.Stats.CurrentATKRange * 0.8f;

                float horizontalDist = Mathf.Abs(playerPos.x - monsterPos.x);
                float verticalDist = Mathf.Abs(playerPos.y - monsterPos.y);

                if (distance <= optimalRange || (horizontalDist <= optimalRange && verticalDist <= optimalRange))
                {
                    player.LookAtTarget(monsterPos);
                    handler.ChangeState(typeof(WarriorAttackState));
                    return;
                }
                else
                {
                    Vector2 directionToMonster = (monsterPos - playerPos).normalized;
                    
                    if (Mathf.Abs(directionToMonster.y) > 0.9f)
                    {
                        directionToMonster.x += (playerPos.x < monsterPos.x) ? 0.3f : -0.3f;
                        directionToMonster = directionToMonster.normalized;
                    }

                    Vector2 optimalPosition = monsterPos - (directionToMonster * optimalRange * 0.8f);
                    player.targetPosition = optimalPosition;
                    handler.ChangeState(typeof(WarriorMoveState));
                    return;
                }
            }
        }
        else
        {
            MonsterBase nearestMonster = UnitManager.Instance.GetNearestMonster();
            if (nearestMonster != null)
            {
                Vector2 playerPos = player.transform.position;
                Vector2 monsterPos = nearestMonster.transform.position;
                float dist = Vector2.Distance(playerPos, monsterPos);
                float horizontalDist = Mathf.Abs(playerPos.x - monsterPos.x);
                float verticalDist = Mathf.Abs(playerPos.y - monsterPos.y);

                if (dist <= player.Stats.CurrentATKRange * 0.8f || 
                    (horizontalDist <= player.Stats.CurrentATKRange * 0.8f && verticalDist <= player.Stats.CurrentATKRange * 0.8f))
                {
                    player.LookAtTarget(monsterPos);
                    handler.ChangeState(typeof(WarriorAttackState));
                    return;
                }
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
}