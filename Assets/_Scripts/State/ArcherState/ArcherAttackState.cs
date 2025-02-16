using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static Enums;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ArcherAttackState : BaseState<Player>
{
    private const float BASE_ATTACK_DURATION = 0.5f;
    private float attackTimer;
    private bool hasDealtDamage = false;  // 한 번의 공격에 한 번만 데미지를 주기 위한 플래그
    private float attackRange = 0.5f;     // 공격 범위

    [SerializeField] protected string projectileType = "NormalArcher";
    public ArcherAttackState(StateHandler<Player> handler) : base(handler) { }

    private float GetCurrentAttackDuration(Player player)
    {
        float speedMultiplier = 1f / (1f + (player.Stats.CurrentAspd / 100f));
        return BASE_ATTACK_DURATION * speedMultiplier;
    }
        
    public override void Enter(Player player)
    {
        attackTimer = 0f;
        hasDealtDamage = false;

        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("Attack");
        player.Animator?.ResetTrigger("IsMoving");
        player.Animator?.Update(0);

        float animSpeedMultiplier = 1f + (player.Stats.CurrentAspd / 100f);
        if (player.Animator != null)
        {
            player.Animator.speed = animSpeedMultiplier;
            player.Animator.SetTrigger("Attack");
        }

        MonsterBase nearestMonster = UnitManager.Instance.GetNearestMonster();
        if (nearestMonster != null)
        {
            player.LookAtTarget(nearestMonster.transform.position);
        }
    }

    public override void Update(Player player)
    {
        float currentAttackDuration = GetCurrentAttackDuration(player);
        attackTimer += Time.deltaTime;

        if (player.isAuto)
        {
            MonsterBase nearestMonster = UnitManager.Instance.GetNearestMonster();
            if (Vector2.Distance(player.transform.position, nearestMonster.transform.position) < 0.3f)
            if (nearestMonster != null)
            {
                player.LookAtTarget(nearestMonster.transform.position);
            }
        }

        if (!hasDealtDamage && attackTimer >= currentAttackDuration * 0.5f)
        {
            Vector2 direction = (player.transform.position - UnitManager.Instance.GetNearestMonster().transform.position).normalized;

            ProjectileManager.Instance.SpawnMonsterProjectile("NormalArcher",
                player.transform.position, direction, 1, player.Stats.CurrentATK);
            //ProjectileManager.Instance.SpawnProjectile(skillName:Enums.SkillName.Javelin, player.Stats.CurrentATK, 1);

            
            hasDealtDamage = true;
        }

        if (!player.IsAtDestination())
        {
            player.MoveTo(player.targetPosition);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.CanDash())
        {
            handler.ChangeState(typeof(ArcherDashState));
            return;
        }

        if (attackTimer >= currentAttackDuration)
        {
            attackTimer = 0;
            hasDealtDamage = false;

            if (player.isAuto)
            {
                MonsterBase nearestMonster = UnitManager.Instance.GetNearestMonster();
                if (nearestMonster != null)
                {
                    float distance = Vector2.Distance(player.transform.position, nearestMonster.transform.position);

                    if (distance <= 0.3f)
                    {
                        Enter(player);
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

            if (!player.IsAtDestination())
            {
                handler.ChangeState(typeof(ArcherMoveState));
            }
            else
            {
                handler.ChangeState(typeof(ArcherIdleState));
            }
        }
    }


    public override void Exit(Player player)
    {
        if (player.Animator != null)
        {
            player.Animator.speed = 1f;
        }

        player.Animator?.ResetTrigger("Attack");
        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("IsMoving");

        player.Animator?.Update(0);
    }

    public float AttackRange => attackRange;
}
