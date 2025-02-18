using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRan = UnityEngine.Random;
[Serializable]
public struct Augment_Info
{
    //TODO : Enum 추가예정
    public string display_Name;
    public Sprite augment_Sprite;
    [Multiline(4)]
    public string augment_Text;
}
[Serializable]
public struct Skill_Info
{
    public Enums.SkillName skill_Name;
    public string display_Name;
    public Sprite skill_Sprite;
    [Multiline(4)]
    public string skill_Text;
}

public class Augment_Panel : Panel
{
    public List<Augment> current_Augments;
    public List<Augment_Info> augment_Infos;
    public List<Skill_Info> skill_Infos;
    public RectTransform banish_List;
    public Dictionary<Enums.SkillName, Augment_Info> augment_Info_Dic = new Dictionary<Enums.SkillName, Augment_Info>();
    private void Awake()
    {
        buttons[0].onClick.AddListener(Reroll_BTN);
        buttons[1].onClick.AddListener(Banish_BTN);
        buttons[2].onClick.AddListener(Return_BTN);
    }

    private void OnEnable()
    {
        //TODO :플레이어 레벨 가져와서 증강 및 특성 넣어주기
        if (UnitManager.Instance.GetPlayer().Stats.CurrentLevel % 10 == 0)
        {
            //증강과 특성 선택하는 메서드
            OnLevelUp(true);
        }
        else
        {
            //특성만 선택
            OnLevelUp(false);
        }
    }

    private void Return_BTN()
    {
        banish_List.gameObject.SetActive(false);
        buttons[0].interactable = true;
        buttons[1].interactable = true;
    }

    private void Banish_BTN()
    {
        Debug.Log("배니쉬");
        foreach (Augment augment in current_Augments)
        {
            augment.m_BTN.interactable = false;
        }
        buttons[0].interactable = false;
        buttons[1].interactable = false;

        banish_List.gameObject.SetActive(true);
    }

    private void Reroll_BTN()
    {
        Debug.Log("리롤");
    }

    private void OnLevelUp(bool selectAugment = false)
    {
        if (selectAugment)
        {
            //증강뽑기
        }
        else
        {
            //특성뽑기
        }
    }

    private void FindSkillInfo()
    {

    }

    private void FindAugmentInfo()
    {

    }

}
