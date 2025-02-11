using System.Collections.Generic;
using UnityEngine;

public class SkillDispesner : MonoBehaviour
{
    public Dictionary<Enums.SkillName, Skill> skills = new Dictionary<Enums.SkillName, Skill>();

    public int Count;

    private void Update()
    {
        // for Debug
        Count = skills.Count;

        if (Input.GetKeyUp(KeyCode.A))
            RegisterSkill(Enums.SkillName.Needle, 1f);
    }

    public void RegisterSkill(Enums.SkillName skillName, float defaultCooldown)
    {
        switch (skillName)
        {
            case Enums.SkillName.Needle:
                skills.Add(skillName, new Needle(defaultCooldown));
                break;
            case Enums.SkillName.Fireball:
                skills.Add(skillName, new Fireball(defaultCooldown));
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
