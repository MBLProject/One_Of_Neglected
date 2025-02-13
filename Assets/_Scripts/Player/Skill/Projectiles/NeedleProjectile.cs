using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;


public class NeedleProjectile : Projectile
{
    protected override async UniTaskVoid MoveProjectileAsync(CancellationToken token)
    {
        while (isMoving)
        {
            var groundPosition = GetGroundPosition();
            SpawnNeedleAtPosition(groundPosition);

            await UniTask.Delay(1000);
        }
    }

    private Vector3 GetGroundPosition()
    {
        // ???꾩룆理??"?꾩룆??? ?熬곣뫚?????ｌ뫒亦??濡ル츎 ?β돦裕뉐퐲???뚮뿭寃?
        return transform.position; // ???곕뻣???熬곣뫗???熬곣뫚???꾩룇瑗??
    }

    private void SpawnNeedleAtPosition(Vector3 position)
    {
        // ?낅슣?섇젆源띿???熬곣뫚????띠럾???? ??怨쀫꼶??濡ル츎 ?β돦裕뉐퐲???뚮뿭寃?
    }

}