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

        if (Input.GetKeyUp(KeyCode.P))
            RegisterSkill(Enums.SkillName.Gateway, 1f);
    }

    public void RegisterSkill(Enums.SkillName skillName, float defaultCooldown)
    {
        if (skills.ContainsKey(skillName))
        {
            skills[skillName].InitSkill(2f, 1, 0, 1, 1, 0.1f, 0.5f);
            return;
        }

        Skill newSkill = SkillFactory.CreateSkill(skillName, defaultCooldown);
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

    public void UpdateSkill(Enums.SkillName skillName)
    {

    }
}
