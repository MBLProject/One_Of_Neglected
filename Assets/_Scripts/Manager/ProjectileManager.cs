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
        float speed = 1f;

        // find nearest monster's position
        Vector3 targetPosition = FindNearestMonsterPosition(startPosition, 3f);

        // if target is null, shoot with random direction
        if (targetPosition == Vector3.zero)
        {
            Vector3 randomDirection = Random.insideUnitCircle.normalized;
            targetPosition = startPosition + randomDirection * 15f;
        }

        Projectile projectile = Instantiate(projectiles[skillName]);
        projectile.InitProjectile(startPosition, targetPosition, speed, damage, 10f);

        //print($"SpawnProjectile : {skillName}, startPosition : {startPosition}, targetPosition : {targetPosition}, speed : {speed}");

        activeProjectiles.Add(projectile);
    }

    private Vector3 FindNearestMonsterPosition(Vector3 center, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);
        float closestDistance = float.MaxValue;
        Vector3 nearestMonsterPosition = Vector3.zero;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Monster"))
            {
                float distance = Vector3.Distance(center, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestMonsterPosition = collider.transform.position;
                }
            }
        }

        return nearestMonsterPosition;
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
