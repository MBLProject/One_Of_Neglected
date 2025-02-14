using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdControlUniqueMonster : NormalMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 160f,
            speed: 1f,
            damage: 15f,
            range: 3f,
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