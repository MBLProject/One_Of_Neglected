using System.Collections;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PoisonShoesProjectile : Projectile
{
    private HashSet<MonsterBase> monstersInRange = new HashSet<MonsterBase>();
    private float duration = 5f; // 吏?띿떆媛?
    private float damagePerFrame = 0.5f; // 1珥덈떦 媛???꾨젅???곕?吏??珥앸웾

    protected override void Start()
    {
        isMoving = true;
        transform.position = startPosition;
        cts = new CancellationTokenSource();
        CalculateDamagePerFrame();  // damagePerFrame 怨꾩궛
        MoveProjectileAsync(cts.Token).Forget();
    }

    // damage瑜?湲곕컲?쇰줈 damagePerFrame??deltaTime??怨좊젮?섏뿬 怨꾩궛?섎뒗 ?⑥닔
    private void CalculateDamagePerFrame()
    {
        // damage瑜?1珥??숈븞 二쇰뒗 珥??곕?吏濡??ㅼ젙?섍퀬 deltaTime??怨좊젮
        damagePerFrame = damage * Time.deltaTime;  // Time.deltaTime??怨깊빐二쇱뼱 ?꾨젅?꾨쭏???곸슜?섎뒗 ?곕?吏瑜?鍮꾨??곸쑝濡?議곗젙
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

        // damagePerFrame 怨꾩궛
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
            }
        }
    }
}
