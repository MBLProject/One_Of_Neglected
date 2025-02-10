using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUniqueMonster : NormalMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 150f,
            speed: 3.8f,
            damage: 35f,
            range: 2f,
            cooldown: 1.2f
        );
    }
}