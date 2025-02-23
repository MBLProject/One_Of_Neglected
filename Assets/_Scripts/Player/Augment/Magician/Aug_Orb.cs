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
    private float projectileSize = 3f;
    private int penetration = 100;
    private float duration = 5f;
    private float maxDistance = 10f;
    public Aug_Orb(Player owner, float interval) : base(owner, interval)
    {
        aguName = Enums.AugmentName.Orb;
    }

    private float CurrentDamage => owner.Stats.CurrentATK * damageMultiplier;


    protected override void OnTrigger()
    {
        SpawnJewelProjectile();
    }

    private void SpawnJewelProjectile()
    {
        Vector2 randomOffset = Random.insideUnitCircle * 3f;
        Vector3 spawnPosition = owner.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
        Vector2 dashStart = owner.transform.position;
        
        float damage = CurrentDamage;
        Debug.Log($"Orb Damage: {damage}, CurrentATK: {owner.Stats.CurrentATK}, Multiplier: {damageMultiplier}");
        
        PlayerProjectile proj = ProjectileManager.Instance.SpawnPlayerProjectile(
            "JewelProjectile",
            dashStart,
            dashStart,
            0f,
            damage,
            projectileSize,
            maxDistance,
            penetration,
            duration);
        proj.transform.rotation = Quaternion.identity;
    }
}
