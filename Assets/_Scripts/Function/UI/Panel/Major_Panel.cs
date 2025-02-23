using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Major_Panel : MonoBehaviour
{
    #region 구조체 영역
    [Serializable]
    public struct Base_TMP
    {
        public TextMeshProUGUI time;
        public TextMeshProUGUI killCount;
        public TextMeshProUGUI gold;
        public TextMeshProUGUI remnents;
    }
    [Serializable]
    public struct Augment_TMP
    {
        public TextMeshProUGUI augName;
        public TextMeshProUGUI augLevel;
        public TextMeshProUGUI augDamage;
        public TextMeshProUGUI augTime;
    }

    #endregion
    [Header("Left_Display")]
    public Base_TMP base_TMP;
    [SerializeField] private List<Image> mainSkill_Icons;
    [SerializeField] private List<Image> subSkill_Icons;
    public Image playerIcon;
    [Header("Right_Display")]
    public Augment_TMP augment_TMP;
    public List<SkillMember> skillMembers;
    private void Start()
    {
        playerIcon.sprite = null;
        InGameUI_Panel panel =
        UI_Manager.Instance.panel_Dic["InGameUI_Panel"].GetComponent<InGameUI_Panel>();
        //스킬 아이콘
        for (int i = 0; i < mainSkill_Icons.Count; i++)
        {
            mainSkill_Icons[i].sprite = panel.mainSkill_Icon_Container[i].sprite;
        }
        for (int i = 0; i < subSkill_Icons.Count; i++)
        {
            subSkill_Icons[i].sprite = panel.subSkill_Icon_Container[i].sprite;
        }
        //기본 정보
        base_TMP.time.text = panel.TimeCalc(TimeManager.Instance.gameTime);
        base_TMP.killCount.text = "";
        base_TMP.gold.text = "";
        base_TMP.remnents.text = "";

    }
}
