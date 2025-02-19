using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Enums;

public class AugmentSelector : MonoBehaviour
{
    [SerializeField]private Player owner;
    [SerializeField] private Dictionary<Enums.ClassType, List<Augment>> availableAugments;
    [SerializeField] public  List<Augment> activeAugments = new List<Augment>();
    
    public void Initialize(Player owner)
    {
        this.owner = owner;
        InitializeAugments();
        owner.OnPlayerAttack += OnPlayerAttacked;
        Debug.Log("onplayerattack 등록됨");
    }
    
    private void InitializeAugments()
    {
        availableAugments = new Dictionary<Enums.ClassType, List<Augment>>();
        
        availableAugments[Enums.ClassType.Warrior] = new List<Augment>
        {
            new Aug_TwoHandSword(owner),
            new Aug_BigSword(owner),
            new Aug_SwordShield(owner),
            new Aug_Shielder(owner),
        };

        availableAugments[Enums.ClassType.Archer] = new List<Augment>
        {
            new Aug_LongBow(owner),
            new Aug_CrossBow(owner, 1f),
            new Aug_GreatBow(owner, 1f),
            new Aug_ArcRanger(owner),
        };

        availableAugments[Enums.ClassType.Magician] = new List<Augment>
        {
            new Aug_Staff(owner, 1f),
            new Aug_Wand(owner, 1f),
            new Aug_Orb(owner, 1f),
            new Aug_Warlock(owner),
        };
    }

    private List<Augment> GetAvailableAugments()
    {
        if (availableAugments.TryGetValue(owner.ClassType, out var augments))
        {
            return augments.FindAll(a => !activeAugments.Contains(a));
        }
        return new List<Augment>();
    }
    
    public List<Augment> SelectAugments()
    {
        var availableList = GetAvailableAugments();
        return availableList.ToList();
    }

    public void ChooseAugment(Augment chosenAugment)
    {
        if (!activeAugments.Contains(chosenAugment))
        {
            chosenAugment.Activate();
            activeAugments.Add(chosenAugment);
        }
        else
        {
            chosenAugment.LevelUp();
        }
    }

    public void ChooseAugment2(AugmentName augName)
    {
        var availableList = GetAvailableAugments();
        var selectedAugment = availableList.FirstOrDefault(aug => aug.AugmentName == augName);
        
        if (selectedAugment != null)
        {
            if (!activeAugments.Contains(selectedAugment))
            {
                selectedAugment.Activate();
                activeAugments.Add(selectedAugment);
            }
            else
            {
                selectedAugment.LevelUp();
            }
        }
        else
        {
            Debug.LogWarning($"증강 {augName}을(를) 찾을 수 없습니다.");
        }
    }

    private void Update()
    {
        // Update에서는 시간 기반이나 다른 조건을 체크해야 하는 증강만 체크하도록 수정
        // 공격 기반 증강은 OnPlayerAttacked에서 처리하므로 여기서는 제거
    }
    private void OnDisable()
    {
        if (owner != null)
        {
            owner.OnPlayerAttack -= OnPlayerAttacked;
            Debug.Log("onplayerattack 해제됨");
        }
    }

    private void OnPlayerAttacked()
    {
        foreach (var augment in activeAugments)
        {
            if (augment is ConditionalAugment conditionalAugment)
            {
                bool conditionMet = conditionalAugment.CheckCondition();
                if (conditionMet)
                {
                    Debug.Log($"Condition met for {augment.AugmentName}");
                    conditionalAugment.OnConditionDetect();
                }
            }
        }
    }
} 