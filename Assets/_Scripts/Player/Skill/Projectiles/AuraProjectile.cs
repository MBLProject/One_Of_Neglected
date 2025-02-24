using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System;
using System.Linq;

public class AuraProjectile : Projectile
{
    private HashSet<MonsterBase> monstersInRange = new HashSet<MonsterBase>();
    private float tickInterval = 0.5f;

    protected override void Start()
    {
        isMoving = true;
        transform.SetParent(UnitManager.Instance.GetPlayer().transform);
        transform.localPosition = Vector3.zero;
        cts = new CancellationTokenSource();
        MoveProjectileAsync(cts.Token).Forget();
        ApplyDamageLoop(cts.Token).Forget();
    }
    protected override async UniTaskVoid MoveProjectileAsync(CancellationToken token)
    {
        while (isMoving)
        {
            if (!GameManager.Instance.isPaused)
            {
                transform.Rotate(0, 0, 360 * 5 * Time.deltaTime);

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }

    private async UniTaskVoid ApplyDamageLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (!GameManager.Instance.isPaused)
            {
                foreach (var monster in monstersInRange.ToList())
                {
                    float finalFinalDamage = UnityEngine.Random.value < stats.critical ? stats.finalDamage * stats.cATK : stats.finalDamage;

                    monster.TakeDamage(finalFinalDamage);

                    DataManager.Instance.AddDamageData(finalFinalDamage, stats.skillName);
                }
            }
            await UniTask.Delay(TimeSpan.FromSeconds(tickInterval), cancellationToken: token);
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
            if (monstersInRange.Contains(monster))
            {
                RemoveMonsterFromSet(monster);
            }
        }
    }

    private void RemoveMonsterFromSet(MonsterBase monster)
    {
        monstersInRange.Remove(monster);
        monster.OnDeath -= RemoveMonsterFromSet;
    }

    //private void FixedUpdate()
    //{
    //    if (!GameManager.Instance.isPaused)
    //    {
    //        // FixedUpdate?癒?퐣 ?怨?筌왖 筌ｌ꼶??
    //        foreach (var monster in monstersInRange.ToList())
    //        {
    //            monster.TakeDamage(damagePerFrame);

    //            DamageTracker.OnDamageDealt?.Invoke(new DamageInfo
    //            {
    //                damage = damagePerFrame,
    //                projectileName = gameObject.name,
    //            });
    //        }
    //    }
    //}

    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, float spd, float dmg, float maxDist = 0f, int pierceCnt = 0, float lifetime = 5f)
    {
        startPosition = startPos;
        targetPosition = targetPos;
        speed = spd;
        maxDistance = maxDist;
        damage = dmg;
        pierceCount = pierceCnt;
        lifeTime = lifetime;

    }

    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, ProjectileStats projectileStats)
    {
        startPosition = startPos;
        targetPosition = targetPos;
        stats = projectileStats;

        gameObject.transform.localScale = new Vector3(stats.finalATKRange, stats.finalATKRange, stats.finalATKRange);
    }
}
