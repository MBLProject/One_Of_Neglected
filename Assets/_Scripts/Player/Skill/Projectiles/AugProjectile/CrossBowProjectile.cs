using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowProjectile : PlayerProjectile
{
    protected override void Start()
    {
        pType = projType.Normal;
        base.Start();
    }
}
