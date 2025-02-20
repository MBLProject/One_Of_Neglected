using System.Collections;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PoisonShoesProjectile : Projectile
{
    private HashSet<MonsterBase> monstersInRange = new HashSet<MonsterBase>();
    private float damagePerFrame = 0.5f;

    protected override void Start()
    {
        isMoving = true;
        transform.position = startPosition;
        CalculateDamagePerFrame();
    }

    private void CalculateDamagePerFrame()
    {
        damagePerFrame = damage * Time.deltaTime;
    }

    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, float spd, float dmg, float maxDist = 0f, int pierceCnt = 0, float lifetime = 1.1f)
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

        CalculateDamagePerFrame();
    }

    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, ProjectileStats projectileStats)
    {

        startPosition = startPos;
        targetPosition = targetPos;
        stats = projectileStats;

        print($"PoisonShoesProjectile - InitProjectile - lifetime : {stats.lifetime}");

        CancelInvoke("DestroyProjectile");

        Invoke("DestroyProjectile", stats.lifetime);

        CalculateDamagePerFrame();
    }

    protected override async UniTaskVoid MoveProjectileAsync(CancellationToken token)
    {
        await UniTask.CompletedTask;
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

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isPaused)
        {
            foreach (var monster in monstersInRange.ToList())
            {
                monster.TakeDamage(damagePerFrame);

                DamageTracker.OnDamageDealt?.Invoke(new DamageInfo
                {
                    damage = damagePerFrame,
                    projectileName = gameObject.name,
                });
            }
        }
    }
}
