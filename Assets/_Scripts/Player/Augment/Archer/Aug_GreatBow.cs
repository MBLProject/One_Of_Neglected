using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Aug_GreatBow : TimeBasedAugment
{
    private float damageMultiplier = 1f; 
    private float projectileSpeed = 2f;
    private float baseProjectileSize = 1f;
    private int penetration = 1000;
    private float duration = 5f;
    private float maxDistance = 10f;

    private float CurrentDamage => owner.Stats.CurrentATK * damageMultiplier;
    private float CurrentProjectileSize => baseProjectileSize * owner.Stats.CurrentATKRange;

    public Aug_GreatBow(Player owner, float interval) : base(owner, interval)
    {
        aguName = Enums.AugmentName.GreatBow;
        damageMultiplier = 5f;
    }

    protected override async void OnTrigger()
    {
        Vector3 targetPos = UnitManager.Instance.GetNearestMonster().transform.position;
        Vector3 direction = (targetPos - owner.transform.position).normalized;

        int projAmount = owner.Stats.CurrentProjAmount - 1;

        for (int i = 0; i < projAmount + 1; i++)
        {
            SpawnProjectile(direction);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
    }

    private void SpawnProjectile(Vector3 direction)
    {
        Vector3 targetPosition = owner.transform.position + direction * 10f;

        ProjectileManager.Instance.SpawnPlayerProjectile(
            "GreatBowProjectile",
            owner.transform.position,
            targetPosition,
            projectileSpeed,
            CurrentDamage,
            CurrentProjectileSize,
            maxDistance,
            penetration,
            duration);
    }


    protected override void OnLevelUp()
    {
        base.OnLevelUp();
        switch (level)
        {
            case 1:
                break;
            case 2:
                ModifyBaseInterval(-2f);
                break;
            case 3:
                //TODO : 타격횟수 2회로
                break;
            case 4:
                damageMultiplier *= 2f;
                break;
            case 5:
                //TODO : 폭발화살
                break;
        }
    }
}
