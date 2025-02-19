using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class SkillContainer : MonoBehaviour
{
    // 蹂댁쑀???ㅽ궗 紐⑸줉
    private List<SkillName> ownedSkills = new List<SkillName>();

    // ?덈꺼?????좏깮 媛?ν븳 ?ㅽ궗 紐⑸줉
    private List<SkillName> selectableSkills = new List<SkillName>();

    private int maxActiveSkills = 3;
    private int maxPassiveSkills = 3;

    public void AddSkill(SkillName skillName)
    {
        // 蹂댁쑀???ㅽ궗 紐⑸줉??異붽?
        if (!ownedSkills.Contains(skillName))
        {
            ownedSkills.Add(skillName);
        }
    }

    private void Awake()
    {
        foreach (SkillName skillName in System.Enum.GetValues(typeof(SkillName)))
        {
            if (skillName == SkillName.None) continue;
            AddSelectableSkill(skillName);
        }
    }

    public void AddSelectableSkill(SkillName skillName)
    {
        // ?좏깮 媛?ν븳 ?ㅽ궗 紐⑸줉??異붽?
        if (!selectableSkills.Contains(skillName))
        {
            selectableSkills.Add(skillName);
        }
    }

    public List<SkillName> GetAvailableSkills()
    {
        // ?덈꺼?????좏깮 媛?ν븳 ?ㅽ궗 紐⑸줉 諛섑솚
        return new List<SkillName>(selectableSkills);
    }

    public SkillName GetSkill(SkillName skillName)
    {
        // 蹂댁쑀???ㅽ궗 紐⑸줉?먯꽌 ?대떦 SkillName??諛섑솚
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
}
