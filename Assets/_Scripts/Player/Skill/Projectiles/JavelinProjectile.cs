using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MonsterBase>(out var monster))
        {
            float finalFinalDamage = UnityEngine.Random.value < stats.critical ? stats.finalDamage * stats.cATK : stats.finalDamage;

            monster.TakeDamage(finalFinalDamage);
            DataManager.Instance.AddDamageData(finalFinalDamage, Enums.SkillName.Javelin);

            if (pierceCount > 0)
            {
                pierceCount--;
            }
            else
            {
                DestroyProjectile();
            }
        }
    }
}
