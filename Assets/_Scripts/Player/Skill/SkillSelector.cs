using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillSelector : MonoBehaviour
{
    private SkillContainer skillContainer;
    private SkillDispenser skillDispenser;

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
        if (Input.GetKeyDown(KeyCode.O))
            ChooseSkill(Enums.SkillName.Shuriken);
        if (Input.GetKeyDown(KeyCode.P))
            ChooseSkill(Enums.SkillName.Ring);
    }

    public List<Enums.SkillName> SelectSkills()
    {
        List<Enums.SkillName> availableSkills = skillContainer.GetAvailableSkills();
        HashSet<Enums.SkillName> selectedSkills = new HashSet<Enums.SkillName>();

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

                if (IsPassiveSkill(skill) || IsEtcSkill(skill) || skillContainer.GetSkill(skill) != Enums.SkillName.None)
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

                if (IsActiveSkill(skill) || IsEtcSkill(skill) || skillContainer.GetSkill(skill) != Enums.SkillName.None)
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

        foreach (var skill in skillList)
        {
            print(Enum.GetName(typeof(Enums.SkillName), skill));
        }

        if (skillList.Count < 3)
        {
            Debug.LogWarning($"selected Abilities is less than 3!!! selectedAbilities.Count : {skillList.Count}");
            return skillList;
        }

        // Fisher - Yates Shuffle
        int n = skillList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            var temp = skillList[k];
            skillList[k] = skillList[n];
            skillList[n] = temp;
        }
        
        return skillList.Take(3).ToList();
    }

    public void ChooseSkill(Enums.SkillName chosenAbility)
    {
        Enums.SkillName skillName = skillContainer.GetSkill(chosenAbility);

        if (skillName == Enums.SkillName.None)
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

        if(skillDispenser.skills.Count >= 10)
        {
            int nonMaxLevelSkills = skillDispenser.skills.Count(skill => !IsMaxLevel(skill.Key));
            if (nonMaxLevelSkills <= 2)
            {
                if (!skillContainer.SelectableSkills.Contains(Enums.SkillName.Cheese))
                    skillContainer.AddSelectableSkill(Enums.SkillName.Cheese);
                if (!skillContainer.SelectableSkills.Contains(Enums.SkillName.Gold))
                    skillContainer.AddSelectableSkill(Enums.SkillName.Gold);
            }
        }
    }


    public void DeductSkill(Enums.SkillName deDuctSkillName)
    {
        if (skillDispenser.skills.ContainsKey(deDuctSkillName))
        {
            skillContainer.removedSkills.Add(deDuctSkillName, skillDispenser.skills[deDuctSkillName].level);

            skillDispenser.UnRegisterSkill(deDuctSkillName);

            skillContainer.RemoveSkill(deDuctSkillName);
        }
    }

    public int SkillLevel(Enums.SkillName skillName)
    {
        if (skillDispenser.skills.ContainsKey(skillName))
        {
            return skillDispenser.skills[skillName].level;
        }
        else if(skillContainer.removedSkills.ContainsKey(skillName))
        {
            return skillContainer.removedSkills[skillName];
        }
        else
        {
            Debug.Log($"{skillName} is not Registerd / Removed Skill!!!");
            return -1;
        }
    }

    private bool IsActiveSkill(Enums.SkillName skillName)
    {
        return SkillFactory.IsActiveSkill(skillName) == 1;
    }

    private bool IsPassiveSkill(Enums.SkillName skillName)
    {
        return SkillFactory.IsActiveSkill(skillName) == 0;
    }

    private bool IsEtcSkill(Enums.SkillName skillName)
    {
        return SkillFactory.IsActiveSkill(skillName) == 2;
    }


    private bool IsMaxLevel(Enums.SkillName skillName)
    {
        if (skillName == Enums.SkillName.Ring) return SkillLevel(skillName) >= 2;
        return IsActiveSkill(skillName) ? SkillLevel(skillName) >= 6 : SkillLevel(skillName) >= 5;
    }
}
