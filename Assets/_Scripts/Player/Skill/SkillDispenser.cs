using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillDispesner : MonoBehaviour
{
    public Dictionary<Enums.SkillName, Skill> skills = new Dictionary<Enums.SkillName, Skill>();

    void Start()
    {
        //skills.Add(Enums.SkillName.Aura, new Skill(Enums.SkillName.Aura, 1.5f));
        //skills.Add(Enums.SkillName.Ifrit, new Skill(Enums.SkillName.Ifrit, 2.0f));
        //skills.Add(Enums.SkillName.GravityField, new Skill(Enums.SkillName.GravityField, 3.0f));
    }

    void Update()
    {
    }
}
