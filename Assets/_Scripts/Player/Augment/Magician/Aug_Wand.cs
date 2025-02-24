using UnityEngine;

public class Aug_Wand : TimeBasedAugment
{
    private GameObject wandtEffectPrefab;

    public Aug_Wand(Player owner, float interval) : base(owner, interval)
    {
        aguName = Enums.AugmentName.Wand;
        wandtEffectPrefab = Resources.Load<GameObject>("Using/Effect/WandEffect");
    }

    protected override void OnTrigger()
    {
        if (wandtEffectPrefab != null)
        {
            GameObject startEffect = GameObject.Instantiate(wandtEffectPrefab, owner.transform.position, Quaternion.identity);
            GameObject.Destroy(startEffect, 1f); 
        }

        var skillDispenser = owner.GetComponent<SkillDispenser>();
        //skillDispenser.FireAllSkills();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
