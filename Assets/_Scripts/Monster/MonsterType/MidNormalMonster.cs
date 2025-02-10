using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGameMeleeMonster : NormalMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 120f,
            speed: 3.5f,
            damage: 15f,
            range: 1.5f,
            cooldown: 0.8f
        );
    }
}