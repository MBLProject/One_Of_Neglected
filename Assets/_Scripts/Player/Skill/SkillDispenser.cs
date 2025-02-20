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
            RegisterSkill(Enums.SkillName.PoisonShoes, 0.5f);
    }

    public void RegisterSkill(Enums.SkillName skillName, float defaultCooldown)
    {
        if (skills.ContainsKey(skillName))
        {
            skills[skillName].LevelUp(); // ??덇볼??筌롫뗄苑???紐꾪뀱
            return;
        }
        Skill newSkill = SkillFactory.CreateSkill(skillName);
        if (newSkill != null)
        {
            newSkill.InitSkill();
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
}
