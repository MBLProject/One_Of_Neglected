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

    private bool isDestroyed = false;  // ???댘 ??? 筌ｋ똾寃?????삋域?

    protected override async UniTaskVoid MoveProjectileAsync()
    {
        try
        {
            while (!isDestroyed)  // isDestroyed 筌ｋ똾寃?
            {
                if (this == null || gameObject == null)  // null 筌ｋ똾寃??곕떽?
                {
                    return;
                }

                transform.position += direction * speed * Time.deltaTime;
                await UniTask.Yield();
            }
        }
        catch (System.Exception e)
        {
            // ?癒?쑎 嚥≪뮄??(?醫뤾문??鍮?
            Debug.LogWarning($"Projectile movement interrupted: {e.Message}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ?겸뫖猷?筌ｌ꼶??
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
        if (isDestroyed) return;  // ??? ???댘??뤿???삠늺 ?귐뗪쉘

        isDestroyed = true;

        // ?袁⑥쨮?????筌띲끇????癒?퐣 ??볤탢
        ProjectileManager.Instance?.RemoveProjectile(this);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }
}