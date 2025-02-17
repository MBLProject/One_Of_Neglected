using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TankUniqueMonster : NormalMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 300f,
            speed: 1f,
            damage: 5f,
            range: 2f,
            cooldown: 1.5f,
            defense: 4f,        
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
