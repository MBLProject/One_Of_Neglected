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
            if(Vector2.Distance(player.transform.position, nearestMonster.transform.position) < 0.3f)
            if (nearestMonster != null)
            {
                player.LookAtTarget(nearestMonster.transform.position);
            }
        }

        if (!hasDealtDamage && attackTimer >= currentAttackDuration * 0.5f)
        {
            bool isLookingRight = !player.Animator.GetComponent<SpriteRenderer>().flipX;
            Vector2 direction = isLookingRight ? Vector2.right : Vector2.left;
            Vector3 spawnPosition = player.transform.position + (Vector3)(direction * 0.2f);
            
            // 공격 방향을 기준으로 타겟 위치 설정 (좌우 방향만 사용)
            Vector3 targetPosition = spawnPosition + (Vector3)(direction * 1f); // 방향에 따라 약간 앞쪽을 타겟으로 설정
            
            ProjectileManager.Instance.SpawnPlayerProjectile(
                "WarriorAttackProjectile",
                spawnPosition,
                targetPosition,
                0f,
                player.Stats.CurrentATK
            );
            
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
                        bool isCritical = UnityEngine.Random.value <= player.Stats.CurrentCriRate;
                        float damage = player.Stats.CurrentATK;
                        if (isCritical)
                        {
                            damage *= (1f + player.Stats.CurrentCriDamage);
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
