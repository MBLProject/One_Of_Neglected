using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Banish_Panel : Panel
{
    [SerializeField] private LevelUp_Panel levelUp_Panel;
    [SerializeField] private List<ActiveSkills> activeSkills;
    [SerializeField] private List<PassiveSkills> passiveSkills;
    private void Awake()
    {
        buttons[0].onClick.AddListener(Return_BTN);
    }

    private void Return_BTN()
    {
        UI_Manager.Instance.panel_Dic["Banish_Panel"].PanelClose();
        levelUp_Panel.SelectionOnOff(true);
    }

    public void FindEmptySlot(Enums.SkillName skillName)
    {
        Debug.Log(skillName);
        foreach (ActiveSkills activeSkill in activeSkills)
        {
            if (activeSkill.m_SkillName == Enums.SkillName.None)
            {
                Skill_Info skill_Info =
                levelUp_Panel.skill_Info_Dic[skillName];

                activeSkill.m_SkillName = skill_Info.skill_Name;
                activeSkill.m_Icon.sprite = skill_Info.skill_Sprite;
                activeSkill.m_Icon.color = Color.white;
                activeSkill.m_BTN.interactable = true;
                return;
            }
        }
    }
}
