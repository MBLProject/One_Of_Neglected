using UnityEngine;
using Cysharp.Threading.Tasks;
using static Enums;

public class Projectile : MonoBehaviour
{
    public float speed;

    protected float damage;

    public float maxDistance = 10f;
    protected Vector3 startPosition;

    protected Vector3 targetPosition;

    private bool isMoving = true;

    protected virtual void Start()
    {
        transform.position = startPosition;
        MoveProjectileAsync().Forget();
    }

    protected virtual async UniTaskVoid MoveProjectileAsync()
    {
        float traveledDistance = 0f;

        while (isMoving)
        {
            if (!GameManager.Instance.isPaused)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                transform.position += speed * Time.deltaTime * direction;

                traveledDistance = (transform.position - startPosition).magnitude;

                if (traveledDistance > maxDistance)
                {
                    Destroy(gameObject);
                    break;
                }

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }
    public void InitProjectile(Vector3 startPos, Vector3 targetPos, float spd, float dmg, float maxDist = 0f)
    {
        this.startPosition = startPos;
        this.targetPosition = targetPos;
        speed = spd;
        maxDistance = maxDist;
        damage = dmg;

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

            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        if (!isMoving) return;

        isMoving = false;

        ProjectileManager.Instance?.RemoveProjectile(this);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        isMoving = false;
    }
}
