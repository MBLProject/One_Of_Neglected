using UnityEngine;
using Cysharp.Threading.Tasks;
using static Enums;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    protected float damage;

    public float maxDistance = 10f;
    protected Vector3 startPosition;

    private bool isMoving = true;

    protected virtual void Start()
    {
        transform.position = startPosition;
        MoveProjectileAsync().Forget();
    }

    protected virtual async UniTaskVoid MoveProjectileAsync()
    {
        float distanceTraveled = 0f;

        while (isMoving)
        {
            if (!GameManager.Instance.isPaused)
            {
                transform.position += speed * Time.deltaTime * direction;

                distanceTraveled = (transform.position - startPosition).magnitude;

                if (distanceTraveled > maxDistance)
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
    public void InitProjectile(Vector3 startPos, Vector3 dir, float spd, float dmg, float maxDist = 0f)
    {
        this.startPosition = startPos;
        direction = dir;
        speed = spd;
        maxDistance = maxDist;
        damage = dmg;

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
