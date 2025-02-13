using UnityEngine;
using Cysharp.Threading.Tasks;
using static Enums;

public class MonsterProjectile : Projectile
{
    protected override void Start()
    {
        transform.position = startPosition;
        MoveProjectileAsync().Forget();
    }

    private bool isDestroyed = false;  // ???�� ??? 泥댄�?????���?

    protected override async UniTaskVoid MoveProjectileAsync()
    {
        try
        {
            while (!isDestroyed)  // isDestroyed 泥댄�?
            {
                if (this == null || gameObject == null)  // null 泥댄�??�붽?
                {
                    return;
                }

                Vector3 direction = (targetPosition - transform.position).normalized;
                transform.position += speed * Time.deltaTime * direction;

                await UniTask.Yield();
            }
        }
        catch (System.Exception e)
        {
            // ?�?�� 濡쒓??(?좏깮??�?
            Debug.LogWarning($"Projectile movement interrupted: {e.Message}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ?�⑸�?泥섎??
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            DestroyProjectile();
        }
        //else if (collision.CompareTag("Wall"))
        //{
        //    DestroyProjectile();
        //}
    }

    private void DestroyProjectile()
    {
        if (isDestroyed) return;  // ???? ???��??��???�㈃ ?�ы꽩

        isDestroyed = true;

        // ?꾨줈?????留ㅻ????�?�� ??�굅
        ProjectileManager.Instance?.RemoveProjectile(this);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }
}