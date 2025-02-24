using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using static Enums;

public class Aug_LongBow : ConditionalAugment
{
    private float damageMultiplier = 1f;
    private float projectileSpeed = 1f;
    private float baseProjectileSize = 10f;
    private int penetration = 0;
    private float delayedProjectileDelay = 0.1f;
    private float duration = 10f;
    private float maxDistance = 30f;

    private float CurrentDamage => owner.Stats.CurrentATK * damageMultiplier;
    private float CurrentProjectileSize => baseProjectileSize * owner.Stats.CurrentATKRange;
    private float lastAttackTime = 0f;

    public Aug_LongBow(Player owner) : base(owner)
    {
        aguName = AugmentName.LongBow;
    }

    public override void Activate()
    {
        base.Activate();
        owner.attackDetect += OnAttackDetect;
    }

    private async void OnAttackDetect(Vector3 targetPosition)
    {
        int projAmount = owner.Stats.CurrentProjAmount - 1;

        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

        for (int i = 0; i < projAmount + 1; i++)
        {
            SpawnProjectile(targetPosition);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
        
    }
    private void SpawnProjectile(Vector3 direction)
    {
        ProjectileManager.Instance.SpawnPlayerProjectile(
              "LongBowProjectile",
              owner.transform.position,
              direction,
              projectileSpeed,
              CurrentDamage,
              CurrentProjectileSize,
              maxDistance,
              penetration,
              duration
          );
    }
    public override bool CheckCondition()
    {
        return true;
    }

    public override void OnConditionDetect()
    {
    }

    protected override void OnLevelUp()
    {
        base.OnLevelUp();
        switch (level)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        owner.attackDetect -= OnAttackDetect;
    }
}
