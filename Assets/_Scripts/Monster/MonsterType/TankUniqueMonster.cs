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
            speed: 2.5f,
            damage: 20f,
            range: 2f,
            cooldown: 1.5f
        );
    }

}
