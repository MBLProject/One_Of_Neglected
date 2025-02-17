using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Linq;

public class PoisonShoesProjectile : Projectile
{
    private HashSet<MonsterBase> monstersInRange = new HashSet<MonsterBase>();
    private float duration = 5f; // 잔류 시간
    private float damagePerFrame = 0.5f; // 프레임 당 데미지

    protected override void Start()
    {
        isMoving = true;
        transform.position = startPosition; // 시작 위치 설정
        cts = new CancellationTokenSource();
        MoveProjectileAsync(cts.Token).Forget();
        Destroy(gameObject, duration); // 일정 시간 후에 파괴
    }

    protected override async UniTaskVoid MoveProjectileAsync(CancellationToken token)
    {
        while (isMoving)
        {
            if (!GameManager.Instance.isPaused)
            {
                // 몬스터에게 데미지 주기
                foreach (var monster in monstersInRange.ToList())
                {
                    monster.TakeDamage(damagePerFrame);
                }
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
        if (collision.TryGetComponent<MonsterBase>(out var monster) && monstersInRange.Add(monster))
        {
            monster.OnDeath += RemoveMonsterFromSet;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MonsterBase>(out var monster))
        {
            RemoveMonsterFromSet(monster);
        }
    }

    private void RemoveMonsterFromSet(MonsterBase monster)
    {
        monstersInRange.Remove(monster);
        monster.OnDeath -= RemoveMonsterFromSet;
    }
}