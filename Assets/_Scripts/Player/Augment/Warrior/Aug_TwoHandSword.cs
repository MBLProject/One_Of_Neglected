using System;
using UnityEngine;

public class Aug_TwoHandSword : ConditionalAugment
{
    private int attackCount = 0;
    // n회마다 발싸!!
    private int requiredAttacks = 3; 
    
    public Aug_TwoHandSword(Player owner) : base(owner) 
    {
        aguName = Enums.AugmentName.TwoHandSword;
    }

    public override bool CheckCondition()
    {
        Debug.Log($"attackCount : {attackCount}");
        attackCount++;
        if (attackCount >= requiredAttacks)
        {
            attackCount = 0; 
            return true;
        }
        return false;
    }

    public override void OnConditionDetect()
    {
        ProjectileManager.Instance.SpawnPlayerProjectile(
            "MagicianAttackProjectile",
            owner.transform.position,
            UnitManager.Instance.GetNearestMonster().transform.position,
            1f,
            10f,
            0.1f,
            1,
            10f
        );
    }

    protected override void OnLevelUp()
    {
        base.OnLevelUp();
    }
}