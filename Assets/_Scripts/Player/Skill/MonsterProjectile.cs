using UnityEngine;
using Cysharp.Threading.Tasks;
using static Enums;

public class MonsterProjectile : Projectile
{
    protected override void Start()
    {
        MoveProjectileAsync().Forget();
    }

    private bool isDestroyed = false;  // ?뚭눼 ?щ? 泥댄겕???뚮옒洹?

    protected override async UniTaskVoid MoveProjectileAsync()
    {
        try
        {
            while (!isDestroyed)  // isDestroyed 泥댄겕
            {
                if (this == null || gameObject == null)  // null 泥댄겕 異붽?
                {
                    return;
                }

                transform.position += direction * speed * Time.deltaTime;
                await UniTask.Yield();
            }
        }
        catch (System.Exception e)
        {
            // ?먮윭 濡쒓퉭 (?좏깮?ы빆)
            Debug.LogWarning($"Projectile movement interrupted: {e.Message}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 異⑸룎 泥섎━
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
        if (isDestroyed) return;  // ?대? ?뚭눼?섏뿀?ㅻ㈃ 由ы꽩

        isDestroyed = true;

        // ?꾨줈?앺???留ㅻ땲??먯꽌 ?쒓굅
        ProjectileManager.Instance?.RemoveProjectile(this);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }
}