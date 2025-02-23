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

    private Projectile currentAuraProjectile;

    private void Start()
    {
        projectiles.Add(Enums.SkillName.Javelin, Resources.Load<Projectile>("Using/Projectile/JavelinProjectile"));
        projectiles.Add(Enums.SkillName.Needle, Resources.Load<Projectile>("Using/Projectile/NeedleProjectile"));
        projectiles.Add(Enums.SkillName.Shuriken, Resources.Load<Projectile>("Using/Projectile/ShurikenProjectile"));
        projectiles.Add(Enums.SkillName.Aura, Resources.Load<Projectile>("Using/Projectile/AuraProjectile"));
        projectiles.Add(Enums.SkillName.Claw, Resources.Load<Projectile>("Using/Projectile/ClawLv3"));
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
        playerProjectiles.Add("GreatBowProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/GreatBowProjectile"));
        playerProjectiles.Add("CrossBowProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/CrossBowProjectile"));
        playerProjectiles.Add("RushProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/RushProjectile"));
        playerProjectiles.Add("ArcRangerProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/ArcRangerProjectile"));
        playerProjectiles.Add("PowerEffect", Resources.Load<PlayerProjectile>("Using/Projectile/PowerEffect"));
        playerProjectiles.Add("JewelProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/JewelProjectile"));

    }

    public void SpawnProjectile(Enums.SkillName skillName, float damage, int level, int shotCount, int projectileCount, float pierceDelay, float shotDelay, int pireceCount)
    {
        if (!projectiles.ContainsKey(skillName))
        {
            Debug.LogError($"Projectile type {skillName} not found!");
            return;
        }

        Vector3 startPosition = UnitManager.Instance.GetPlayer().transform.position;
        float speed = 3f;

        if (skillName == Enums.SkillName.Aura)
        {
            if (currentAuraProjectile != null)
            {
                RemoveProjectile(currentAuraProjectile);
            }

            Projectile projectile = Instantiate(projectiles[skillName], UnitManager.Instance.GetPlayer().transform);
            projectile.InitProjectile(startPosition, startPosition, speed, damage, 10000f, pireceCount, 100000f);
            currentAuraProjectile = projectile;
            activeProjectiles.Add(projectile);

            return;
        }

        List<Vector3> targetPositions = GetTargetPositionsBySkill(skillName, startPosition);
        int totalShots = shotCount * projectileCount;

        // Needle ?????? targetPositions ???ル봿?????됰Þ????????????????????ル봿?????됰엪??? ?轅붽틓???????ル∥堉??轅붽틓??影?뽧걤????????ш끽維뽳쭩???
        bool isNeedle = skillName == Enums.SkillName.Needle;
        bool isAura = skillName == Enums.SkillName.Aura;
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
                    else if (isAura)
                    {
                        targetPosition = startPosition;
                    }
                    else
                    {
                        targetPosition = targetPositions[j % targetPositions.Count];
                    }

                    Projectile projectile = Instantiate(projectiles[skillName]);
                    projectile.InitProjectile(startPosition, targetPosition, speed, damage, 10f, pireceCount, 5f);
                    activeProjectiles.Add(projectile);

                    await UniTask.Delay(TimeSpan.FromSeconds(shotDelay));
                }
                await UniTask.Delay(TimeSpan.FromSeconds(pierceDelay));
            }
        });
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
                RemoveProjectile(currentAuraProjectile);
            }

            Projectile projectile = Instantiate(projectiles[skillName], UnitManager.Instance.GetPlayer().transform);
            //projectile.InitProjectile(startPosition, Vector3.zero, speed, stats.finalDamage, -1f, stats.pierceCount, -1f);
            projectile.InitProjectile(startPosition, Vector3.zero, stats);

            currentAuraProjectile = projectile;
            activeProjectiles.Add(projectile);

            return;
        }

        List<Vector3> targetPositions = GetTargetPositionsBySkill(skillName, startPosition);

        // Needle ?????? targetPositions ???ル봿?????됰Þ????????????????????ル봿?????됰엪??? ?轅붽틓???????ル∥堉??轅붽틓??影?뽧걤????????ш끽維뽳쭩???
        bool isNeedle = skillName == Enums.SkillName.Needle;
        int currentIndex = 0;


        UniTask.Void(async () =>
        {
            for (int i = 0; i < stats.shotCount; i++)
            {
                for (int j = 0; j < stats.projectileCount; j++)
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
                    projectile.InitProjectile(startPosition, targetPosition, stats);
                    activeProjectiles.Add(projectile);
                    await UniTask.Delay(TimeSpan.FromSeconds(stats.projectileDelay));
                }
                await UniTask.Delay(TimeSpan.FromSeconds(Mathf.Max((stats.shotDelay - stats.projectileCount * stats.projectileDelay), 0f)));
            }
        });
    }

    private List<Vector3> GetTargetPositionsBySkill(Enums.SkillName skillName, Vector3 startPosition)
    {
        List<Vector3> targetPositions = new List<Vector3>();

        switch (skillName)
        {
            case Enums.SkillName.Javelin:
                Vector3? javelinTarget = UnitManager.Instance.GetNearestMonsterPosition();
                if (javelinTarget.HasValue)
                    targetPositions.Add(javelinTarget.Value);
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
                Vector3? fireballTarget = UnitManager.Instance.GetNearestMonsterPosition();
                if (fireballTarget.HasValue)
                    targetPositions.Add(fireballTarget.Value);
                break;

            default:
                // ????????????⑤뜪癲ル슢?뤺キ?????ル봿???????ル봿???關?쒎첎?嫄????轅붽틓??????우ク??? ????β뼯援???? ?????쇨덧?筌먦렜逾???癲ル슢??節륁춻???ш끽維??λ궔????
                Vector3? defaultTarget = UnitManager.Instance.GetNearestMonsterPosition();
                if (defaultTarget.HasValue)
                    targetPositions.Add(defaultTarget.Value);
                break;
        }

        // ????β뼯援?????????쇨덧?筌먦렜逾???????????癲ル슢??節륁춻???ш끽維??λ궔???????濚밸Ŧ???
        if (targetPositions.Count == 0)
        {
            Vector3 randomDirection = Random.insideUnitCircle.normalized;
            targetPositions.Add(startPosition + randomDirection * 15f);
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
        print($"RemoveProjectile! : {projectile.gameObject.name}");
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
