using UnityEngine;

public class LongBowProjectile : PlayerProjectile
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
                monster.TakeDamage(finalFinalDamage);
                DataManager.Instance.AddDamageData(finalFinalDamage, Enums.AugmentName.LongBow);
                
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
