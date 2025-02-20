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
    }
    
    private void InitializeAugments()
    {
        availableAugments = new Dictionary<Enums.ClassType, List<Augment>>();
        
        availableAugments[Enums.ClassType.Warrior] = new List<Augment>
        {
            new Aug_TwoHandSword(owner, 10f),
            new Aug_BigSword(owner, 10f),
            new Aug_SwordShield(owner, 10f),
            new Aug_Shielder(owner),
        };

        availableAugments[Enums.ClassType.Archer] = new List<Augment>
        {
            new Aug_LongBow(owner),
            new Aug_CrossBow(owner, 10f),
            new Aug_GreatBow(owner, 10f),
            new Aug_ArcRanger(owner),
        };

        availableAugments[Enums.ClassType.Magician] = new List<Augment>
        {
            new Aug_Staff(owner, 20f),
            new Aug_Wand(owner, 1f),
            new Aug_Orb(owner, 1f),
            new Aug_Warlock(owner),
        };
    }

    private List<Augment> GetAvailableAugments()
    {
        
        if (availableAugments.TryGetValue(owner.ClassType, out var augments))
        {
            var available = augments.FindAll(a => !activeAugments.Contains(a));
            return available;
        }
        return new List<Augment>();
    }
    
    public List<Augment> SelectAugmentsWithInfo()
    {
        var availableList = GetAvailableAugments();
        // 랜덤하게 3개 선택 -> 원래 4갠데 칸이 모자람 ㅎㅎㅈㅅ
        if (availableList.Count > 3)
        {
            var randomIndices = new List<int>();
            while (randomIndices.Count < 3)
            {
                var index = UnityEngine.Random.Range(0, availableList.Count);
                if (!randomIndices.Contains(index))
                {
                    randomIndices.Add(index);
                }
            }

            var selectedAugments = new List<Augment>();
            foreach (var index in randomIndices)
            {
                selectedAugments.Add(availableList[index]);
            }
            return selectedAugments;
        }
        
        return availableList;
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
                selectedAugment.LevelUp();
            }
            else
            {
                selectedAugment.LevelUp();
            }
        }
    }

    public int GetAugmentLevel(AugmentName augmentName)
    {
        var augment = activeAugments.FirstOrDefault(aug => aug.AugmentName == augmentName);
        return augment?.Level ?? 0;
    }

    public bool IsAugmentActive(AugmentName augmentName)
    {
        return activeAugments.Any(aug => aug.AugmentName == augmentName);
    }

    public bool CanLevelUpAugment(AugmentName augmentName)
    {
        var augment = activeAugments.FirstOrDefault(aug => aug.AugmentName == augmentName);
        return augment != null && augment.Level < 5;
    }

    public void LevelUpAugment(AugmentName augmentName)
    {
        var augment = activeAugments.FirstOrDefault(aug => aug.AugmentName == augmentName);
        if (augment != null && augment.Level < 5)
        {
            augment.LevelUp();
        }
    }

    public List<AugmentName> SelectAugments()
    {
        var availableList = GetAvailableAugments();
        
        var selectedAugments = new List<Augment>();
        if (availableList.Count > 3)
        {
            var randomIndices = new List<int>();
            while (randomIndices.Count < 3)
            {
                var index = UnityEngine.Random.Range(0, availableList.Count);
                if (!randomIndices.Contains(index))
                {
                    randomIndices.Add(index);
                }
            }

            foreach (var index in randomIndices)
            {
                selectedAugments.Add(availableList[index]);
            }
        }
        else
        {
            selectedAugments = availableList;
        }
        
        var result = selectedAugments.Select(aug => aug.AugmentName).ToList();
        
        return result;
    }
} 