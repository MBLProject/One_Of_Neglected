using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
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

    void OnDisable()
    {

    }

    private void Select_BTN()
    {
        Debug.Log(m_skillName);
        levelUp_Panel.skillSelector.ChooseSkill(m_skillName);
        inGameUI_Panel.SetSkill_Icon(m_skillName);
        levelUp_Panel.PanelClose();
    }
}