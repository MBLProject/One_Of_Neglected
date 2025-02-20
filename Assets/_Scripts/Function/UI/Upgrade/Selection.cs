using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    [SerializeField] private Banish_Panel banish_Panel;
    [SerializeField] private InGameUI_Panel inGameUI_Panel;
    [SerializeField] private LevelUp_Panel levelUp_Panel;
    public Enums.SkillName m_skillName;
    public Button m_BTN;
    public TextMeshProUGUI displayName_TMP;
    public TextMeshProUGUI info_TMP;
    public Image icon_IMG;

    private void Awake()
    {
        m_BTN.onClick.AddListener(Select_BTN);
    }

    private void Select_BTN()
    {
        Debug.Log(m_skillName);
        if (inGameUI_Panel.skillContainer.
        GetSkill(m_skillName) == Enums.SkillName.None)
        {
            //TODO 골드도 예외처리
            if (m_skillName != Enums.SkillName.Cheese)
            {
                inGameUI_Panel.SetIconCell_Banish(m_skillName);
                banish_Panel.SetIconCell_Mini(m_skillName);
            }
        }
        inGameUI_Panel.skillSelector.ChooseSkill(m_skillName);
        levelUp_Panel.PanelClose();
    }
}