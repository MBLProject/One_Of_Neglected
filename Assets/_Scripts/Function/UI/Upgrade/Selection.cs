using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    public Button m_BTN;
    public TextMeshProUGUI displayName_TMP;
    public TextMeshProUGUI info_TMP;
    public Image icon_IMG;
    public Action<Enums.SkillName> selectSkill;

    void OnDisable()
    {

    }
}
