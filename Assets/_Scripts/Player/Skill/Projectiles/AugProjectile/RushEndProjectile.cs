using Cysharp.Threading.Tasks;
using UnityEngine;

public class RushEndProjectile : PlayerProjectile
{
    protected override void Start()
    {
        pType = projType.Melee;
        isMoving = true;
        transform.position = startPosition;

        transform.rotation = Quaternion.identity;
        speed = 0f;
    }
    protected override async UniTaskVoid MoveProjectileAsync(System.Threading.CancellationToken token)
    {
        transform.position = startPosition;

        while (isMoving && !token.IsCancellationRequested)
        {
            if (gameObject == null || !isMoving)
            {
                break;
            }

            await UniTask.Yield(token);
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (collision.TryGetComponent(out MonsterBase monster))
            {
                float finalFinalDamage = UnityEngine.Random.value < stats.critical ? stats.finalDamage * stats.cATK : stats.finalDamage;
                monster.TakeDamage(finalFinalDamage);
                DataManager.Instance.AddDamageData(finalFinalDamage, Enums.AugmentName.Shielder);
                
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
    }
}
