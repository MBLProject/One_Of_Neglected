
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    public List<Projectile> activeProjectiles = new List<Projectile>();

    public void SpawnProjectile(Enums.SkillName skillName)
    {
        // TODO : create correct Projectile by SkillName with switch

        Vector3 startPosition = Vector3.zero; // TODO : player pos
        Vector3 direction = Random.insideUnitCircle.normalized; // TODO : closest monster
        float speed = 1f;


        //activeProjectiles.Add(new Projectile(startPosition, direction, speed));
    }

    private void Update()
    {

    }
}
