
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    public List<MonsterProjectile> activeProjectiles = new List<MonsterProjectile>();

    public Dictionary<Enums.SkillName, MonsterProjectile> projectiles = new Dictionary<Enums.SkillName, MonsterProjectile>();

    public int count;

    private string path = "Resources/SaveFile/";

    public Dictionary<string, MonsterProjectile> monsterProjectiles = new Dictionary<string, MonsterProjectile>();

    private void Start()
    {
        projectiles.Add(Enums.SkillName.Needle, Resources.Load<MonsterProjectile>("Using/Projectile/NeedleProjectile"));

        monsterProjectiles.Add("RangedNormal", Resources.Load<MonsterProjectile>("Using/Projectile/MonsterProjectile"));
    }

    public void SpawnProjectile(Enums.SkillName skillName)
    {
        // TODO : create correct Projectile by SkillName with Dictionary key

        Vector3 startPosition = Vector3.zero; // TODO : player pos
        Vector3 direction = Random.insideUnitCircle.normalized; // TODO : closest monster
        float speed = 1f;

        MonsterProjectile projectile = Instantiate(projectiles[skillName]);
        projectile.InitProjectile(startPosition, direction, speed, 10f);

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

    // 안전한 정리를 위한 메서드 추가
    private void OnDestroy()
    {
        activeProjectiles.Clear();
    }

    private void Update()
    {
        count = projectiles.Count+monsterProjectiles.Count;
    }
}
