using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillDispesner : MonoBehaviour
{
    public Dictionary<SkillName, Skill> skills = new Dictionary<SkillName, Skill>();

    void Start()
    {
        skills.Add(SkillName.Aura, new Skill(SkillName.Aura, 1.5f));
        skills.Add(SkillName.Ifrit, new Skill(SkillName.Ifrit, 2.0f));
        skills.Add(SkillName.GravityField, new Skill(SkillName.GravityField, 3.0f));
    }

    void Update()
    {
        foreach (var skill in skills)
        {
            skill.Value.UpdateSkill(Time.deltaTime);
        }
    }
}
