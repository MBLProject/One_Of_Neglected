using System.Collections;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PoisonShoesProjectile : Projectile
{
    private HashSet<MonsterBase> monstersInRange = new HashSet<MonsterBase>();
    private float duration = 5f; // 지속시간
    private float damagePerFrame = 0.5f; // 1초당 가한 프레임 데미지의 총량

    protected override void Start()
    {
        isMoving = true;
        transform.position = startPosition;
        cts = new CancellationTokenSource();
        CalculateDamagePerFrame();  // damagePerFrame 계산
        MoveProjectileAsync(cts.Token).Forget();
        Destroy(gameObject, duration);
    }

    // damage를 기반으로 damagePerFrame을 deltaTime을 고려하여 계산하는 함수
    private void CalculateDamagePerFrame()
    {
        // damage를 1초 동안 주는 총 데미지로 설정하고 deltaTime을 고려
        damagePerFrame = damage * Time.deltaTime;  // Time.deltaTime을 곱해주어 프레임마다 적용되는 데미지를 비례적으로 조정
    }

    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, float spd, float dmg, float maxDist = 0f, int pierceCnt = 0, float lifetime = 5f)
    {
        this.startPosition = startPos;
        this.targetPosition = targetPos;
        speed = spd;
        maxDistance = maxDist;
        damage = dmg;
        pierceCount = pierceCnt;
        lifeTime = lifetime;

        CancelInvoke("DestroyProjectile");

        Invoke("DestroyProjectile", lifeTime);

        // damagePerFrame 계산
        CalculateDamagePerFrame();

        direction = (targetPosition - startPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    protected override async UniTaskVoid MoveProjectileAsync(CancellationToken token)
    {
        while (isMoving)
        {
            if (!GameManager.Instance.isPaused)
            {
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

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isPaused)
        {
            foreach (var monster in monstersInRange.ToList())
            {
                monster.TakeDamage(damagePerFrame);
            }
        }
    }
}
