using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI_Panel : Panel
{

    private bool isOptionActive;
    private int mainSkill_Num;
    private int subSkill_Num;
    [SerializeField] private LevelUp_Panel levelUp_Panel;
    [SerializeField] private List<Image> mainSkills_List;
    [SerializeField] public List<Image> subSkills_List;

    private void Update()
    {
        //TODO : 키코드 입력받는거 몰아넣기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOptionActive = UI_Manager.Instance.panel_Dic["Option_Panel"].gameObject.activeSelf;
            if (isOptionActive == false)
            {

                UI_Manager.Instance.panel_Dic["Option_Panel"].PanelOpen();
                return;
            }
            if (isOptionActive)
            {
                UI_Manager.Instance.panel_Dic["Option_Panel"].PanelClose();
                return;
            }
        }
    }
    public void SetSkill_Icon(Enums.SkillName skillName)
    {
        bool isActiveSkill = SkillFactory.IsActiveSkill(skillName);
        Debug.Log(isActiveSkill);
        if (isActiveSkill)
        {
            mainSkills_List[mainSkill_Num].sprite = levelUp_Panel.skill_Info_Dic[skillName].skill_Sprite;
            mainSkills_List[mainSkill_Num].color = Color.white;
            //TODO : 강화될 경우 스프라이트 업뎃 안하게
            mainSkill_Num++;
        }
        else
        {
            // subSkills_List[subSkill_Num].sprite =    
            subSkill_Num++;
        }
    }
}
