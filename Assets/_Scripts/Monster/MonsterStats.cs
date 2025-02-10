using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MonsterStats
{
    public float maxHealth;     // 최대 체력
    public float currentHealth; // 현재 체력
    public float moveSpeed;     // 이동 속도
    public float attackDamage;  // 공격력
    public float attackRange;   // 공격 범위
    public float attackCooldown;// 공격 쿨다운

    public MonsterStats(float health, float speed, float damage, float range, float cooldown)
    {
        maxHealth = currentHealth = health;
        moveSpeed = speed;
        attackDamage = damage;
        attackRange = range;
        attackCooldown = cooldown;
    }
}