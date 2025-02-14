using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileManager : Singleton<ProjectileManager>
{
    public List<Projectile> activeProjectiles = new List<Projectile>();

    public Dictionary<Enums.SkillName, Projectile> projectiles = new Dictionary<Enums.SkillName, Projectile>();
    public Dictionary<string, MonsterProjectile> monsterProjectiles = new Dictionary<string, MonsterProjectile>();

    public int count;

    private void Start()
    {
        projectiles.Add(Enums.SkillName.Javelin, Resources.Load<Projectile>("Using/Projectile/JavelinProjectile"));
        projectiles.Add(Enums.SkillName.Needle, Resources.Load<Projectile>("Using/Projectile/NeedleProjectile"));

        monsterProjectiles.Add("RangedNormal", Resources.Load<MonsterProjectile>("Using/Projectile/MonsterProjectile"));
    }

    public void SpawnProjectile(Enums.SkillName skillName, float damage, int level, int shotCount, int projectileCount, float pierceDelay, float shotDelay)
    {
        if (!projectiles.ContainsKey(skillName))
        {
            Debug.LogError($"Projectile type {skillName} not found!");
            return;
        }

        Vector3 startPosition = UnitManager.Instance.GetPlayer().transform.position;
        float speed = 1f;

        List<Vector3> targetPositions = GetTargetPositionsBySkill(skillName, startPosition);
        int totalShots = shotCount * projectileCount;

        // Needle 특성: targetPositions 개수보다 총 투사체 개수가 많으면 처음부터 반복
        bool isNeedle = skillName == Enums.SkillName.Needle;
        int currentIndex = 0;

        UniTask.Void(async () =>
        {
            for (int i = 0; i < shotCount; i++)
            {
                for (int j = 0; j < projectileCount; j++)
                {
                    if (targetPositions.Count == 0) continue;

                    Vector3 targetPosition;
                    if (isNeedle)
                    {
                        targetPosition = targetPositions[currentIndex];
                        currentIndex = (currentIndex + 1) % targetPositions.Count;
                    }
                    else
                    {
                        targetPosition = targetPositions[j % targetPositions.Count];
                    }

                    Projectile projectile = Instantiate(projectiles[skillName]);
                    projectile.InitProjectile(startPosition, targetPosition, speed, damage, 10f);
                    activeProjectiles.Add(projectile);

                    await UniTask.Delay(TimeSpan.FromSeconds(shotDelay));
                }
                await UniTask.Delay(TimeSpan.FromSeconds(pierceDelay));
            }
        });
    }



    private List<Vector3> GetTargetPositionsBySkill(Enums.SkillName skillName, Vector3 startPosition)
    {
        List<Vector3> targetPositions = new List<Vector3>();

        switch (skillName)
        {
            case Enums.SkillName.Javelin:
                Vector3? nearest = UnitManager.Instance.GetNearestMonsterPosition();
                if (nearest.HasValue)
                    targetPositions.Add(nearest.Value);
                break;

            case Enums.SkillName.Needle:
                List<Vector3> needleTargets = UnitManager.Instance.GetMonsterPositionsInRange(0f, 3f);
                if (needleTargets.Count > 0)
                    targetPositions.AddRange(needleTargets);
                break;

            case Enums.SkillName.Shuriken:
                Vector3? shurikenTarget = UnitManager.Instance.GetNearestMonsterPosition();
                if (shurikenTarget.HasValue)
                    targetPositions.Add(shurikenTarget.Value);
                break;

            case Enums.SkillName.Fireball:
                // 가장 가까운 몬스터가 있으면 타겟팅, 없으면 직선 방향 발사
                Vector3? fireballTarget = UnitManager.Instance.GetNearestMonsterPosition();
                if (fireballTarget.HasValue)
                    targetPositions.Add(fireballTarget.Value);
                break;

            default:
                // 기본적으로 가장 가까운 몬스터를 타겟팅, 없으면 랜덤 방향
                Vector3? defaultTarget = UnitManager.Instance.GetNearestMonsterPosition();
                if (defaultTarget.HasValue)
                    targetPositions.Add(defaultTarget.Value);
                break;
        }

        // 타겟이 없으면 기본 랜덤 방향 설정
        if (targetPositions.Count == 0)
        {
            Vector3 randomDirection = Random.insideUnitCircle.normalized;
            targetPositions.Add(startPosition + randomDirection * 15f);
        }

        return targetPositions;
    }

    public void SpawnMonsterProjectile(string projectileType, Vector3 startPosition, Vector3 direction, float speed, float damage)
    {
        if (!monsterProjectiles.ContainsKey(projectileType))
        {
            Debug.LogError($"Projectile type {projectileType} not found!");
            return;
        }

        MonsterProjectile projectile = Instantiate(monsterProjectiles[projectileType]);
        projectile.InitProjectile(startPosition, direction, speed, damage);

        activeProjectiles.Add(projectile);
    }
    public void RemoveProjectile(MonsterProjectile projectile)
    {
        if (activeProjectiles.Contains(projectile))
        {
            activeProjectiles.Remove(projectile);
        }
    }

    public void RemoveProjectile(Projectile projectile)
    {
        print("RemoveProjectile!");
        if (activeProjectiles.Contains(projectile))
        {
            activeProjectiles.Remove(projectile);
        }
        Destroy(projectile.gameObject);
    }

    private void OnDestroy()
    {
        print("OnDestroy : ProjectileManager!");

        activeProjectiles.Clear();
    }

    private void Update()
    {
        count = projectiles.Count + monsterProjectiles.Count;
    }
}
