using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManagerTest : MonoBehaviour
{
    private void Start()
    {
        // 플레이어 생성 테스트
        Player player = UnitManager.Instance.SpawnPlayer(Vector2.zero);
        Debug.Log("플레이어 생성됨: " + (player != null));
    }
}
