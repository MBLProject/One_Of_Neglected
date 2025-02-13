using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShurikenProjectile : Projectile
{
    public int spin = 1;
    public int mul = 1;

    protected override void Start()
    {
        base.Start();
        RotateProjectileAsync(cts.Token).Forget();
    }

    protected virtual async UniTaskVoid RotateProjectileAsync(CancellationToken token)
    {
        while (isMoving)
        {
            if (!GameManager.Instance.isPaused)
            {
                transform.Rotate(0, 0, 360 / spin * mul * Time.deltaTime);

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (pierceCount > 0)
            {
                Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, maxDistance);
                Collider2D closestMonster = null;
                float closestDistance = float.MaxValue;
                foreach (var col in monsters)
                {
                    if (col.CompareTag("Monster") && col != collision)
                    {
                        float distance = Vector2.Distance(transform.position, col.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestMonster = col;
                        }
                    }
                }
                if (closestMonster != null)
                {
                    targetPosition = closestMonster.transform.position;
                    pierceCount--;
                }
            }
            else
            {
                DestroyProjectile();
            }
        }
    }

    protected override void OnBecameInvisible()
    {
        if (pierceCount > 0) return;
        else DestroyProjectile();
    }
}