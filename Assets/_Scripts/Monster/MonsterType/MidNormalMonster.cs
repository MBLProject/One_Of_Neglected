using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGameMeleeMonster : NormalMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 40f,
            speed: 1.2f,
            damage: 1f,
            range: 1f,
            cooldown: 1f,
            defense:2f
        );
    }
}