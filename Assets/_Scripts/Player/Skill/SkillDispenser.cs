using System.Collections.Generic;
using UnityEngine;

public class SkillDispesner : MonoBehaviour
{
    public Dictionary<Enums.SkillName, Skill> skills = new Dictionary<Enums.SkillName, Skill>();
    //public List<Skill> skills = new List<Skill>();

    public int Count;

    private void Update()
    {
        // for Debug
        Count = skills.Count;

        if (Input.GetKeyUp(KeyCode.P))
            RegisterSkill(Enums.SkillName.Javelin, 1f);
    }

    public void RegisterSkill(Enums.SkillName skillName, float defaultCooldown)
    {
        switch (skillName)
        {
            case Enums.SkillName.Javelin:
                print($"RegisterSkill : {skillName}");
                var javelin = new Javelin(defaultCooldown);
                javelin.StartMainTask();
                skills.Add(Enums.SkillName.Javelin, javelin);
                break;
            case Enums.SkillName.Needle:
                print($"RegisterSkill : {skillName}");
                var needle = new Needle(defaultCooldown);
                needle.StartMainTask();
                skills.Add(Enums.SkillName.Needle, needle);
                break;
        }
    }

    public void UnRegisterSkill(Enums.SkillName skillName)
    {

    }

    public void UpdateSkill(Enums.SkillName skillName)
    {

    }
}
