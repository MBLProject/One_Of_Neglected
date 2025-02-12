using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleProjectile : Projectile
{
    protected override void Start()
    {
        transform.position = startPosition;
        MoveProjectileAsync().Forget();
    }
}
