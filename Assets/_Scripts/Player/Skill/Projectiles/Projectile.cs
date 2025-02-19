using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using static Enums;
using System;

public class Projectile : MonoBehaviour
{
    public float speed;
    protected float damage;
    public float maxDistance = 10f;
    protected Vector3 startPosition;
    protected Vector3 targetPosition;
    protected bool isMoving;
    protected int pierceCount = 0;
    protected float lifeTime = 5f;

    protected Vector3 direction;

    protected CancellationTokenSource cts;

    protected virtual void Start()
    {
        isMoving = true;
        transform.position = startPosition;
        cts = new CancellationTokenSource();
        MoveProjectileAsync(cts.Token).Forget();
    }

    protected virtual async UniTaskVoid MoveProjectileAsync(CancellationToken token)
    {
        try
        {
            float traveledDistance = 0f;

            while (isMoving && !token.IsCancellationRequested)
            {
                if (gameObject == null || !isMoving)
                {
                    break;
                }

                if (!GameManager.Instance.isPaused)
                {
                    transform.position += speed * Time.deltaTime * direction;

                    traveledDistance = (transform.position - startPosition).magnitude;

                    if (traveledDistance >= maxDistance)
                    {
                        DestroyProjectile();
                        break;
                    }

                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                }
                else
                {
                    await UniTask.Yield(token);
                }
            }
        }
        catch (Exception ex)
        {
            print(ex.ToString() + " " + ex.StackTrace.ToString());
        }
    }

    public virtual void InitProjectile(Vector3 startPos, Vector3 targetPos, float spd, float dmg, float maxDist = 0f, int pierceCnt = 0, float lifetime = 5f)
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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent<MonsterBase>(out var monster))
        {
            monster.TakeDamage(damage);
            print($"TakeDamage : {gameObject.name}, Damage : {damage}");

            if (pierceCount > 0)
            {
                pierceCount--;
            }
            else
            {
                DestroyProjectile();
            }
        }
    }

    protected void DestroyProjectile()
    {

        if (!isMoving || gameObject == null)
        {
            return;
        }

        isMoving = false;

        CancelInvoke("DestroyProjectile");

        cts?.Cancel();
        cts?.Dispose();
        cts = null;

        ProjectileManager.Instance?.RemoveProjectile(this);

        Destroy(gameObject);
    }

    protected virtual void OnBecameInvisible()
    {
        if (pierceCount > 0) return;
        DestroyProjectile();
    }
}