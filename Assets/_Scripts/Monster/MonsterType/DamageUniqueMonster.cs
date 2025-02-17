using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUniqueMonster : NormalMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 100f,
            speed: 1.2f,
            damage: 10f,
            range: 0.5f,
            cooldown: 1f,
            defense: 1f,
            regen: 1f,
            regenDelay: 1f
        );
    }
    protected override void Update()
    {
        base.Update();
        if (stats.healthRegen > 0)
        {
            stats.RegenerateHealth(Time.deltaTime);
        }
    }
}