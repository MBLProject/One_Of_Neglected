
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

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

    public void SpawnProjectile(Enums.SkillName skillName, float damage, int level)
    {
        if (!projectiles.ContainsKey(skillName))
        {
            Debug.LogError($"Projectile type {skillName} not found!");
            return;
        }

        // TODO : create correct Projectile by SkillName with Dictionary key

        Vector3 startPosition = UnitManager.Instance.GetPlayer().gameObject.transform.position; // TODO : player pos
        Vector3 direction = Random.insideUnitCircle.normalized; // TODO : closest monster
        float speed = 1f;

        Projectile projectile = Instantiate(projectiles[skillName]);
        projectile.InitProjectile(startPosition, direction, speed, damage, 10f);

        print($"SpawnProjectile : {skillName}, startPosition : {startPosition}, direction : {direction}, speed : {speed}");

        activeProjectiles.Add(projectile);
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
        if (activeProjectiles.Contains(projectile))
        {
            activeProjectiles.Remove(projectile);
        }
    }

    // ?덉쟾???뺣━瑜??꾪븳 硫붿꽌??異붽?
    private void OnDestroy()
    {
        activeProjectiles.Clear();
    }

    private void Update()
    {
        count = projectiles.Count + monsterProjectiles.Count;
    }
}
