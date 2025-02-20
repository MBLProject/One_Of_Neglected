using UnityEngine;

public class Aug_SwordShield : TimeBasedAugment
{
    private int maxParryCount = 1;
    private int currentParryCount = 0;
    
    private float invincibilityDuration = 0f;  
    private float speedBuffDuration = 0.1f;    
    private float speedBuffPercent = 0.3f;     

    private float projectileSpeed = 2f;
    private float projectileSize = 1f;
    private int penetration = 100;
    private float duration = 5f;
    private float maxDistance = 10f;

    public Aug_SwordShield(Player owner, float interval) : base(owner, interval)
    {
        aguName = Enums.AugmentName.SwordShield;
    }

    protected override void OnTrigger()
    {
        if (currentParryCount < maxParryCount)
        {
            currentParryCount++;
        }
    }

    public bool TryParryProjectile(MonsterProjectile projectile)
    {
        if (currentParryCount > 0)
        {
            currentParryCount--;
            
            if (level >= 5)
            {
                owner.SetInvincible(invincibilityDuration);
                owner.ApplySpeedBuff(speedBuffDuration, speedBuffPercent);
            }
            else if (level >= 3)
            {
                owner.SetInvincible(invincibilityDuration);
            }

            Vector3 currentDirection = (projectile.transform.position - projectile.StartPosition).normalized;
            Vector3 reflectDirection = -currentDirection;  

            ProjectileManager.Instance.SpawnPlayerProjectile(
                "SwordAurorProjectile", 
                projectile.transform.position,  
                projectile.transform.position + reflectDirection * 10f,
                projectileSpeed,
                owner.Stats.CurrentATK * ((owner.Stats.CurrentDefense * 10) + 100) / 100,
                projectileSize,
                maxDistance,
                penetration,
                duration
            );

            return true;
        }
        return false;
    }

    protected override void OnLevelUp()
    {
        base.OnLevelUp();
        switch (level)
        {
            case 1:
                break;
            case 2:
                ModifyBaseInterval(-2f); 
                break;
            case 3:
                invincibilityDuration = 0.1f;
                speedBuffDuration = 0.1f;
                speedBuffPercent = 0.1f; 
                break;
            case 4:
                maxParryCount++;  
                break;
            case 5:
                invincibilityDuration = 0.15f;
                speedBuffDuration = 0.15f;
                speedBuffPercent = 0.3f;  
                break;
        }
    }
}
