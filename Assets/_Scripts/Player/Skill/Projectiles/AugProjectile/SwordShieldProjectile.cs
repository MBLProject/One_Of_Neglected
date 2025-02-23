using UnityEngine;
using static UnityEditor.MaterialProperty;

public class SwordShieldProjectile : PlayerProjectile
{
    protected override void Start()
    {
        pType = projType.Normal;
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (collision.TryGetComponent(out MonsterBase monster))
            {
                float finalFinalDamage = UnityEngine.Random.value < stats.critical ? stats.finalDamage * stats.cATK : stats.finalDamage;

                float finalDamage = finalFinalDamage * ((UnitManager.Instance.GetPlayer().Stats.CurrentDefense * 10) + 100) / 100;
                monster.TakeDamage(finalDamage);
                DataManager.Instance.AddDamageData(finalDamage, Enums.AugmentName.SwordShield);
                
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
}
