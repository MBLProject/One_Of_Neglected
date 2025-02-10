using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class RangedNormalMonster : RangedMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 60f,
            speed: 2.5f,
            damage: 8f,
            range: 5f,
            cooldown: 1.5f
        );
    }
}
