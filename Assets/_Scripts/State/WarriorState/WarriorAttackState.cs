using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttackState : BaseState<Player>
{
    private const float BASE_ATTACK_DURATION = 0.5f; 
    private float attackTimer;
    private bool hasDealtDamage = false;  // 한 번의 공격에 한 번만 데미지를 주기 위한 플래그
    private float attackRange = 0.5f;     // 공격 범위

    public WarriorAttackState(StateHandler<Player> handler) : base(handler) { }

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
        
        MonsterBase nearestMonster = player.GetNearestMonster();
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
            MonsterBase nearestMonster = player.FindNearestMonsterInRange(5f);
            if (nearestMonster != null)
            {
                player.LookAtTarget(nearestMonster.transform.position);
            }
        }

        if (!hasDealtDamage && attackTimer >= currentAttackDuration * 0.5f)
        {
            DealDamageToMonsters(player);
            hasDealtDamage = true;
        }

        if (!player.IsAtDestination())
        {
            player.MoveTo(player.targetPosition);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.CanDash())
        {
            handler.ChangeState(typeof(WarriorDashState));
            return;
        }

        if (attackTimer >= currentAttackDuration)
        {
            attackTimer = 0;
            hasDealtDamage = false;
            
            if (player.isAuto)
            {
                MonsterBase nearestMonster = player.FindNearestMonsterInRange(5f);
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
                        handler.ChangeState(typeof(WarriorMoveState));
                        return;
                    }
                }
            }
            
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

    private void DealDamageToMonsters(Player player)
    {
        //TODO 공격 판정 다시 만들기
        Vector2 playerPosition = player.transform.position;
        float attackAngle = 90f;  
        
        int monsterLayer = LayerMask.NameToLayer("Monster");
        int layerMask = 1 << monsterLayer;

        Collider2D[] hits = Physics2D.OverlapCircleAll(playerPosition, attackRange, layerMask);
        
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Monster"))
            {
                Vector2 directionToMonster = (hit.transform.position - player.transform.position).normalized;
                float angle = Vector2.Angle(player.transform.right * (player.Animator.GetComponent<SpriteRenderer>().flipX ? -1 : 1), directionToMonster);

                if (angle <= attackAngle * 0.5f)
                {
                    MonsterBase monster = hit.GetComponent<MonsterBase>();
                    if (monster != null)
                    {
                        bool isCritical = UnityEngine.Random.value <= player.Stats.CurrentCritical;
                        float damage = player.Stats.CurrentATK;
                        if (isCritical)
                        {
                            damage *= (1f + player.Stats.CurrentCATK);
                        }
                        
                        monster.TakeDamage(damage);
                    }
                }
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
