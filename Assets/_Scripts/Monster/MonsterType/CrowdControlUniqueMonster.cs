using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdControlUniqueMonster : NormalMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 160f,
            speed: 3.2f,
            damage: 15f,
            range: 3f,
            cooldown: 2f
        );
    }
}