using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSkills : MonoBehaviour
{
    private LevelUp_Panel levelUp_Panel;
    private Banish_Panel banish_Panel;
    public Enums.SkillName m_SkillName;
    public Button m_BTN;
    public Image m_Icon;

    private void Awake()
    {
        if (levelUp_Panel == null) levelUp_Panel =
        UI_Manager.Instance.panel_Dic["LevelUp_Panel"].GetComponent<LevelUp_Panel>();

        if (banish_Panel == null) banish_Panel =
        UI_Manager.Instance.panel_Dic["Banish_Panel"].GetComponent<Banish_Panel>();

        if (m_BTN == null) m_BTN = GetComponent<Button>();
        m_BTN.onClick.AddListener(RemoveSkill);
    }

    private void RemoveSkill()
    {
        if (DataManager.Instance.BTS.Banish == 0) return;
        DataManager.Instance.BTS.Banish--;
        levelUp_Panel.UpdateBanishCnt();
        Destroy(this.gameObject);
        Debug.Log($"{m_SkillName} 지움");
    }

    private void OnDestroy()
    {
        banish_Panel.MakeNewCell();
    }
}
