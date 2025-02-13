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

    private bool isDestroyed = false;  // ???àº ??? Ôß£ÎåÑÍ≤?????òíÊ¥?

    protected override async UniTaskVoid MoveProjectileAsync()
    {
        try
        {
            while (!isDestroyed)  // isDestroyed Ôß£ÎåÑÍ≤?
            {
                if (this == null || gameObject == null)  // null Ôß£ÎåÑÍ≤??∞Î∂Ω?
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
            // ?Î®?ú≠ Êø°Ïíì??(?Ï¢èÍπÆ??Îπ?
            Debug.LogWarning($"Projectile movement interrupted: {e.Message}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ?∞‚ë∏Î£?Ôß£ÏÑé??
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
        if (isDestroyed) return;  // ???? ???àº??èÎ???ª„àÉ ?±—ãÍΩ©

        isDestroyed = true;

        // ?Íæ®Ï§à?????Ôßç„Öª????Î®?Ωå ??ìÍµÖ
        ProjectileManager.Instance?.RemoveProjectile(this);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }
}