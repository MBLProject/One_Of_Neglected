using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [Serializable]
    public struct Skill_TMP
    {
        public TextMeshProUGUI skillName;
        public TextMeshProUGUI level;
        public TextMeshProUGUI damage;
        public TextMeshProUGUI time;
    }
    #endregion

    [SerializeField] private List<Image> mainSkill_Icons;
    [SerializeField] private List<Image> subSkill_Icons;
    [SerializeField] private List<Skill_TMP> skillList = new List<Skill_TMP>();

    public Base_TMP base_TMP;
    public Augment_TMP augment_TMP;

    private void Start()
    {

    }
}
