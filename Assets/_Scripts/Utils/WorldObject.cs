using UnityEngine;
using static Enums;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class WorldObject : MonoBehaviour
{
    public WorldObjectType objectType;
    private int ExpAmount;

    //아이템 합쳐지는 시간
    private float expConcentrationInterval = 25f;
    private float expConcentrationRadius = 1f;
    private LayerMask expLayer;
    private bool isConcentrating = false;
    private CancellationTokenSource cts;


    public int GetExpAmount()
    {
        return ExpAmount;
    }


    private void Awake()
    {
        switch (objectType)
        {
            case WorldObjectType.ExpBlue:
                ExpAmount = 10;
                if (!isConcentrating)
                {
                    cts = new CancellationTokenSource();
                    StartExpConcentration(cts.Token).Forget();
                    isConcentrating = true;
                }
                break;
        }

        expLayer = LayerMask.GetMask("EnvObject");
    }

    private async UniTaskVoid StartExpConcentration(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(expConcentrationInterval), cancellationToken: cancellationToken);
                
                if (this == null || !gameObject || GameManager.Instance.isPaused) 
                    continue;

                await ConcentrateNearbyExp(cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {

        }
        catch (Exception ex)
        {
            Debug.LogError($"StartExpConcentration 에러: {ex}");
        }
    }

    private async UniTask ConcentrateNearbyExp(CancellationToken cancellationToken)
    {
        if (objectType != WorldObjectType.ExpBlue || !gameObject) return;

        var colliders = Physics2D.OverlapCircleAll(
            transform.position, 
            expConcentrationRadius,
            expLayer
        );

        int expCount = 0;
        var expObjects = new System.Collections.Generic.List<WorldObject>();

        foreach (var collider in colliders)
        {
            if (cancellationToken.IsCancellationRequested) return;

            WorldObject worldObj = collider.GetComponent<WorldObject>();
            
            if (worldObj != null && worldObj != this && worldObj.objectType == WorldObjectType.ExpBlue)
            {
                expObjects.Add(worldObj);
                expCount++;
            }
        }
        if (expCount > 0 && gameObject != null)
        {
            GameObject expBlackPrefab = Resources.Load<GameObject>("Using/Env/Env_BlueExp");
            Vector3 spawnPosition = transform.position + (Vector3)(UnityEngine.Random.insideUnitCircle * 1f);
            
            try
            {
                GameObject blackExp = Instantiate(expBlackPrefab, spawnPosition, Quaternion.identity);

                foreach (var expObj in expObjects)
                {
                    if (cancellationToken.IsCancellationRequested) return;

                    if (expObj != null && expObj.gameObject != null)
                    {
                        Destroy(expObj.gameObject);
                        await UniTask.Yield(cancellationToken);
                    }
                }

                if (gameObject != null)
                {
                    Destroy(gameObject);
                }

            }
            catch (Exception ex)
            {
                Debug.LogError($"ConcentrateNearbyExp 에러: {ex}");
            }
        }
    }

    public void selfDestroy()
    {
        cts?.Cancel();
        Destroy(gameObject);
    }

    //private void OnDrawGizmos()
    //{
    //    // 디버그용 시각화
    //    if (objectType == WorldObjectType.ExpBlue)
    //    {
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawWireSphere(transform.position, expConcentrationRadius);
    //    }
    //}
    private void OnDestroy()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = null;
    }
}
