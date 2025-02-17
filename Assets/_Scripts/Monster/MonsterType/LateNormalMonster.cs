using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateNormalMonster : NormalMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 100f,
            speed: 1.5f,
            damage: 15f,
            range: 1f,
            cooldown: 1f,
            defense: 3f
        );
    }
}