using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

// 0~3분 초반부 일반 몬스터
public class EarlyNormalMonster : NormalMonster
{
    protected override void InitializeStats()
    {
        stats = new MonsterStats(
            health: 80f,
            speed: 3f,
            damage: 10f,
            range: 1.5f,
            cooldown: 1f
        );
    }
}
