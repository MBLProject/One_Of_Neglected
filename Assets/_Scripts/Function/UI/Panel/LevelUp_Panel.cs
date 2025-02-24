using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRan = UnityEngine.Random;

[Serializable]
public struct Skill_Info
{
    public Enums.SkillName skill_Name;
    public string display_Name;
    [Multiline(4)]
    public string skill_Text;
    public Sprite skill_Sprite;
    public float selectedTime;
}

public class LevelUp_Panel : Panel
{
    [SerializeField] InGameUI_Panel inGameUI_Panel;
    [SerializeField] private List<Selection> current_Selections;
    [SerializeField] private Augment_Info aug_Infos = new();
    [SerializeField] private List<Skill_Info> skill_Infos;
    [SerializeField] private TextMeshProUGUI reroll_Counter_TMP;
    [SerializeField] private TextMeshProUGUI banish_Counter_TMP;
    public Dictionary<Enums.SkillName, Skill_Info> skill_Info_Dic = new Dictionary<Enums.SkillName, Skill_Info>();
    private int augUpCount = 0;
    public bool isAugSelected = false;

    private void Awake()
    {

        if (skill_Infos == null) { skill_Infos = new List<Skill_Info>(); }

        skill_Info_Dic = new Dictionary<Enums.SkillName, Skill_Info>();

        foreach (Skill_Info skill_Info in skill_Infos)
        {
            skill_Info_Dic.Add(skill_Info.skill_Name, skill_Info);
        }

        if (buttons != null && buttons.Count >= 2)
        {
            buttons[0].onClick.AddListener(Reroll_BTN);
            buttons[1].onClick.AddListener(Banish_BTN);
        }

        if (reroll_Counter_TMP != null && DataManager.Instance?.BTS != null)
        {
            reroll_Counter_TMP.text = DataManager.Instance.BTS.Reroll.ToString();
        }

        if (banish_Counter_TMP != null && DataManager.Instance?.BTS != null)
        {
            banish_Counter_TMP.text = DataManager.Instance.BTS.Banish.ToString();
        }
        Debug.LogWarning("AWAKE");
        Debug.Log(DataManager.Instance.classSelect_Type);
        switch (DataManager.Instance.classSelect_Type)
        {
            case Enums.ClassType.Warrior:
                aug_Infos.WarroirInit();
                Debug.LogWarning("전사설정");
                break;
            case Enums.ClassType.Archer:
                aug_Infos.ArcherInit();
                Debug.LogWarning("궁수설정");
                break;
            case Enums.ClassType.Magician:
                aug_Infos.MagicianInit();
                Debug.LogWarning("마법사설정");
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name != "Game") return;
        Time.timeScale = 0;
        inGameUI_Panel.display_Level_TMP.text =
        "Lv." + UnitManager.Instance.GetPlayer().Stats.CurrentLevel.ToString();

        if (UnitManager.Instance.GetPlayer().Stats.CurrentLevel != 0 &&
        UnitManager.Instance.GetPlayer().Stats.CurrentLevel % 10 == 0)
        {
            //증강과 특성 선택하는 메서드
            AugSelections();
            Debug.Log("증강");
        }
        else
        {
            //특성만 선택
            ChangeSelections();
        }

    }
    private void OnDisable()
    {
        if (UnitManager.Instance != null)
        {
            UnitManager.Instance.ResumeGame();
            Time.timeScale = 1;
        }
    }
    private void Banish_BTN()
    {
        Debug.Log("배니쉬");
        SelectionOnOff(false);
        UI_Manager.Instance.panel_Dic["Banish_Panel"].PanelOpen();
    }
    private void Reroll_BTN()
    {
        Debug.Log("리롤");
        if (DataManager.Instance.BTS.Reroll > 0)
        {
            ChangeSelections();
            DataManager.Instance.BTS.Reroll--;
            reroll_Counter_TMP.text = DataManager.Instance.BTS.Reroll.ToString();
        }
    }
    private void ChangeSelections()
    {
        if (current_Selections[3].gameObject.activeSelf)
            current_Selections[3].gameObject.SetActive(false);
        for (int i = 1; i < 3; i++)
        {
            if (current_Selections[i].gameObject.activeSelf == false)
                current_Selections[i].gameObject.SetActive(true);
            else break;
        }
        List<Enums.SkillName> popSkill_List = inGameUI_Panel.skillSelector.SelectSkills();
        Skill_Info skill_Info;
        for (int i = 0; i < popSkill_List.Count; i++)
        {
            skill_Info = skill_Info_Dic[popSkill_List[i]];

            current_Selections[i].m_skillName = skill_Info.skill_Name;
            current_Selections[i].display_Name.text = skill_Info.display_Name;
            current_Selections[i].info_TMP.text = skill_Info.skill_Text;
            current_Selections[i].icon_IMG.sprite = skill_Info.skill_Sprite;
        }
    }
    private void AugSelections()
    {

        // Player player = UnitManager.Instance.GetPlayer();

        // List<Enums.AugmentName> popAgu_List = player.augment.SelectAugments();
        if (isAugSelected == false)
        {
            current_Selections[3].gameObject.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                current_Selections[i].m_augType = aug_Infos.aug_Type[i];
                current_Selections[i].display_Name.text = aug_Infos.aug_Name[i];
                current_Selections[i].info_TMP.text = aug_Infos.aug_Text[i];
                current_Selections[i].icon_IMG.sprite = aug_Infos.aug_Icon[i];
            }
        }
        else
        {
            for (int i = 1; i < 4; i++)
            {
                current_Selections[i].gameObject.SetActive(false);
            }
            current_Selections[0].m_augType = aug_Infos.aug_Type[0];
            Debug.Log($"Selection : {current_Selections[0].m_augType}");
            Debug.Log($"augInfo : {aug_Infos.aug_Type[0]}");
            current_Selections[0].display_Name.text = aug_Infos.aug_Name[augUpCount];
            current_Selections[0].info_TMP.text = aug_Infos.aug_Text[augUpCount];
            current_Selections[0].icon_IMG.sprite = aug_Infos.aug_Icon[0];
            augUpCount++;
        }

    }
    public void SelectionOnOff(bool On)
    {
        foreach (Selection selection in current_Selections)
        {
            selection.m_BTN.interactable = On;
        }
        buttons[0].interactable = On;
        buttons[0].interactable = On;
    }
    public void UpdateBanishCnt()
    {
        banish_Counter_TMP.text = DataManager.Instance.BTS.Banish.ToString();
    }

    public void SetAugTextInit(Enums.AugmentName augmentName)
    {
        if (isAugSelected) return;
        aug_Infos.aug_Type.Clear();
        aug_Infos.aug_Name.Clear();
        aug_Infos.aug_Text.Clear();
        aug_Infos.aug_Icon.Clear();
        switch (augmentName)
        {
            case Enums.AugmentName.TwoHandSword:
                aug_Infos.Two_Hand_Sword();
                isAugSelected = true;
                break;
            case Enums.AugmentName.BigSword:
                aug_Infos.Big_Sword();
                isAugSelected = true;
                break;
            case Enums.AugmentName.SwordShield:
                aug_Infos.Sword_Shield();
                isAugSelected = true;
                break;
            case Enums.AugmentName.Shielder:
                aug_Infos.Shielder();
                isAugSelected = true;
                break;
            case Enums.AugmentName.LongBow:
                aug_Infos.Long_Bow();
                isAugSelected = true;
                break;
            case Enums.AugmentName.CrossBow:
                aug_Infos.Cross_Bow();
                isAugSelected = true;
                break;
            case Enums.AugmentName.GreatBow:
                aug_Infos.Great_Bow();
                isAugSelected = true;
                break;
            case Enums.AugmentName.ArcRanger:
                aug_Infos.Arc_Ranger();
                isAugSelected = true;
                break;
            case Enums.AugmentName.Staff:
                aug_Infos.Staff();
                isAugSelected = true;
                break;
            case Enums.AugmentName.Wand:
                aug_Infos.Wand();
                isAugSelected = true;
                break;
            case Enums.AugmentName.Orb:
                aug_Infos.Orb();
                isAugSelected = true;
                break;
            case Enums.AugmentName.Warlock:
                aug_Infos.Warlock();
                isAugSelected = true;
                break;
        }

    }
}
