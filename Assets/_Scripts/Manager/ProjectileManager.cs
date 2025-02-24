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
    public Dictionary<string, PlayerProjectile> playerProjectiles = new Dictionary<string, PlayerProjectile>();

    public int count;

    public Projectile currentAuraProjectile;

    private void Start()
    {
        projectiles.Add(Enums.SkillName.Javelin, Resources.Load<Projectile>("Using/Projectile/JavelinProjectile"));
        projectiles.Add(Enums.SkillName.Needle, Resources.Load<Projectile>("Using/Projectile/NeedleProjectile"));
        projectiles.Add(Enums.SkillName.Shuriken, Resources.Load<Projectile>("Using/Projectile/ShurikenProjectile"));
        projectiles.Add(Enums.SkillName.Aura, Resources.Load<Projectile>("Using/Projectile/AuraProjectile"));
        projectiles.Add(Enums.SkillName.Claw, Resources.Load<Projectile>("Using/Projectile/ClawProjectile"));
        projectiles.Add(Enums.SkillName.PoisonShoes, Resources.Load<Projectile>("Using/Projectile/PoisonShoesProjectile"));
        projectiles.Add(Enums.SkillName.Fireball, Resources.Load<Projectile>("Using/Projectile/FireballProjectile"));
        projectiles.Add(Enums.SkillName.Gateway, Resources.Load<Projectile>("Using/Projectile/GatewayProjectile"));


        monsterProjectiles.Add("RangedNormal", Resources.Load<MonsterProjectile>("Using/Projectile/MonsterProjectile"));

        playerProjectiles.Add("WarriorAttackProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/WarriorAttackProjectile"));
        playerProjectiles.Add("MagicianAttackProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/MagicianAttackProjectile"));
        playerProjectiles.Add("ArcherAttackProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/ArcherAttackProjectile"));

        playerProjectiles.Add("SwordAurorProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/SwordAurorProjectile"));
        playerProjectiles.Add("EarthquakeProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/EarthquakeProjectile"));
        playerProjectiles.Add("SubEarthquakeProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/SubEarthquakeProjectile"));
        playerProjectiles.Add("SwordShieldProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/SwordShieldProjectile"));
        playerProjectiles.Add("RushEndProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/RushEndProjectile"));
        playerProjectiles.Add("RushProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/RushProjectile"));

        playerProjectiles.Add("GreatBowProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/GreatBowProjectile"));
        playerProjectiles.Add("CrossBowProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/CrossBowProjectile"));
        playerProjectiles.Add("ArcRangerProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/ArcRangerProjectile"));
        playerProjectiles.Add("LongBowProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/LongBowProjectile"));

        playerProjectiles.Add("PowerEffect", Resources.Load<PlayerProjectile>("Using/Projectile/PowerEffect"));
        playerProjectiles.Add("JewelProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/JewelProjectile"));

    }

    public void SpawnProjectile(Enums.SkillName skillName, ProjectileStats stats)
    {
        if (!projectiles.ContainsKey(skillName))
        {
            Debug.LogError($"Projectile type {skillName} not found!");
            return;
        }

        Vector3 startPosition = UnitManager.Instance.GetPlayer().transform.position;

        if (skillName == Enums.SkillName.Aura)
        {
            if (currentAuraProjectile != null)
            {
                currentAuraProjectile.InitProjectile(startPosition, Vector3.zero, stats);
                return;
            }

            Projectile projectile = Instantiate(projectiles[skillName], UnitManager.Instance.GetPlayer().transform);
            //projectile.InitProjectile(startPosition, Vector3.zero, speed, stats.finalDamage, -1f, stats.pierceCount, -1f);
            projectile.InitProjectile(startPosition, Vector3.zero, stats);

            currentAuraProjectile = projectile;
            activeProjectiles.Add(projectile);

            return;
        }

        List<Vector3> targetPositions = GetTargetPositionsBySkill(skillName, startPosition, stats);

        // Needle ?????? targetPositions ?????ル뒌??????嫄??????????????????????ル뒌??????怨쀫역??? ?耀붾굝????????????떔???耀붾굝????鶯ㅺ동??筌믡룓愿???????????밸븶筌믩끃????
        bool isMultipleTarget = skillName == Enums.SkillName.Needle || skillName == Enums.SkillName.Gateway;
        int currentIndex = 0;


        UniTask.Void(async () =>
        {
            for (int i = 0; i < stats.shotCount; i++)
            {
                for (int j = 0; j < stats.projectileCount; j++)
                {
                    if (targetPositions.Count == 0) continue;

                    Vector3 targetPosition;
                    if (isMultipleTarget)
                    {
                        targetPosition = targetPositions[currentIndex];
                        currentIndex = (currentIndex + 1) % targetPositions.Count;
                    }
                    else
                    {
                        targetPosition = targetPositions[j % targetPositions.Count];
                    }

                    Projectile projectile = Instantiate(projectiles[skillName]);
                    projectile.InitProjectile(startPosition, targetPosition, stats);
                    activeProjectiles.Add(projectile);
                    await UniTask.Delay(TimeSpan.FromSeconds(stats.projectileDelay));
                }
                await UniTask.Delay(TimeSpan.FromSeconds(Mathf.Max((stats.shotDelay - stats.projectileCount * stats.projectileDelay), 0f)));
            }
        });
    }

    private List<Vector3> GetTargetPositionsBySkill(Enums.SkillName skillName, Vector3 startPosition, ProjectileStats stats)
    {
        List<Vector3> targetPositions = new List<Vector3>();

        switch (skillName)
        {
            case Enums.SkillName.Gateway:
                List<Vector3> gatewayTargets = UnitManager.Instance.GetMonsterRamdomPositionsInRange(0f, 3f, stats.projectileCount);
                if (gatewayTargets.Count > 0)
                    targetPositions.AddRange(gatewayTargets);
                break;

            case Enums.SkillName.Needle:
                List<Vector3> needleTargets = UnitManager.Instance.GetMonsterPositionsInRange(0f, 3f);
                if (needleTargets.Count > 0)
                    targetPositions.AddRange(needleTargets);
                break;

            default:
                Vector3? defaultTarget = UnitManager.Instance.GetNearestMonsterPosition();
                if (defaultTarget.HasValue)
                    targetPositions.Add(defaultTarget.Value);
                break;
        }

        if (targetPositions.Count == 0)
        {
            if(skillName == Enums.SkillName.Needle || skillName == Enums.SkillName.Gateway)
            {
                for(int i = 0; i < stats.projectileCount * stats.shotCount; ++i)
                {
                    Vector3 randomDirection = Random.insideUnitCircle.normalized;
                    targetPositions.Add(startPosition + randomDirection * 1f);
                }
            }
            else
            {
                Vector3 randomDirection = Random.insideUnitCircle.normalized;
                targetPositions.Add(startPosition + randomDirection * 15f);
            }
        }
        print($"GetTargetPositionsBySkill : {skillName} : {targetPositions.Count}");

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

    public PlayerProjectile SpawnPlayerProjectile(string prefabName, Vector3 startPos, Vector3 targetPos, 
        float speed, float damage, float size, float maxDist = 10f, int pierceCnt = 0, float lifetime = 5f)
    {
        if (!playerProjectiles.ContainsKey(prefabName))
        {
            Debug.LogError($"Projectile type {prefabName} not found!");
            return null;
        }

        PlayerProjectile projectile = Instantiate(playerProjectiles[prefabName]);
        projectile.transform.localScale = Vector3.one * size;
        projectile.InitProjectile(startPos, targetPos, speed, damage, maxDist, pierceCnt, lifetime);

        activeProjectiles.Add(projectile);
        return projectile;
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
        projectile.DestroyProjectile();
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
