using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateNormalMonster : NormalMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 200f,
            speed: 1f,
            damage: 25f,
            range: 1.5f,
            cooldown: 0.7f
        );
    }
}