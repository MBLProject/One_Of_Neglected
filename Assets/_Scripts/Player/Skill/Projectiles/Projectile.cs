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

    protected CancellationTokenSource cts; // 🔹 UniTask 취소용 토큰
    protected virtual void Start()
    {
        isMoving = true;
        transform.position = startPosition;
        cts = new CancellationTokenSource(); // 🔹 취소 토큰 초기화
        MoveProjectileAsync(cts.Token).Forget();
    }

    protected virtual async UniTaskVoid MoveProjectileAsync(CancellationToken token)
    {
        try
        {
            float traveledDistance = 0f;

            while (isMoving && !token.IsCancellationRequested)
            {
                // 오브젝트가 파괴되었으면 UniTask 실행을 멈추도록 확인
                if (gameObject == null || !isMoving)
                {
                    print("gameObject == null || !isMoving!!!");
                    break;
                }

                if (!GameManager.Instance.isPaused)
                {
                    Vector3 direction = (targetPosition - transform.position).normalized;
                    transform.position += speed * Time.deltaTime * direction;

                    traveledDistance = (transform.position - startPosition).magnitude;

                    //if (traveledDistance >= maxDistance)
                    //{
                    //    print("traveledDistance > maxDistance!!!");
                    //    print("DestroyProjectile!!!00");

                    //    break;
                    //}

                    await UniTask.Yield(PlayerLoopTiming.Update, token);  // 🔹 토큰을 통해 안전 종료 가능
                }
                else
                {
                    await UniTask.Yield(token); // 🔹 토큰 추가
                }
            }
            print("UniTask Quit!!!");
        }
        catch (Exception ex)
        {
            print(ex.ToString() + " " + ex.StackTrace.ToString());
        }
    }

    public void InitProjectile(Vector3 startPos, Vector3 targetPos, float spd, float dmg, float maxDist = 0f, int pierceCnt = 0)
    {
        this.startPosition = startPos;
        this.targetPosition = targetPos;
        speed = spd;
        maxDistance = maxDist;
        damage = dmg;
        pierceCount = pierceCnt;

        Vector3 direction = (targetPos - startPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (collision.TryGetComponent(out MonsterBase monster))
                monster.TakeDamage(damage);

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
        print("DestroyProjectile!!!!0000");

        if (!isMoving)
        {
            print("!isMoving!!");
            return;  // 🔹 오브젝트가 이미 파괴되었으면 처리하지 않음
        }
        if (gameObject == null)
        {
            print("gameObject == null!!");
            return;  // 🔹 오브젝트가 이미 파괴되었으면 처리하지 않음
        }
        print("DestroyProjectile!!!!1");
        isMoving = false;
        print("DestroyProjectile!!!!2");

        ProjectileManager.Instance?.RemoveProjectile(this);
        print("DestroyProjectile!!!!3");

        Destroy(gameObject);
    }

    protected virtual void OnBecameInvisible()
    {
        print("OnBecameInvisible!!!!!");

        if (pierceCount > 0) return;
        DestroyProjectile();
    }

    private void OnDestroy()
    {
        print($"DestroyProjectile!!!!4 : {isMoving}");
        Debug.Log("Stack Trace: " + Environment.StackTrace);

        //var token = this.GetCancellationTokenOnDestroy();
        cts?.Cancel();
        cts?.Dispose();
        if (cts != null) cts = null;

        ProjectileManager.Instance?.RemoveProjectile(this);

        print("DestroyProjectile!!!!5");
    }
}
