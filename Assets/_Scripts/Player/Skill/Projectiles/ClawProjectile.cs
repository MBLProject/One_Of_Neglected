using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawProjectile : Projectile
{
    private SpriteRenderer myrenderer;

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

        direction = (targetPosition - startPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation *= Quaternion.AngleAxis(angle - 90, Vector3.forward);
        DecideImage();
    }

    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, ProjectileStats projectileStats)
    {
        startPosition = startPos;
        targetPosition = targetPos;
        stats = projectileStats;

        CancelInvoke("DestroyProjectile");

        Invoke("DestroyProjectile", stats.lifetime);

        direction = (targetPosition - startPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation *= Quaternion.AngleAxis(angle - 90, Vector3.forward);

        gameObject.transform.localScale = Vector3.one * stats.finalATKRange;
        DecideImage();
    }

    private void DecideImage()
    {
        myrenderer = GetComponent<SpriteRenderer>();
        switch (stats.level)
        {
            case 1:
            case 2:
                myrenderer.sprite = Resources.Load<Sprite>("Using/Projectile/ClawLv1Sprite");
                break;
            case 3:
            case 4:
            case 5:
                myrenderer.sprite = Resources.Load<Sprite>("Using/Projectile/ClawLv2Sprite");
                break;
            case 6:
                myrenderer.sprite = Resources.Load<Sprite>("Using/Projectile/ClawLv3Sprite");
                break;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MonsterBase>(out var monster))
        {
            float finalFinalDamage = Random.value < stats.critical ? stats.finalDamage * stats.cATK : stats.finalDamage;

            monster.TakeDamage(finalFinalDamage);
            DataManager.Instance.AddDamageData(finalFinalDamage, stats.skillName);
            Instantiate(Resources.Load<GameObject>("Using/Projectile/ClawEffect"),monster.gameObject.transform);
        }
    }
}
