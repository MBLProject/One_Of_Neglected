using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRan = UnityEngine.Random;
[Serializable]
public class Augment_Info
{

    public Enums.AugmentName aug_Name;
    public List<string> display_Name = new List<string>();
    [Multiline(4)]
    public List<string> augment_Text = new List<string>();
    public List<Sprite> augment_Sprite = new List<Sprite>();
    public float selectedTime;
    //0 ~ MAX
    private int augsLevel = 4;

    #region 워리어
    public void Two_Hand_Sword()
    {

        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("검기[Lv.1]");
                    augment_Text.Add("전방에 검기를 날립니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("검기[Lv.2]");
                    augment_Text.Add("검기의 크기가 증가합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("검기[Lv.3]");
                    augment_Text.Add("검기를 두 방향으로 날립니다.");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("검기[Lv.4]");
                    augment_Text.Add("검기를 더 빨리 사용합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("검기[Lv.5]");
                    augment_Text.Add("검기를 세방향으로 날려보내며, 검기가 적의 투사체를 막아냅니다.");
                    augment_Sprite.Add(null);
                    break;
            }
        }

    }
    public void Big_Sword()
    {
        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("지진[Lv.1]");
                    augment_Text.Add("캐릭터를 중심으로 땅을 강하게 내리쳐 피해를 입힙니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("지진[Lv.2]");
                    augment_Text.Add("지진의 범위가 증가합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("지진[Lv.3]");
                    augment_Text.Add("지진을 더 빨리 사용합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("지진[Lv.4]");
                    augment_Text.Add("지진의 강도가 증가합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("지진[Lv.5]");
                    augment_Text.Add("지진의 범위가 더욱 넓어지며, 여진이 추가됩니다.");
                    augment_Sprite.Add(null);
                    break;
            }
        }
    }

    public void Sword_Shield()
    {

        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("패링[Lv.1]");
                    augment_Text.Add("적의 공격을 방어하며, 투사체라면 투사체를 튕겨내고 맞은 적에게 데미지를 가합니다. 사용 후 일정시간이 지나야 재사용 가능합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("패링[Lv.2]");
                    augment_Text.Add("조금 더 빨리 방어 태세에 들어갑니다.");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("패링[Lv.3]");
                    augment_Text.Add("방어 시 잠시간 피해를 받지 않으며, 이동 속도가 증가합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("패링[Lv.4]");
                    augment_Text.Add("방패가 조금 더 두꺼워집니다.");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("패링[Lv.5]");
                    augment_Text.Add("피해를 받지 않는 시간이 증가하며, 이동 속도가 큰폭으로 증가합니다.");
                    augment_Sprite.Add(null);
                    break;
            }
        }
    }
    public void Shielder()
    {
        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("돌진[Lv.1]");
                    augment_Text.Add("캐릭터가 받는 피해가 감소하며, 대쉬 사용 시 전방으로 돌진하여 경로에 있는 적에게 피해를 가합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("돌진[Lv.2]");
                    augment_Text.Add("대쉬를 조금 더 자주 사용할 수 있습니다.");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("돌진[Lv.3]");
                    augment_Text.Add("방패가 조금 더 단단해지며, 대쉬 회수가 증가합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("돌진[Lv.4]");
                    augment_Text.Add("대쉬를 더 자주 사용할 수 있습니다.");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("돌진[Lv.5]");
                    augment_Text.Add("검기가 적의 투사체를 막아냅니다.");
                    augment_Sprite.Add(null);
                    break;
            }
        }
    }
    #endregion
    #region 아처
    public void Long_Bow()
    {

        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("기본기[Lv.1]");
                    augment_Text.Add("기본 공격 시, 투사체가 1개 추가됩니다. 기본 공격에 사거리 제한이 사라집니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("기본기[Lv.2]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("기본기[Lv.3]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("기본기[Lv.4]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("기본기[Lv.5]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
            }
        }
    }
    public void Cross_Bow()
    {
        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("속사[Lv.1]");
                    augment_Text.Add("전방에 빠르게 화살을 쏘아냅니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("속사[Lv.2]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("속사[Lv.3]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("속사[Lv.4]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("속사[Lv.5]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
            }
        }
    }
    public void Great_Bow()
    {
        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("관통[Lv.1]");
                    augment_Text.Add("전방에 강력한 화살을 발사합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("관통[Lv.2]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("관통[Lv.3]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("관통[Lv.4]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("관통[Lv.5]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
            }
        }
    }
    public void Arc_Ranger()
    {
        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("테크닉[Lv.1]");
                    augment_Text.Add("대쉬 사용 시, 전방의 부채꼴 방면으로 화살을 퍼트리듯 발사하며 이동합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("테크닉[Lv.2]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("테크닉[Lv.3]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("테크닉[Lv.4]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("테크닉[Lv.5]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
            }
        }
    }
    #endregion
    #region 매지션
    public void Staff()
    {
        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("파워[Lv.1]");
                    augment_Text.Add("필드의 모든 몬스터를 대상으로 강한 공격을 가합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("파워[Lv.2]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("파워[Lv.3]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("파워[Lv.4]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("파워[Lv.5]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
            }
        }
    }
    public void Wand()
    {
        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("캐스팅[Lv.1]");
                    augment_Text.Add("메인 특성의 쿨타임을 주기적으로 초기화합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("캐스팅[Lv.2]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("캐스팅[Lv.3]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("캐스팅[Lv.4]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("캐스팅[Lv.5]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
            }
        }
    }
    public void Orb()
    {
        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("쥬얼[Lv.1]");
                    augment_Text.Add("자기를 중심으로 일정 범위 내 피해를 주는 화염구를 소환합니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("쥬얼[Lv.2]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("쥬얼[Lv.3]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("쥬얼[Lv.4]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("쥬얼[Lv.5]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
            }
        }
    }
    public void Warlock()
    {
        for (int i = 0; i < augsLevel; i++)
        {
            switch (i)
            {
                case 0:
                    display_Name.Add("룬워드[Lv.1]");
                    augment_Text.Add("대쉬가 텔레포트로 변경됩니다.");
                    augment_Sprite.Add(null);
                    break;
                case 1:
                    display_Name.Add("룬워드[Lv.2]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 2:
                    display_Name.Add("룬워드[Lv.3]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 3:
                    display_Name.Add("룬워드[Lv.4]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
                case 4:
                    display_Name.Add("룬워드[Lv.5]");
                    augment_Text.Add("");
                    augment_Sprite.Add(null);
                    break;
            }
        }
    }
    #endregion
}
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
    [SerializeField] private Augment_Info augment_Infos;
    [SerializeField] private List<Skill_Info> skill_Infos;
    [SerializeField] private TextMeshProUGUI reroll_Counter_TMP;
    [SerializeField] private TextMeshProUGUI banish_Counter_TMP;
    public Dictionary<Enums.SkillName, Skill_Info> skill_Info_Dic = new Dictionary<Enums.SkillName, Skill_Info>();
    // public Dictionary<Enums.AugmentName, Augment_Info> aug_Info_Dic = new Dictionary<Enums.AugmentName, Augment_Info>();
    private int augmentUpCount = 0;
    private void Awake()
    {

        if (skill_Infos == null) { skill_Infos = new List<Skill_Info>(); }

        skill_Info_Dic = new Dictionary<Enums.SkillName, Skill_Info>();
        // aug_Info_Dic = new Dictionary<Enums.AugmentName, Augment_Info>();

        foreach (Skill_Info skill_Info in skill_Infos)
        {
            skill_Info_Dic.Add(skill_Info.skill_Name, skill_Info);
        }

        // foreach (Augment_Info aug_Info in augment_Infos)
        // {
        //     aug_Info_Dic.Add(aug_Info.aug_Name, aug_Info);
        // }

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
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name != "Game") return;

        UnitManager.Instance.PauseGame();
        Time.timeScale = 0;
        inGameUI_Panel.display_Level_TMP.text =
        "Lv." + UnitManager.Instance.GetPlayer().Stats.CurrentLevel.ToString();

        if (UnitManager.Instance.GetPlayer().Stats.CurrentLevel != 0 && UnitManager.Instance.GetPlayer().Stats.CurrentLevel % 10 == 0)
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
        current_Selections[3].gameObject.SetActive(false);
        List<Enums.SkillName> popSkill_List = inGameUI_Panel.skillSelector.SelectSkills();
        Skill_Info skill_Info;
        for (int i = 0; i < popSkill_List.Count; i++)
        {
            Debug.Log($"뽑힌 스킬 : {popSkill_List[i]}");
            skill_Info = skill_Info_Dic[popSkill_List[i]];

            current_Selections[i].m_skillName = skill_Info.skill_Name;
            current_Selections[i].displayName_TMP.text = skill_Info.display_Name;
            current_Selections[i].info_TMP.text = skill_Info.skill_Text;
            current_Selections[i].icon_IMG.sprite = skill_Info.skill_Sprite;
        }
    }
    private void AugSelections()
    {
        current_Selections[3].gameObject.SetActive(true);
        Player player = UnitManager.Instance.GetPlayer();

        List<Enums.AugmentName> popAgu_List = player.augment.SelectAugments();

        // Augment_Info aug_Info;
        for (int i = 0; i < popAgu_List.Count; i++)
        {
            // aug_Info = aug_Info_Dic[popAgu_List[i]];

            // current_Selections[i].m_augName = aug_Info.aug_Name;
            // current_Selections[i].displayName_TMP.text = aug_Info.display_Name;

            // if (augmentUpCount > 0) aug_Info.augment_Text = m_AugText[augmentUpCount];
            // current_Selections[i].info_TMP.text = aug_Info.augment_Text;
            // current_Selections[i].icon_IMG.sprite = aug_Info.augment_Sprite;
        }
        augmentUpCount++;
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
        // if (augmentUpCount > 1) return;
        // switch (augmentName)
        // {
        //     case Enums.AugmentName.TwoHandSword:
        //         augment_Infos.Two_Hand_Sword();
        //         break;
        //     case Enums.AugmentName.BigSword:
        //         augText_Table.Big_Sword(ref m_AugText);
        //         break;
        //     case Enums.AugmentName.SwordShield:
        //         augText_Table.Sword_Shield(ref m_AugText);
        //         break;
        //     case Enums.AugmentName.Shielder:
        //         augText_Table.Shielder(ref m_AugText);
        //         break;
        //     case Enums.AugmentName.LongBow:
        //         augText_Table.Long_Bow(ref m_AugText);
        //         break;
        //     case Enums.AugmentName.CrossBow:
        //         augText_Table.Cross_Bow(ref m_AugText);
        //         break;
        //     case Enums.AugmentName.GreatBow:
        //         augText_Table.Great_Bow(ref m_AugText);
        //         break;
        //     case Enums.AugmentName.ArcRanger:
        //         augText_Table.Arc_Ranger(ref m_AugText);
        //         break;
        //     case Enums.AugmentName.Staff:
        //         augText_Table.Staff(ref m_AugText);
        //         break;
        //     case Enums.AugmentName.Wand:
        //         augText_Table.Wand(ref m_AugText);
        //         break;
        //     case Enums.AugmentName.Orb:
        //         augText_Table.Orb(ref m_AugText);
        //         break;
        //     case Enums.AugmentName.Warlock:
        //         augText_Table.Warlock(ref m_AugText);
        //         break;

        // }
        // foreach (string a in m_AugText)
        // {
        //     Debug.Log(a);
        // }
    }
}
