using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class NeedleProjectile : Projectile
{
    private Collider2D projectileCollider;

    protected override void Start()
    {
        isMoving = false;
        transform.position = targetPosition;
        projectileCollider = GetComponent<Collider2D>();
        cts = new CancellationTokenSource();
        DespawnNeedleAtPosition().Forget();
    }

    protected override async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (collision.TryGetComponent(out MonsterBase monster))
                monster.TakeDamage(damage);

            if (projectileCollider != null)
                projectileCollider.enabled = false;

            await UniTask.Delay(TimeSpan.FromSeconds(0.25f), cancellationToken: cts.Token);
            DestroyProjectile();
        }
    }

    private async UniTaskVoid DespawnNeedleAtPosition()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f), cancellationToken: cts.Token);
        DestroyProjectile();
    }
}
