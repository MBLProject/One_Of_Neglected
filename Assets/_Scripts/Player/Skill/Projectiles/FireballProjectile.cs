using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FireballProjectile : Projectile
{
    private float knockbackForce;

    protected override void Start()
    {
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
            monster.TakeDamage(damage);

            monster.ApplyKnockback(transform.position, knockbackForce);

            DamageTracker.OnDamageDealt?.Invoke(new DamageInfo
            {
                damage = damage,
                projectileName = gameObject.name,
            });
        }
    }
}
