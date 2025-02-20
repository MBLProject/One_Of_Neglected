using UnityEngine;

public class Aug_TwoHandSword : TimeBasedAugment
{
    private float damageMultiplier = 1f;  // 기본 데미지 배율
    private float projectileSpeed = 2f;
    private float projectileSize = 1f;
    private int penetration = 100;
    private float duration = 5f;
    private float maxDistance = 10f;
    
    private float CurrentDamage => owner.Stats.CurrentATK * damageMultiplier;

    public Aug_TwoHandSword(Player owner, float interval) : base(owner, interval)
    {
        aguName = Enums.AugmentName.TwoHandSword;
    }

    protected override void OnTrigger()
    {
        Vector3 targetPos = UnitManager.Instance.GetNearestMonster().transform.position;
        Vector3 direction = (targetPos - owner.transform.position).normalized;
        
        if(level >= 5)
        {
            SpawnProjectile(direction);
            SpawnProjectile(RotateVector(direction, 15));
            SpawnProjectile(RotateVector(direction, -15));
        }
        else if(level >= 3)
        {
            SpawnProjectile(direction);
            SpawnProjectile(RotateVector(direction, 15));
        }
        else
        {
            SpawnProjectile(direction);
        }
    }

    private void SpawnProjectile(Vector3 direction)
    {
        Vector3 targetPosition = owner.transform.position + direction * 10f;
        
        ProjectileManager.Instance.SpawnPlayerProjectile(
            "SwordAurorProjectile",
            owner.transform.position,
            targetPosition,
            projectileSpeed,
            CurrentDamage,  // 현재 플레이어 공격력 기반 데미지
            projectileSize,
            maxDistance,
            penetration,
            duration);
    }

    private Vector3 RotateVector(Vector3 vector, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
        
        float x = vector.x * cos - vector.y * sin;
        float y = vector.x * sin + vector.y * cos;
        
        return new Vector3(x, y, 0);
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