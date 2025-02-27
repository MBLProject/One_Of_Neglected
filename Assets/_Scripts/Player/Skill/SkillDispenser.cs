using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillDispenser : MonoBehaviour
{
    public Dictionary<Enums.SkillName, Skill> skills = new Dictionary<Enums.SkillName, Skill>();
    //public List<Skill> skills = new List<Skill>();

    public List<Enums.SkillName> Count = new List<Enums.SkillName>();

    public void RegisterSkill(Enums.SkillName skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            skills[skillName].LevelUp();
            return;
        }

        Skill newSkill = SkillFactory.CreateSkill(skillName);

        if (newSkill != null)
        {
            newSkill.StartMainTask();
            skills.Add(skillName, newSkill);
            Count.Add(skillName);
            
            if (SkillFactory.IsActiveSkill(skillName) == 0)
            {
                newSkill.ModifySkill();
            }
        }
    }

    public void UnRegisterSkill(Enums.SkillName skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            skills[skillName].UnRegister();
            skills[skillName].StopMainTask();
            skills.Remove(skillName);
            print("SuccessFully Removed in Dispenser!!");

        }
        else
            print($"{skillName} is NOT Registered Skill!!");
    }

    public void FireAllSkills()
    {
        foreach (var skill in skills)
        {
            skill.Value.Fire();
        }
    }
}
