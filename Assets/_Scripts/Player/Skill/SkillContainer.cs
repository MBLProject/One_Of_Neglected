using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class SkillContainer : MonoBehaviour
{
    // 보유한 스킬 목록
    private List<SkillName> ownedSkills = new List<SkillName>();

    // 레벨업 시 선택 가능한 스킬 목록
    private List<SkillName> selectableSkills = new List<SkillName>();

    private int maxActiveSkills = 3;
    private int maxPassiveSkills = 3;

    public void AddSkill(SkillName skillName)
    {
        // 보유한 스킬 목록에 추가
        if (!ownedSkills.Contains(skillName))
        {
            ownedSkills.Add(skillName);
        }
    }

    private void Awake()
    {
        foreach (SkillName skillName in System.Enum.GetValues(typeof(SkillName)))
        {
            AddSelectableSkill(skillName);
        }
    }

    public void AddSelectableSkill(SkillName skillName)
    {
        // 선택 가능한 스킬 목록에 추가
        if (!selectableSkills.Contains(skillName))
        {
            selectableSkills.Add(skillName);
        }
    }

    public List<SkillName> GetAvailableSkills()
    {
        // 레벨업 시 선택 가능한 스킬 목록 반환
        return new List<SkillName>(selectableSkills);
    }

    public SkillName GetSkill(SkillName skillName)
    {
        // 보유한 스킬 목록에서 해당 SkillName을 반환
        if (ownedSkills.Contains(skillName))
        {
            return skillName;
        }
        return Enums.SkillName.None;
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
