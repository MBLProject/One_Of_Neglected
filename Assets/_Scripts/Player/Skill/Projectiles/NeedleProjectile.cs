using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class NeedleProjectile : Projectile
{
    private Collider2D projectileCollider;

    protected override void Start()
    {
        isMoving = true;
        transform.position = targetPosition;
        projectileCollider = GetComponent<Collider2D>();
        cts = new CancellationTokenSource();
        DespawnNeedleAtPosition().Forget();
    }

    private async UniTaskVoid DespawnNeedleAtPosition()
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
            if (isMoving)
            {
                DestroyProjectile();
                isMoving = false;
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Needle despawn was canceled.");
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public override void InitProjectile(Vector3 startPos, Vector3 targetPos, float spd, float dmg, float maxDist = 0f, int pierceCnt = 0, float lifetime = 5f)
    {
        this.startPosition = startPos;
        this.targetPosition = targetPos;
        speed = spd;
        maxDistance = maxDist;
        damage = dmg;
        pierceCount = pierceCnt;
        lifeTime = lifetime;

        CancelInvoke("DestroyProjectile");
        Invoke("DestroyProjectile", lifeTime);

        direction = (targetPosition - startPos).normalized;
    }

}
