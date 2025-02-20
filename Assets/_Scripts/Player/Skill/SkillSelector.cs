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

        if (skillName == Enums.SkillName.None)
        {
            skillDispenser.RegisterSkill(chosenAbility, 2f);

            skillContainer.AddSkill(chosenAbility);
        }
        else
        {
            skillDispenser.RegisterSkill(skillName, 2f);
        }
    }

    public void DeductSkill(Enums.SkillName deDuctSkillName)
    {
        if (skillDispenser.skills.ContainsKey(deDuctSkillName))
        {
            skillDispenser.UnRegisterSkill(deDuctSkillName);

            skillContainer.RemoveSkill(deDuctSkillName);
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
