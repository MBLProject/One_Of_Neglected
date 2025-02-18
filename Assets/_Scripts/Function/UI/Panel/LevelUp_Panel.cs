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
    [SerializeField] private SkillSelector skillSelector;
    [SerializeField] private SkillContainer skillContainer;
    [SerializeField] private SkillDispenser skillDispenser;
    public List<Selection> current_Selections;
    public List<Augment_Info> augment_Infos;
    public List<Skill_Info> skill_Infos;

    public RectTransform banish_Panel;
    public Dictionary<Enums.SkillName, Skill_Info> skill_Info_Dic = new Dictionary<Enums.SkillName, Skill_Info>();
    private void Awake()
    {
        if (skillSelector == null) skillSelector = GetComponent<SkillSelector>();
        Debug.Log(skill_Info_Dic.Count);
        foreach (Skill_Info skill_Info in skill_Infos)
        {
            skill_Info_Dic.Add(skill_Info.skill_Name, skill_Info);
        }
        buttons[0].onClick.AddListener(Reroll_BTN);
        buttons[1].onClick.AddListener(Banish_BTN);
        buttons[2].onClick.AddListener(Return_BTN);
        skillSelector.Initialize(skillContainer, skillDispenser);
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
            List<Enums.SkillName> popSkill_List =
            skillSelector.SelectSkills();
            Skill_Info skill_Info;
            for (int i = 0; i < popSkill_List.Count; i++)
            {
                Debug.Log($"뽑힌 스킬 : {popSkill_List[i]}");
                skill_Info = skill_Info_Dic[popSkill_List[i]];

                current_Selections[i].
                displayName_TMP.text = skill_Info.display_Name;

                current_Selections[i].
                info_TMP.text = skill_Info.skill_Text;

                current_Selections[i].
                icon_IMG.sprite = skill_Info.skill_Sprite;

            }
        }
    }

    private void Return_BTN()
    {
        banish_Panel.gameObject.SetActive(false);
        buttons[0].interactable = true;
        buttons[1].interactable = true;
    }

    private void Banish_BTN()
    {
        Debug.Log("배니쉬");
        foreach (Selection augment in current_Selections)
        {
            augment.m_BTN.interactable = false;
        }
        buttons[0].interactable = false;
        buttons[1].interactable = false;

        banish_Panel.gameObject.SetActive(true);
    }

    private void Reroll_BTN()
    {
        Debug.Log("리롤");
    }

    private void FindSkillInfo()
    {

    }

    private void FindAugmentInfo()
    {

    }

}
