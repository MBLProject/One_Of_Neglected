using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Projectile : MonoBehaviour
{
    public Vector3 playerPosition;
    public Vector3 direction;
    public float speed;

    public float maxDistance = 10f;  // 최대 사거리
    private Vector3 startPosition;   // 투사체의 시작 위치

    void Start()
    {
        startPosition = transform.position;  // 투사체의 시작 위치 저장
    }

    void Update()
    {
        float distanceTraveled = (transform.position - startPosition).magnitude;

        if (distanceTraveled > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public Projectile(Vector3 startPos, Vector3 dir)
    {
        playerPosition = startPos;
        direction = dir;
        speed = 1f;
    }

    public Projectile(Vector3 startPos, Vector3 dir, float spd)
    {
        playerPosition = startPos;
        direction = dir;
        speed = spd;
    }
}
