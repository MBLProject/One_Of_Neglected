using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Aug_Staff : TimeBasedAugment
{
    private float damageMultiplier = 1f;
    private float projectileSpeed = 2f;
    private float projectileSize = 1f;
    private int penetration = 100;
    private float duration = 5f;
    private float maxDistance = 10f;

    private float CurrentDamage => owner.Stats.CurrentATK * damageMultiplier;

    public Aug_Staff(Player owner, float interval) : base(owner, interval)
    {
        aguName = Enums.AugmentName.Staff;
    }

    protected override  void OnTrigger()
    {
        UnitManager.Instance.TakeAllDamage(CurrentDamage * 2);
        
    }

    protected override void OnLevelUp()
    {
        base.OnLevelUp();
        switch (level)
        {
            case 1:
                damageMultiplier = 1f;
                break;
            case 2:
                projectileSize += 0.3f;
                break;
            case 3:
                damageMultiplier *= 1.2f;
                break;
            case 4:
                ModifyBaseInterval(-2f);
                break;
            case 5:
                damageMultiplier *= 1.3f;
                break;
        }
    }
}
