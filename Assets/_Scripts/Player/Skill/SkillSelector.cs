using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillSelector : MonoBehaviour
{
    private SkillContainer skillContainer;
    private SkillDispenser skillDispenser;

    public void Initialize(SkillContainer container, SkillDispenser dispenser)
    {
        skillContainer = container;
        skillDispenser = dispenser;
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
                selectedAbilities.Add(skill);
            }
        }
        else if (activeMax)
        {
            foreach (var skill in availableAbilities)
            {
                if (IsActiveSkill(skill))
                {
                    if (skillContainer.GetSkill(skill) != Enums.SkillName.None) selectedAbilities.Add(skill);
                }
                else
                {
                    selectedAbilities.Add(skill);
                }
            }
        }
        else if (passiveMax)
        {
            foreach (var skill in availableAbilities)
            {
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
            selectedAbilities = new List<Enums.SkillName>(availableAbilities);
        }

        return selectedAbilities.OrderBy(x => Random.value).Take(3).ToList();
    }

    public void ChooseSkill(Enums.SkillName chosenAbility)
    {
        Enums.SkillName skillName = skillContainer.GetSkill(chosenAbility);
        if (skillName != Enums.SkillName.None)
        {
            Skill skill = SkillFactory.CreateSkill(skillName, 2f); // SkillName으로 Skill 객체 생성
            skill.InitSkill(2f, 1, 0, 1, 1, 0.1f, 0.5f); // 예시로 초기화
            skillDispenser.RegisterSkill(chosenAbility, skill.cooldown);
            skillContainer.AddSkill(skillName);
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
}
