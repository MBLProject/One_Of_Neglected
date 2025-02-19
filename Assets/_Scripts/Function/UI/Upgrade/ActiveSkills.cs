using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSkills : MonoBehaviour
{
    public Enums.SkillName m_SkillName;
    public Button m_BTN;
    public Image m_Icon;

    private void Awake()
    {
        if (m_BTN == null) m_BTN = GetComponent<Button>();
        m_BTN.onClick.AddListener(RemoveSkill);
    }

    private void RemoveSkill()
    {
        if (DataManager.Instance.BTS.Banish == 0) return;
        DataManager.Instance.BTS.Banish--;
        Debug.Log($"{m_SkillName} 지움");
    }
}
