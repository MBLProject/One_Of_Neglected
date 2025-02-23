using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class PlayerProjectile : Projectile
{
    protected projType pType;
    protected float projectileSize = 1f;

    protected enum projType
    {
        Melee,
        Normal,
        None,
    }

    public virtual void InitProjectile(Vector3 startPos, Vector3 targetPos, float spd, float dmg, float size, float maxDist = 0f, int pierceCnt = 0, float lifetime = 5f)
    {
        projectileSize = size;
        transform.localScale = Vector3.one * size;
        base.InitProjectile(startPos, targetPos, spd, dmg, maxDist, pierceCnt, lifetime);
    }

    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, ProjectileStats projectileStats)
    {
        // 기존 크기 유지
        Vector3 currentScale = transform.localScale;
        base.InitProjectile(startPos, targetPos, projectileStats);
        transform.localScale = currentScale;
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override async UniTaskVoid MoveProjectileAsync(CancellationToken token)
    {
        try
        {
            float traveledDistance = 0f;
         

            while (isMoving && !token.IsCancellationRequested)
            {
                if (gameObject == null || !isMoving)
                {
                    break;
                }

                if (!GameManager.Instance.isPaused)
                {
                    Vector3 direction = (targetPosition - startPosition).normalized;
                    transform.position += speed * Time.deltaTime * direction;

                    traveledDistance = (transform.position - startPosition).magnitude;
                    if (traveledDistance >= maxDistance)
                    {
                        DestroyProjectile();
                        break;
                    }
                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                }
                else
                {
                    await UniTask.Yield(token);
                }
            }
        }
        catch (Exception ex)
        {
            print(ex.ToString() + " " + ex.StackTrace.ToString());
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (collision.TryGetComponent(out MonsterBase monster))
            {
                monster.TakeDamage(damage);
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
} 