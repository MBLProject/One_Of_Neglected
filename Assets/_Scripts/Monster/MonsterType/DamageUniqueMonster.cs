using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUniqueMonster : NormalMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 150f,
            speed: 1f,
            damage: 35f,
            range: 2f,
            cooldown: 1.5f,
            defense: 5f,
            regen: 5f,
            regenDelay: 3f
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