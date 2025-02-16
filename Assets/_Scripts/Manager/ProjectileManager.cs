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

        monsterProjectiles.Add("RangedNormal", Resources.Load<MonsterProjectile>("Using/Projectile/MonsterProjectile"));

        playerProjectiles.Add("WarriorAttackProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/WarriorAttackProjectile"));
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

        if (skillName == Enums.SkillName.Aura)
        {
            if (currentAuraProjectile != null)
            {
                RemoveProjectile(currentAuraProjectile);
            }

            Projectile projectile = Instantiate(projectiles[skillName], UnitManager.Instance.GetPlayer().transform);
            projectile.InitProjectile(startPosition, startPosition, speed, damage, 10000f, 0, 100000f);
            currentAuraProjectile = projectile;
            activeProjectiles.Add(projectile);

            return;
        }

        List<Vector3> targetPositions = GetTargetPositionsBySkill(skillName, startPosition);
        int totalShots = shotCount * projectileCount;

        // Needle ?獄??? targetPositions ?띠룇裕?遺삵돦????????亦낆?럸??띠룇裕?遺룹쾸? 嶺뚮씭???얠춺?嶺뚳퐣瑗??寃????꾩룇瑗??
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
                // ?リ옇????⑤챷紐드슖??띠럾????띠럾?濚밸Ŧ???嶺뚮ㄳ?낅츩??? ???롪퍔??? ??怨몃さ嶺???類ｌ몓 ?꾩렮維싧젆?
                Vector3? defaultTarget = UnitManager.Instance.GetNearestMonsterPosition();
                if (defaultTarget.HasValue)
                    targetPositions.Add(defaultTarget.Value);
                break;
        }

        // ???롪퍔?????怨몃さ嶺??リ옇?????類ｌ몓 ?꾩렮維싧젆????깆젧
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

    public void SpawnPlayerProjectile(string prefabName, Vector3 startPos, Vector3 targetPos, float speed, float damge)
    {
        if (!playerProjectiles.ContainsKey(prefabName))
        {
            Debug.LogError($"Projectile type {prefabName} not found!");
            return;
        }

        PlayerProjectile projectile = Instantiate(playerProjectiles[prefabName]);
        projectile.InitProjectile(startPos, targetPos, speed, damge);


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
