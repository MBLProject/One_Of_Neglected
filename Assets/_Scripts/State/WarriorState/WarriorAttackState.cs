using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttackState : BaseState<Player>
{
    private float attackDuration = 0.5f; 
    private float attackTimer;
    private bool hasDealtDamage = false;  // 한 번의 공격에 한 번만 데미지를 주기 위한 플래그
    private float attackRange = 0.5f;     // 공격 범위

    public WarriorAttackState(StateHandler<Player> handler) : base(handler) { }

    public override void Enter(Player player)
    {
        attackTimer = 0f;
        hasDealtDamage = false;  // 공격 시작시 초기화
        
        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("Attack");
        player.Animator?.ResetTrigger("IsMoving");
        player.Animator?.Update(0);

        player.Animator?.SetTrigger("Attack");
        
        MonsterBase nearestMonster = player.GetNearestMonster();
        if (nearestMonster != null)
        {
            player.LookAtTarget(nearestMonster.transform.position);
        }
    }

    public override void Update(Player player)
    {
        attackTimer += Time.deltaTime;

        // 공격 판정 타이밍
        if (!hasDealtDamage && attackTimer >= attackDuration * 0.5f)
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

        // 공격 종료 시
        if (attackTimer >= attackDuration)
        {
            attackTimer = 0;
            hasDealtDamage = false;  // 다음 공격을 위해 초기화
            
            // 자동 전투 모드일 때
            if (player.isAuto)
            {
                MonsterBase nearestMonster = player.FindNearestMonsterInRange(5f);
                if (nearestMonster != null)
                {
                    float distance = Vector2.Distance(player.transform.position, nearestMonster.transform.position);
                    
                    // 공격 범위 내에 있으면 다시 공격
                    if (distance <= 0.3f)
                    {
                        player.LookAtTarget(nearestMonster.transform.position);
                        handler.ChangeState(typeof(WarriorIdleState));  // 잠시 Idle로 전환
                        return;
                    }
                    // 범위 밖이면 이동
                    else
                    {
                        player.targetPosition = nearestMonster.transform.position;
                        handler.ChangeState(typeof(WarriorMoveState));
                        return;
                    }
                }
                else
                {
                    handler.ChangeState(typeof(WarriorIdleState));
                    return;
                }
            }
            
            // 자동 전투가 아니거나 타겟이 없으면
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
        // 플레이어 앞쪽에 부채꼴 모양의 공격 범위 체크
        Vector2 playerPosition = player.transform.position;
        float attackAngle = 90f;  // 공격 각도 (예: 90도)
        
        int monsterLayer = LayerMask.NameToLayer("Monster");
        int layerMask = 1 << monsterLayer;

        Collider2D[] hits = Physics2D.OverlapCircleAll(playerPosition, attackRange, layerMask);
        
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Monster"))
            {
                Vector2 directionToMonster = (hit.transform.position - player.transform.position).normalized;
                float angle = Vector2.Angle(player.transform.right * (player.Animator.GetComponent<SpriteRenderer>().flipX ? -1 : 1), directionToMonster);

                // 부채꼴 범위 안에 있는 몬스터만 데미지
                if (angle <= attackAngle * 0.5f)
                {
                    MonsterBase monster = hit.GetComponent<MonsterBase>();
                    if (monster != null)
                    {
                        // 크리티컬 계산
                        bool isCritical = UnityEngine.Random.value <= player.Stats.Critical;
                        float damage = player.Stats.ATK;
                        if (isCritical)
                        {
                            damage *= (1f + player.Stats.CATK);
                        }
                        
                        monster.TakeDamage(damage);
                    }
                }
            }
        }
    }

    public override void Exit(Player player)
    {
        player.Animator?.ResetTrigger("Attack");
        player.Animator?.ResetTrigger("Idle");
        player.Animator?.ResetTrigger("IsMoving");
        
        player.Animator?.Update(0);
    }

    // 대신 공격 범위를 그리기 위한 프로퍼티 추가
    public float AttackRange => attackRange;
}
