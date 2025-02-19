using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System;
using System.Linq;

public class GatewayProjectile : Projectile
{
    private float knockbackForce;

    protected override void Start()
    {
        isMoving = true;
        transform.position = targetPosition;
        cts = new CancellationTokenSource();
        DespawnNeedleAtPosition().Forget();
        knockbackForce = 2.5f;
    }

    private async UniTaskVoid DespawnNeedleAtPosition()
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            if (isMoving)
            {
                DestroyProjectile();
                isMoving = false;
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Needle despawn was canceled.");
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MonsterBase>(out var monster))
        {
            monster.TakeDamage(damage);

            monster.ApplyKnockback(transform.position, knockbackForce);
        }
        if (collision.TryGetComponent<MonsterProjectile>(out var monsterProjectile))
        {
            monsterProjectile.Invoke("DestroyProjectile", 0f);
        }
    }


    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, float spd, float dmg, float maxDist = 0f, int pierceCnt = 0, float lifetime = 5f)
    {
        startPosition = startPos;
        targetPosition = targetPos;
        speed = spd;
        maxDistance = maxDist;
        damage = dmg;
        pierceCount = pierceCnt;
        lifeTime = lifetime;

        CancelInvoke("DestroyProjectile");
        Invoke("DestroyProjectile", lifeTime);
    }
}
