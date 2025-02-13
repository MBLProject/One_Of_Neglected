using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShurikenProjectile : Projectile
{
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
                transform.Rotate(0, 0, 360 * 7 * Time.deltaTime);

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

    protected override void OnBecameInvisible()        //화면 밖으로 나갔을 때 실행됨
    {
        if (pierceCount > 0) return;        // 관통력이 남아있다면? 무시
        else DestroyProjectile();           // 관통력을 모두 소진했다면? 삭제
    }
}