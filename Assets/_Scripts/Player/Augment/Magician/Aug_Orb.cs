using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Aug_Orb : TimeBasedAugment
{
    private float orbDamage = 10f;
    private float orbLifetime = 5f;
    private float damageMultiplier = 1f;
    private float projectileSpeed = 2f;
    private float baseProjectileSize = 3f;
    private int penetration = 100;
    private float duration = 5f;
    private float maxDistance = 10f;
    
    private float CurrentDamage => owner.Stats.CurrentATK * damageMultiplier;
    private float CurrentProjectileSize => baseProjectileSize * owner.Stats.CurrentATKRange;

    public Aug_Orb(Player owner, float interval) : base(owner, interval)
    {
        aguName = Enums.AugmentName.Orb;
    }

    protected override void OnTrigger()
    {
        SpawnJewelProjectile();
    }

    private void SpawnJewelProjectile()
    {
        Vector2 randomOffset = Random.insideUnitCircle * 3f;
        Vector3 spawnPosition = owner.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
        Vector2 dashStart = owner.transform.position;

        ProjectileManager.Instance.SpawnPlayerProjectile(
            "JewelProjectile",
            dashStart,
            dashStart,
            projectileSpeed,
            CurrentDamage / 2,  
            CurrentProjectileSize,
            maxDistance,
            penetration,
            duration);
    }
}
