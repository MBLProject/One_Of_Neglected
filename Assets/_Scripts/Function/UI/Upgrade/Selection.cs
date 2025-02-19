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

    void OnDisable()
    {

    }

    private void Select_BTN()
    {
        Debug.Log(m_skillName);
        if (inGameUI_Panel.skillContainer.
        GetSkill(m_skillName) == Enums.SkillName.None)
        {
            inGameUI_Panel.SetSkill_Icon(m_skillName);
        }
        inGameUI_Panel.skillSelector.ChooseSkill(m_skillName);
        banish_Panel.FindEmptySlot(m_skillName);
        levelUp_Panel.PanelClose();
    }
}