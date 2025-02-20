using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javelin : ActiveSkill
{
    public Javelin() : base(Enums.SkillName.Javelin) { }

    protected override void SubscribeToPlayerStats()
    {
        //PlayerStats playerStats = UnitManager.Instance.GetPlayer().Stats;
        //playerStats.OnATKChanged += (value) => { stats.aTK = value; };
    }

    public override void LevelUp()
    {
        base.LevelUp();

        switch (level)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
        }
    }
}
