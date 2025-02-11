using UnityEngine;
using Cysharp.Threading.Tasks;
using static Enums;

public class MonsterProjectile : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float damage;
    private bool isDestroyed = false;  // 파괴 여부 체크용 플래그

    public void InitProjectile(Vector3 startPos, Vector3 direction, float speed, float damage)
    {
        transform.position = startPos;
        this.direction = direction;
        this.speed = speed;
        this.damage = damage;

        MoveProjectileAsync().Forget();
    }

    private async UniTaskVoid MoveProjectileAsync()
    {
        try
        {
            while (!isDestroyed)  // isDestroyed 체크
            {
                if (this == null || gameObject == null)  // null 체크 추가
                {
                    return;
                }

                transform.position += direction * speed * Time.deltaTime;
                await UniTask.Yield();
            }
        }
        catch (System.Exception e)
        {
            // 에러 로깅 (선택사항)
            Debug.LogWarning($"Projectile movement interrupted: {e.Message}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌 처리
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
        if (isDestroyed) return;  // 이미 파괴되었다면 리턴

        isDestroyed = true;

        // 프로젝타일 매니저에서 제거
        ProjectileManager.Instance?.RemoveProjectile(this);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }
}