using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Enums;

public class SkillSelector : MonoBehaviour
{
    private SkillContainer skillContainer;
    private SkillDispenser skillDispenser;

    public int count;

    private void Start()
    {
        skillContainer = GetComponent<SkillContainer>();
        if (UnitManager.Instance.GetPlayer().TryGetComponent(out skillDispenser))
            print("Success");
    }

    public void Initialize(SkillContainer container, SkillDispenser dispenser)
    {
        skillContainer = container;
        skillDispenser = dispenser;
    }

    private void Update()
    {
        count = skillContainer.OwnedSkills.Count;

        if (Input.GetKeyDown(KeyCode.O))
            ChooseSkill(SkillName.Javelin);
        if (Input.GetKeyDown(KeyCode.P))
            UnitManager.Instance.GetPlayer().Stats.ModifyStatValue(StatType.ProjAmount, 1f);
    }

    public List<SkillName> SelectSkills()
    {
        List<SkillName> availableSkills = skillContainer.GetAvailableSkills();
        HashSet<SkillName> selectedSkills = new HashSet<SkillName>();

        bool activeMax = !skillContainer.CanAddActiveSkill();
        bool passiveMax = !skillContainer.CanAddPassiveSkill();

        if (activeMax && passiveMax)
        {
            foreach (var skill in skillContainer.OwnedSkills)
            {
                if (IsMaxLevel(skill)) continue;
                selectedSkills.Add(skill);
            }
        }
        else if (activeMax)
        {
            foreach (var skill in availableSkills)
            {
                if (IsMaxLevel(skill)) continue;

                if (IsPassiveSkill(skill) || IsEtcSkill(skill) || skillContainer.GetSkill(skill) != SkillName.None)
                {
                    selectedSkills.Add(skill);
                }
            }
        }
        else if (passiveMax)
        {
            foreach (var skill in availableSkills)
            {
                if (IsMaxLevel(skill)) continue;

                if (IsActiveSkill(skill) || IsEtcSkill(skill) || skillContainer.GetSkill(skill) != SkillName.None)
                {
                    selectedSkills.Add(skill);
                }
            }
        }
        else
        {
            foreach (var skill in availableSkills)
            {
                if (IsMaxLevel(skill)) continue;
                selectedSkills.Add(skill);
            }
        }

        var skillList = selectedSkills.ToList();
        print($"selectedSkills : {selectedSkills.Count}, skillList : {skillList.Count}");

        if (skillList.Count < 3)
        {
            if(!skillList.Contains(SkillName.Cheese))
                skillList.Add(SkillName.Cheese);
            if (!skillList.Contains(SkillName.Gold))
                skillList.Add(SkillName.Gold);
        }

        // Fisher - Yates Shuffle
        int n = skillList.Count;

        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            var temp = skillList[k];
            skillList[k] = skillList[n];
            skillList[n] = temp;
        }

        return skillList.Take(3).ToList();
    }

    public void ChooseSkill(SkillName chosenAbility)
    {
        SkillName skillName = skillContainer.GetSkill(chosenAbility);

        if (skillName == SkillName.None)
        {
            skillDispenser.RegisterSkill(chosenAbility);
            skillContainer.AddSkill(chosenAbility);
        }
        else
        {
            skillDispenser.RegisterSkill(skillName);
        }

        if (IsMaxLevel(skillName))
        {
            skillContainer.SelectableSkills.Remove(skillName);
        }

        if (skillDispenser.skills.Count >= 10)
        {
            int nonMaxLevelSkills = skillDispenser.skills.Count(skill => !IsMaxLevel(skill.Key));
            if (nonMaxLevelSkills <= 2)
            {
                if (!skillContainer.SelectableSkills.Contains(SkillName.Cheese))
                    skillContainer.AddSelectableSkill(SkillName.Cheese);
                if (!skillContainer.SelectableSkills.Contains(SkillName.Gold))
                    skillContainer.AddSelectableSkill(SkillName.Gold);
            }
        }
    }

    public void DeductSkill(SkillName deDuctSkillName)
    {
        if (skillDispenser.skills.ContainsKey(deDuctSkillName))
        {
            skillContainer.removedSkills.Add(deDuctSkillName, skillDispenser.skills[deDuctSkillName].level);

            skillDispenser.UnRegisterSkill(deDuctSkillName);

            skillContainer.RemoveSkill(deDuctSkillName);
        }
    }

    public int SkillLevel(SkillName skillName)
    {
        if (skillDispenser.skills.ContainsKey(skillName))
        {
            return skillDispenser.skills[skillName].level;
        }
        else if (skillContainer.removedSkills.ContainsKey(skillName))
        {
            return skillContainer.removedSkills[skillName];
        }
        else
        {
            Debug.Log($"{skillName} is not Registerd / Removed Skill!!!");
            return -1;
        }
    }

    private bool IsActiveSkill(SkillName skillName)
    {
        return SkillFactory.IsActiveSkill(skillName) == 1;
    }

    private bool IsPassiveSkill(SkillName skillName)
    {
        return SkillFactory.IsActiveSkill(skillName) == 0;
    }

    private bool IsEtcSkill(SkillName skillName)
    {
        return SkillFactory.IsActiveSkill(skillName) == 2;
    }

    private bool IsMaxLevel(SkillName skillName)
    {
        if (skillName == SkillName.Ring) return SkillLevel(skillName) >= 2;
        return IsActiveSkill(skillName) ? SkillLevel(skillName) >= 6 : SkillLevel(skillName) >= 5;
    }
}
