using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InGameUI_Panel : Panel
{
    public SkillSelector skillSelector;
    [SerializeField] private SkillContainer skillContainer;
    private bool isOptionActive;
    private int mainSkill_Num;
    private int subSkill_Num;
    private int min;
    private int sec;
    [SerializeField] private LevelUp_Panel levelUp_Panel;
    [SerializeField] private List<Image> mainSkills_List;
    [SerializeField] private List<Image> subSkills_List;
    [SerializeField] private TextMeshProUGUI display_Time_TMP;
    [SerializeField] private TextMeshProUGUI display_Level_TMP;

    private void Awake()
    {
        buttons[0].onClick.AddListener(Auto_BTN);
    }

    private void Start()
    {
        if (skillSelector == null) skillSelector = GetComponent<SkillSelector>();
        Debug.Log(UnitManager.Instance.GetPlayer().GetComponent<SkillDispenser>());
        skillSelector.Initialize(skillContainer, UnitManager.Instance.GetPlayer().GetComponent<SkillDispenser>());
        display_Level_TMP.text = "Lv." + UnitManager.Instance.GetPlayer().statViewer.Level;
    }
    private void Update()
    {
        //TODO : 키코드 입력받는거 몰아넣기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOptionActive = UI_Manager.Instance.panel_Dic["Option_Panel"].gameObject.activeSelf;
            if (isOptionActive == false)
            {

                UI_Manager.Instance.panel_Dic["Option_Panel"].PanelOpen();
                UnitManager.Instance.PauseGame();
                Time.timeScale = 0;
                return;
            }
            if (isOptionActive)
            {
                UI_Manager.Instance.panel_Dic["Option_Panel"].PanelClose();
                Time.timeScale = 1;
                UnitManager.Instance.ResumeGame();
                return;
            }
        }
        TimeCalc();
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

    private void TimeCalc()
    {
        min = (int)TimeManager.Instance.gameTime / 60;
        sec = (int)TimeManager.Instance.gameTime % 60;
        display_Time_TMP.text = "Time : " + min.ToString("00") + " : " + sec.ToString("00");
    }

    private void Auto_BTN()
    {
        UnitManager.Instance.GetPlayer().isAuto = !UnitManager.Instance.GetPlayer().isAuto;
    }
}
