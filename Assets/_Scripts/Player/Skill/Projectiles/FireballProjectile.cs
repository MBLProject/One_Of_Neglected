using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FireballProjectile : Projectile
{
    private float knockbackForce;

    protected override void Start()
    {
        base.Start();
        isMoving = true;
        transform.position = startPosition;
        cts = new CancellationTokenSource();
        MoveProjectileAsync(cts.Token).Forget();
        knockbackForce = 5f;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MonsterBase>(out var monster))
        {
            float finalDamage = Random.value < stats.critical ? stats.finalDamage * stats.cATK : stats.finalDamage;

            monster.TakeDamage(finalDamage);
            
            DataManager.Instance.AddDamageData(finalDamage, Enums.SkillName.Fireball);
            monster.ApplyKnockback(transform.position, knockbackForce);
        }
    }
}
