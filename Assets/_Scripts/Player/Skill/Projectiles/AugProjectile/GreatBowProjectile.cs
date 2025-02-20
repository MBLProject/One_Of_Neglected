using UnityEngine;

public class GreatBowProjectile : PlayerProjectile
{
    protected override void Start()
    {
        pType = projType.Normal;
        base.Start();
    }
    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, float spd, float dmg, float maxDist = 0f, int pierceCnt = 0, float lifetime = 5f)
    {
        base.InitProjectile(startPos, targetPos, spd, dmg, maxDist, pierceCnt, lifetime);
        // 투사체가 목표를 향해 회전하도록 설정
        Vector3 direction = (startPos - targetPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
