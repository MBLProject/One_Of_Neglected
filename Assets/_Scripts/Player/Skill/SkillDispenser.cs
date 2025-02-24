using System.Collections.Generic;
using UnityEngine;

public class SkillDispenser : MonoBehaviour
{
    public Dictionary<Enums.SkillName, Skill> skills = new Dictionary<Enums.SkillName, Skill>();
    //public List<Skill> skills = new List<Skill>();

    public int Count;

    private void Update()
    {
        // for Debug
        Count = skills.Count;

    }

    public void RegisterSkill(Enums.SkillName skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            print($"RegisterSkill!!!!2 : {skills[skillName].level}");

            skills[skillName].LevelUp(); // ???뉖낵??嶺뚮∥?꾥땻???筌뤾쑵??
            print($"RegisterSkill!!!!3 : {skills[skillName].level}");

            return;
        }
        print("RegisterSkill!!!!1");
        Skill newSkill = SkillFactory.CreateSkill(skillName);
        if (newSkill != null)
        {
            newSkill.StartMainTask();
            skills.Add(skillName, newSkill);
        }
    }

    public void UnRegisterSkill(Enums.SkillName skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            skills[skillName].StopMainTask();
            skills.Remove(skillName);
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
