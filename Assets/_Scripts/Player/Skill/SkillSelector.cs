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
            ChooseSkill(Enums.SkillName.Claw);
    }

    public List<Enums.SkillName> SelectSkills()
    {
        List<Enums.SkillName> availableAbilities = skillContainer.GetAvailableSkills();
        List<Enums.SkillName> selectedAbilities = new List<Enums.SkillName>();

        bool activeMax = !skillContainer.CanAddActiveSkill();
        bool passiveMax = !skillContainer.CanAddPassiveSkill();

        if (activeMax && passiveMax)
        {
            foreach (var skill in availableAbilities)
            {
                if (IsMaxLevel(skill)) continue;

                selectedAbilities.Add(skill);
            }
        }
        else if (activeMax)
        {
            foreach (var skill in availableAbilities)
            {
                if (IsMaxLevel(skill)) continue;

                if (IsActiveSkill(skill))
                    if (skillContainer.GetSkill(skill) != Enums.SkillName.None) selectedAbilities.Add(skill);
                else
                    selectedAbilities.Add(skill);
            }
        }
        else if (passiveMax)
        {
            foreach (var skill in availableAbilities)
            {
                if (IsMaxLevel(skill)) continue;

                if (IsPassiveSkill(skill))
                {
                    if (skillContainer.GetSkill(skill) != Enums.SkillName.None) selectedAbilities.Add(skill);
                }
                else
                {
                    selectedAbilities.Add(skill);
                }
            }
        }
        else
        {
            foreach (var skill in availableAbilities)
            {
                if (IsMaxLevel(skill)) continue;

                selectedAbilities.Add(skill);
            }
        }

        return selectedAbilities.OrderBy(x => Random.value).Take(3).ToList();
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
        return SkillFactory.IsActiveSkill(skillName);
    }

    private bool IsPassiveSkill(Enums.SkillName skillName)
    {
        return !SkillFactory.IsActiveSkill(skillName);
    }

    private bool IsMaxLevel(Enums.SkillName skillName)
    {
        return IsActiveSkill(skillName) ? SkillLevel(skillName) >= 6 : SkillLevel(skillName) >= 5;
    }
}
