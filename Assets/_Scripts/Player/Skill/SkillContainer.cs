using System;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class SkillContainer : MonoBehaviour
{
    private List<SkillName> ownedSkills = new List<SkillName>();

    private List<SkillName> selectableSkills = new List<SkillName>();

    public Dictionary<SkillName, int> removedSkills = new Dictionary<SkillName, int>();

    private int maxActiveSkills = 3;
    private int maxPassiveSkills = 3;

    public void AddSkill(SkillName skillName)
    {
        // 癰귣똻?????쎄텢 筌뤴뫖以???곕떽?
        if (!ownedSkills.Contains(skillName))
        {
            ownedSkills.Add(skillName);
        }
    }

    private void Awake()
    {
        foreach (SkillName skillName in Enum.GetValues(typeof(SkillName)))
        {
            if (skillName == SkillName.None) continue;
            AddSelectableSkill(skillName);
        }
    }

    public void AddSelectableSkill(SkillName skillName)
    {
        // ?醫뤾문 揶쎛?館釉???쎄텢 筌뤴뫖以???곕떽?
        if (!selectableSkills.Contains(skillName))
        {
            selectableSkills.Add(skillName);
        }
    }

    public List<SkillName> GetAvailableSkills()
    {
        // ??덇볼?????醫뤾문 揶쎛?館釉???쎄텢 筌뤴뫖以?獄쏆꼹??
        return new List<SkillName>(selectableSkills);
    }

    public SkillName GetSkill(SkillName skillName)
    {
        // 癰귣똻?????쎄텢 筌뤴뫖以?癒?퐣 ????SkillName??獄쏆꼹??
        if (ownedSkills.Contains(skillName))
        {
            return skillName;
        }
        return SkillName.None;
    }

    public bool CanAddActiveSkill()
    {
        int activeSkillCount = CountActiveSkills();
        return activeSkillCount < maxActiveSkills;
    }

    public bool CanAddPassiveSkill()
    {
        int passiveSkillCount = CountPassiveSkills();
        return passiveSkillCount < maxPassiveSkills;
    }

    private int CountActiveSkills()
    {
        int count = 0;
        foreach (var skillName in ownedSkills)
        {
            if (SkillFactory.IsActiveSkill(skillName)) count++;
        }
        return count;
    }

    private int CountPassiveSkills()
    {
        int count = 0;
        foreach (var skillName in ownedSkills)
        {
            if (!SkillFactory.IsActiveSkill(skillName)) count++;
        }
        return count;
    }

    public void RemoveSkill(SkillName deDuctSkillName)
    {
        if (ownedSkills.Contains(deDuctSkillName))
        {
            ownedSkills.Remove(deDuctSkillName);
            selectableSkills.Remove(deDuctSkillName);
        }
    }
}
