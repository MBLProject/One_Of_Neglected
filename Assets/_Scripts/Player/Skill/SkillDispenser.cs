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
            case Enums.SkillName.Claw:
                print($"RegisterSkill : {skillName}");
                var claw = new Claw(defaultCooldown);
                claw.StartMainTask();
                skills.Add(Enums.SkillName.Claw, claw);
                break;
            case Enums.SkillName.Aura:
                print($"RegisterSkill : {skillName}");
                var aura = new Aura(defaultCooldown);
                aura.StartMainTask();
                skills.Add(Enums.SkillName.Needle, aura);
                break;
            case Enums.SkillName.Shuriken:
                print($"RegisterSkill : {skillName}");
                var shuriken = new Shuriken(defaultCooldown);
                shuriken.StartMainTask();
                skills.Add(Enums.SkillName.Shuriken, shuriken);
                break;
            case Enums.SkillName.PoisonShoes:
                print($"RegisterSkill : {skillName}");
                var poisonShoes = new PoisonShoes(defaultCooldown);
                poisonShoes.StartMainTask();
                skills.Add(Enums.SkillName.Needle, poisonShoes);
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
