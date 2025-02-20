using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EarthquakeProjectile : PlayerProjectile
{
    protected override void Start()
    {
        pType = projType.Melee;
        isMoving = true;
        transform.position = startPosition;

        transform.rotation = Quaternion.identity;
        speed = 0f;
    }
    protected override async UniTaskVoid MoveProjectileAsync(System.Threading.CancellationToken token)
    {
        transform.position = startPosition;

        while (isMoving && !token.IsCancellationRequested)
        {
            if (gameObject == null || !isMoving)
            {
                break;
            }

            await UniTask.Yield(token);
        }
    }

}
