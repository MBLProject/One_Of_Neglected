using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using static Enums;

[Serializable]
public class Skill
{
    public SkillName skillName;

    public int level;

    protected bool isSkillActive = false;

    protected Skill(SkillName skillName)
    {
        this.skillName = skillName;

        LevelUp();
    }

    protected virtual void SubscribeToPlayerStats()
    {
        //PlayerStats playerStats = UnitManager.Instance.GetPlayer().Stats;
        //playerStats.OnATKChanged += (value) => { stats.aTK = value; };
    }

    public virtual void StartMainTask()
    {
    }

    public virtual void StopMainTask()
    {
    }

    public virtual void ModifySkill()
    {
        
    }

    public virtual void InitSkill()
    {

    }

    public virtual void LevelUp()
    {
        if (level >= 6)
        {
            Debug.LogError($"Skill {skillName}'s current level is {level}!! You Cannot Order this skill's LevelUp!!");
            return;
        }
        level++;
    }
}
