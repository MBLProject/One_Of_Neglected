using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class PlayerProjectile : Projectile
{

    protected projType pType;

    protected enum projType
    {
        Melee,
        Normal,
        None,
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
                    Vector3 direction = (targetPosition - transform.position).normalized;
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
            }

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