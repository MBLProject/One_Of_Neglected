using UnityEngine;

public class Aug_ArcRanger : ConditionalAugment
{
    private float damageMultiplier = 1f;
    private float projectileSpeed = 3f;
    private float projectileSize = 1f;
    private int penetration = 0;
    private float duration = 5f;
    private float maxDistance = 10f;

    private float CurrentDamage => owner.Stats.CurrentATK * damageMultiplier;

    private PlayerProjectile currentPathProjectile;

    public Aug_ArcRanger(Player owner) : base(owner)
    {
        aguName = Enums.AugmentName.ArcRanger;
    }

    public override void Activate()
    {
        base.Activate();

        owner.dashDetect += OnDashDetect;
        owner.dashCompleted += OnDashCompleted;
        owner.DamageReduction += 10f;
    }

    private void OnDashDetect()
    {
        if (CheckCondition())
        {
            OnConditionDetect();
            SpawnRushProjectile();
        }
    }

    public override bool CheckCondition()
    {
        return true;
    }

    public override void OnConditionDetect()
    {
    }

    private void SpawnRushProjectile()
    {
        if (currentPathProjectile != null)
        {
            ProjectileManager.Instance.RemoveProjectile(currentPathProjectile);
        }

        Vector2 dashStart = owner.transform.position;
        
        // 플레이어에서 대쉬 방향 가져오니까 이상함 ㅜㅜ
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool hasKeyboardInput = horizontalInput != 0 || verticalInput != 0;

        Vector2 direction;
        if (hasKeyboardInput)
        {
            direction = new Vector2(horizontalInput, verticalInput).normalized;
        }
        else
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - dashStart).normalized;
        }

        int baseProjectiles = 6;
        float angleStep = 5f;
        
        int totalProjectiles = baseProjectiles + owner.Stats.CurrentProjAmount;
        float totalAngleSpread = (totalProjectiles - 1) * angleStep;
        float startAngle = -totalAngleSpread / 2f;

        for (int i = 0; i < totalProjectiles; i++)
        {
            float currentAngle = startAngle + (i * angleStep);
            Vector2 rotatedDirection = RotateVector(direction, currentAngle);
            Vector2 targetPosition = dashStart + rotatedDirection * maxDistance;

            ProjectileManager.Instance.SpawnPlayerProjectile(
                "ArcRangerProjectile",
                dashStart,
                targetPosition,
                projectileSpeed,
                CurrentDamage,
                projectileSize,
                maxDistance,
                penetration,
                duration
            );
        }

        currentPathProjectile = ProjectileManager.Instance.SpawnPlayerProjectile(
            "ArcRangerProjectile",
            dashStart,
            dashStart,
            0f,
            CurrentDamage,
            1.5f,
            maxDistance,
            penetration,
            duration
        );

        if (currentPathProjectile != null)
        {
            currentPathProjectile.transform.SetParent(owner.transform);
            currentPathProjectile.transform.rotation = Quaternion.identity;
            currentPathProjectile.transform.localPosition = Vector3.zero;
        }
    }

    private Vector2 RotateVector(Vector2 vector, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
        
        float x = vector.x * cos - vector.y * sin;
        float y = vector.x * sin + vector.y * cos;
        
        return new Vector2(x, y);
    }

    private void SpawnshockwaveProjectile()
    {
        Vector2 dashEnd = owner.targetPosition;
        ProjectileManager.Instance.SpawnPlayerProjectile(
            "EarthquakeProjectile",
            dashEnd,
            dashEnd,
            0f,
            CurrentDamage / 2,
            2,
            0.1f,
            penetration,
            0.5f);
    }

    private void OnDashCompleted()
    {
        if (currentPathProjectile != null)
        {
            ProjectileManager.Instance.RemoveProjectile(currentPathProjectile);
            currentPathProjectile = null;
        }

        if (level >= 5)
        {
            SpawnshockwaveProjectile();
        }
    }

    protected override void OnLevelUp()
    {
        base.OnLevelUp();
        switch (level)
        {
            case 1:
                break;
            case 2:
                owner.dashRechargeTime *= 0.9f;
                break;
            case 3:
                owner.DamageReduction += 10f;
                owner.Stats.CurrentDashCount++;
                break;
            case 4:
                owner.dashRechargeTime *= 0.8f;
                break;
            case 5:
                break;
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();

        owner.dashDetect -= OnDashDetect;
        owner.dashCompleted -= OnDashCompleted;
        owner.DamageReduction = 0f;

        if (currentPathProjectile != null)
        {
            ProjectileManager.Instance.RemoveProjectile(currentPathProjectile);
            currentPathProjectile = null;
        }
    }
}
