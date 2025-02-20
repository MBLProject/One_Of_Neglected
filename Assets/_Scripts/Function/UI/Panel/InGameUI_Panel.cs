using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InGameUI_Panel : Panel
{
    private Player player;
    private bool isOptionActive;
    private int min;
    private int sec;
    [SerializeField] private LevelUp_Panel levelUp_Panel;
    [SerializeField] private List<Image> mainSkill_Icon_Container;
    [SerializeField] private List<Image> subSkill_Icon_Container;
    [SerializeField] private TextMeshProUGUI display_Time_TMP;
    [SerializeField] private TextMeshProUGUI display_Level_TMP;
    [SerializeField] private RectTransform main_Icon_Rect;
    [SerializeField] private RectTransform sub_Icon_Rect;
    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private Slider expSlider;
    public SkillSelector skillSelector;
    public SkillContainer skillContainer;

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

    private void OnEnable()
    {

        player = UnitManager.Instance.GetPlayer();
    }

    private void Update()
    {

        expSlider.value = (float)player.Stats.currentExp / player.Stats.CurrentMaxExp;

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
    public void SetIconCell_Banish(Enums.SkillName skillName)
    {

        if (SkillFactory.IsActiveSkill(skillName))
        {
            SetIcons(mainSkill_Icon_Container, skillName);
        }
        else
        {
            SetIcons(subSkill_Icon_Container, skillName);
        }

    }
    public void SetIcons(List<Image> skill_List, Enums.SkillName skillName)
    {
        foreach (Image image in skill_List)
        {
            if (image.sprite == null)
            {
                image.sprite = levelUp_Panel.skill_Info_Dic[skillName].skill_Sprite;
                image.color = Color.white;
                break;
            }
        }
    }
    public void Remove_MiniIcon(Enums.SkillName skillName, Sprite sprite)
    {
        if (SkillFactory.IsActiveSkill(skillName))
        {
            Icon_Replace(mainSkill_Icon_Container, sprite, main_Icon_Rect);
        }
        else
        {
            Icon_Replace(subSkill_Icon_Container, sprite, sub_Icon_Rect);
        }
    }

    public void Icon_Replace(List<Image> icon_Container, Sprite sprite, RectTransform parent_Rect)
    {
        foreach (Image image in icon_Container)
        {
            if (image.sprite == sprite)
            {
                image.sprite = defaultIcon;
                image.transform.parent.SetParent(null);
                image.transform.parent.SetParent(parent_Rect);
            }
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
