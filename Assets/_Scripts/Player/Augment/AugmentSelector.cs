using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Enums;

public class AugmentSelector : MonoBehaviour
{
    [SerializeField] private Player owner;
    [SerializeField] private Dictionary<Enums.ClassType, List<Augment>> availableAugments;
    [SerializeField] public List<Augment> activeAugments = new List<Augment>();

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
            new Aug_Wand(owner,15f),
            new Aug_Orb(owner, 15f),
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
        if (availableAugments.TryGetValue(owner.ClassType, out var augments))
        {
            return augments.Select(aug => aug.AugmentName).ToList();
        }
        return new List<AugmentName>();
    }
}