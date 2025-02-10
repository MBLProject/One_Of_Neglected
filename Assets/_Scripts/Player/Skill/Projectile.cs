using UnityEngine;
using Cysharp.Threading.Tasks;
using static Enums;

public class Projectile : MonoBehaviour
{
    public Vector3 playerPosition;
    public Vector3 direction;
    public float speed;

    public float maxDistance = 10f;
    private Vector3 startPosition;

    private bool isMoving = true;

    void Start()
    {
        startPosition = transform.position;
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
    public void InitProjectile(Vector3 startPos, Vector3 dir, float spd)
    {
        playerPosition = startPos;
        direction = dir;
        speed = spd;
    }
}
