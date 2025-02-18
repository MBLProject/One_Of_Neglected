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



        monsterProjectiles.Add("RangedNormal", Resources.Load<MonsterProjectile>("Using/Projectile/MonsterProjectile"));

        playerProjectiles.Add("WarriorAttackProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/WarriorAttackProjectile"));
        playerProjectiles.Add("MagicianAttackProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/MagicianAttackProjectile"));
        playerProjectiles.Add("ArcherAttackProjectile", Resources.Load<PlayerProjectile>("Using/Projectile/ArcherAttackProjectile"));

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

        // Needle ?????? targetPositions ??좊즵獒??븐궢?????????雅?굞??????좊즵獒??븍９苡? 癲ル슢?????좎떵?癲ル슪?ｇ몭??野????袁⑸즵???
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
                // ??れ삀?????ㅼ굣筌뤿뱶????좊읈?????좊읈?嚥싲갭큔???癲ル슢???낆릇??? ???濡ろ뜑??? ???⑤챶?뺧┼???筌먲퐣紐??袁⑸젻泳?떑??
                Vector3? defaultTarget = UnitManager.Instance.GetNearestMonsterPosition();
                if (defaultTarget.HasValue)
                    targetPositions.Add(defaultTarget.Value);
                break;
        }

        // ???濡ろ뜑??????⑤챶?뺧┼???れ삀?????筌먲퐣紐??袁⑸젻泳?떑?????源놁젳
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

    public void SpawnPlayerProjectile(string prefabName, Vector3 startPos, Vector3 targetPos, float speed, float damge, float maxDist = 0f, int pierceCnt = 0, float lifetime = 5f)
    {
        if (!playerProjectiles.ContainsKey(prefabName))
        {
            Debug.LogError($"Projectile type {prefabName} not found!");
            return;
        }

        PlayerProjectile projectile = Instantiate(playerProjectiles[prefabName]);
        projectile.InitProjectile(startPos, targetPos, speed, damge, maxDist, pierceCnt, lifetime);


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
