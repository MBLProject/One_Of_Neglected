using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class NeedleProjectile : Projectile
{

    protected override void Start()
    {
        isMoving = true;
        transform.position = targetPosition;
        cts = new CancellationTokenSource();
        DespawnNeedleAtPosition().Forget();
        Invoke("DestroyProjectile", 0.25f);
    }

    private async UniTaskVoid DespawnNeedleAtPosition()
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
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

    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, float spd, float dmg, float maxDist = 0f, int pierceCnt = 0, float lifetime = 1f)
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

        direction = (targetPosition - startPos).normalized;
    }

    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, ProjectileStats projectileStats)
    {
        startPosition = startPos;
        targetPosition = targetPos;

        stats = projectileStats;

        CancelInvoke("DestroyProjectile");
        Invoke("DestroyProjectile", stats.lifetime);

        direction = (targetPosition - startPos).normalized;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MonsterBase>(out var monster))
        {
            float finalFinalDamage = UnityEngine.Random.value < stats.critical ? stats.finalDamage * stats.cATK : stats.finalDamage;

            monster.TakeDamage(finalFinalDamage);
            DataManager.Instance.AddDamageData(finalFinalDamage, stats.skillName);

            if (pierceCount > 0)
            {
                pierceCount--;
            }
            else
            {
                DestroyProjectile();
            }
        }
    }
}
