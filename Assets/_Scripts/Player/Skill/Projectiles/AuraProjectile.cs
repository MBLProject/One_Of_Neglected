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
    private float damagePerFrame = 0.5f; // 1珥덈떦 媛???꾨젅???곕?吏??珥앸웾


    protected override void Start()
    {
        isMoving = true;
        transform.SetParent(UnitManager.Instance.GetPlayer().transform);
        transform.localPosition = Vector3.zero;
        cts = new CancellationTokenSource();
        MoveProjectileAsync(cts.Token).Forget();
    }

    private void CalculateDamagePerFrame()
    {
        // damage瑜?1珥??숈븞 二쇰뒗 珥??곕?吏濡??ㅼ젙?섍퀬 deltaTime??怨좊젮
        damagePerFrame = damage * Time.deltaTime;  // Time.deltaTime??怨깊빐二쇱뼱 ?꾨젅?꾨쭏???곸슜?섎뒗 ?곕?吏瑜?鍮꾨??곸쑝濡?議곗젙
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

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isPaused)
        {
            // FixedUpdate에서 데미지 처리
            foreach (var monster in monstersInRange.ToList())
            {
                monster.TakeDamage(damagePerFrame);
            }
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

        CalculateDamagePerFrame();
    }
}
