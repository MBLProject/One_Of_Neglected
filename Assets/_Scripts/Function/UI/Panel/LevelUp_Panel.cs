using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRan = UnityEngine.Random;
[Serializable]
public struct Augment_Info
{
    //TODO : Enum 추가예정
    public string display_Name;
    [Multiline(4)]
    public string augment_Text;
    public Sprite augment_Sprite;

}
[Serializable]
public struct Skill_Info
{
    public Enums.SkillName skill_Name;
    public string display_Name;
    [Multiline(4)]
    public string skill_Text;
    public Sprite skill_Sprite;

}

public class LevelUp_Panel : Panel
{
    [SerializeField] InGameUI_Panel inGameUI_Panel;
    [SerializeField] private List<Selection> current_Selections;
    [SerializeField] private List<Augment_Info> augment_Infos;
    [SerializeField] private List<Skill_Info> skill_Infos;
    [SerializeField] private TextMeshProUGUI reroll_Counter_TMP;
    [SerializeField] private TextMeshProUGUI banish_Counter_TMP;
    public Dictionary<Enums.SkillName, Skill_Info> skill_Info_Dic = new Dictionary<Enums.SkillName, Skill_Info>();
    private void Awake()
    {
        Debug.Log(skill_Info_Dic.Count);
        foreach (Skill_Info skill_Info in skill_Infos)
        {
            skill_Info_Dic.Add(skill_Info.skill_Name, skill_Info);
        }
        buttons[0].onClick.AddListener(Reroll_BTN);
        buttons[1].onClick.AddListener(Banish_BTN);

        reroll_Counter_TMP.text = DataManager.Instance.BTS.Reroll.ToString();
        banish_Counter_TMP.text = DataManager.Instance.BTS.Banish.ToString();
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name != "Game") return;
        Debug.Log("OnEnable");
        Debug.Log($"LV {UnitManager.Instance.GetPlayer().Stats.CurrentLevel}");
        //TODO :플레이어 레벨 가져와서 증강 및 특성 넣어주기
        if (UnitManager.Instance.GetPlayer().Stats.CurrentLevel != 0 && UnitManager.Instance.GetPlayer().Stats.CurrentLevel % 10 == 0)
        {
            //증강과 특성 선택하는 메서드
        }
        else
        {
            //특성만 선택
            ChangeSelections();
        }
    }

    private void Banish_BTN()
    {
        Debug.Log("배니쉬");
        SelectionOnOff(false);
        UI_Manager.Instance.panel_Dic["Banish_Panel"].PanelOpen();
    }

    private void Reroll_BTN()
    {
        Debug.Log("리롤");
        if (DataManager.Instance.BTS.Reroll > 0)
        {
            ChangeSelections();
            DataManager.Instance.BTS.Reroll--;
            reroll_Counter_TMP.text = DataManager.Instance.BTS.Reroll.ToString();
        }
    }

    private void ChangeSelections()
    {
        List<Enums.SkillName> popSkill_List = inGameUI_Panel.skillSelector.SelectSkills();
        Skill_Info skill_Info;
        for (int i = 0; i < popSkill_List.Count; i++)
        {
            Debug.Log($"뽑힌 스킬 : {popSkill_List[i]}");
            skill_Info = skill_Info_Dic[popSkill_List[i]];

            current_Selections[i].m_skillName = skill_Info.skill_Name;
            current_Selections[i].displayName_TMP.text = skill_Info.display_Name;
            current_Selections[i].info_TMP.text = skill_Info.skill_Text;
            current_Selections[i].icon_IMG.sprite = skill_Info.skill_Sprite;
        }

    }

    private void FindAugmentInfo()
    {

    }

    public void SelectionOnOff(bool On)
    {
        foreach (Selection selection in current_Selections)
        {
            selection.m_BTN.interactable = On;
        }
        buttons[0].interactable = On;
        buttons[0].interactable = On;
    }

}
