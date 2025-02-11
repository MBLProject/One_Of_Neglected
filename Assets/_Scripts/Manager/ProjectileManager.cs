
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    public List<Projectile> activeProjectiles = new List<Projectile>();

    public Dictionary<Enums.SkillName, Projectile> projectiles = new Dictionary<Enums.SkillName, Projectile>();

    public int count;

    private string path = "Resources/SaveFile/";

    private void Start()
    {
        projectiles.Add(Enums.SkillName.Needle, Resources.Load<Projectile>("Using/Projectile/NeedleProjectile"));
    }

    public void SpawnProjectile(Enums.SkillName skillName)
    {
        // TODO : create correct Projectile by SkillName with Dictionary key
        
        Vector3 startPosition = Vector3.zero; // TODO : player pos
        Vector3 direction = Random.insideUnitCircle.normalized; // TODO : closest monster
        float speed = 1f;

        Projectile projectile = Instantiate(projectiles[skillName]);
        projectile.InitProjectile(startPosition, direction, speed, 10f);

        print($"SpawnProjectile : {skillName}, startPosition : {startPosition}, direction : {direction}, speed : {speed}");

        activeProjectiles.Add(projectile);
    }

    private void Update()
    {
        count = projectiles.Count;
    }
}
