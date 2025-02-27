using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MonsterBase>(out var monster))
        {
            float finalFinalDamage = Random.value < stats.critical ? stats.finalDamage * stats.cATK : stats.finalDamage;

            monster.TakeDamage(finalFinalDamage);
            DataManager.Instance.AddDamageData(finalFinalDamage, stats.skillName);

            if (stats.pierceCount > 0) stats.pierceCount--;
            else DestroyProjectile();
        }
        if (collision.TryGetComponent<MonsterProjectile>(out var monsterProjectile))
        {

            monsterProjectile.DestroyProjectile();
        }
    }
}
