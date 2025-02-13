using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class UnitManager : MonoBehaviour
{
    private static UnitManager instance;
    public static UnitManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UnitManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("UnitManager");
                    instance = go.AddComponent<UnitManager>();
                }
            }
            return instance;
        }
    }

    [Header("?�리???�정")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject earlyNormalMonsterPrefab;
    [SerializeField] private GameObject rangedNormalMonsterPrefab;
    [SerializeField] private GameObject midNormalMonsterPrefab;
    [SerializeField] private GameObject lateNormalMonsterPrefab;
    [SerializeField] private GameObject damageUniqueMonsterPrefab;
    [SerializeField] private GameObject crowdControlUniqueMonsterPrefab;
    [SerializeField] private GameObject tankUniqueMonsterPrefab;

    [Header("?�폰 ?�정")]
    [SerializeField] private float spawnRadius = 15f;
    [SerializeField] private float minSpawnDistance = 8f;
    [SerializeField] private float spawnInterval = 3f;

    [Header("게임 ?�간 ?�정")]
    [SerializeField] private float earlyGameDuration = 180f;  // 3�?
    [SerializeField] private float midGameDuration = 420f;    // 7�?
    [SerializeField] private float lateGameDuration = 600f;   // 10�?

    [Header("스킬 범위 설정")]
    [SerializeField] private float minRange = 0.5f;  // 최소 스킬 사용 거리
    private float gameTime = 0f;
    private float spawnTimer = 0f;
    private bool isGameStarted = false;

    private Player currentPlayer;
    private List<MonsterBase> activeMonsters = new List<MonsterBase>();
    private Camera mainCamera;

    public float GetGameTime() => gameTime;
    public Player GetPlayer() => currentPlayer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!isGameStarted) return;

        gameTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnMonsters();
            spawnTimer = 0f;
        }
    }

    private void SpawnMonsters()
    {
        // ??�� ?�거�?몬스???�폰
        SpawnMonsterAtRandomPosition(MonsterType.RangedNormal);

        // ?�간??�?몬스???�폰
        if (gameTime <= earlyGameDuration)  // 0~3�?
        {
            SpawnMonsterAtRandomPosition(MonsterType.EarlyNormal);
        }
        else if (gameTime <= midGameDuration)  // 3~7�?
        {
            SpawnMonsterAtRandomPosition(MonsterType.MidNormal);
        }
        else if (gameTime <= lateGameDuration)  // 7~10�?
        {
            SpawnMonsterAtRandomPosition(MonsterType.LateNormal);
        }
    }

    //?�레?�어 ?�성�???
    public Player SpawnPlayer(Vector2 position)
    {
        if (currentPlayer != null)
        {
            return currentPlayer;
        }

        GameObject playerObj = Instantiate(playerPrefab, position, Quaternion.identity);
        playerObj.AddComponent<SkillDispesner>();

        currentPlayer = playerObj.GetComponent<Player>();

        return currentPlayer;
    }
    public void StartGame()
    {
        gameTime = 0f;
        spawnTimer = 0f;
        isGameStarted = true;
        ClearAllMonsters();
    }

    public void PauseGame()
    {
        isGameStarted = false;
    }

    public void ResumeGame()
    {
        isGameStarted = true;
    }


    // 몬스???�성 메서??
    public MonsterBase SpawnMonster(MonsterType type, Vector2 position)
    {
        GameObject prefab = GetMonsterPrefab(type);
        if (prefab == null) return null;

        GameObject monsterObj = Instantiate(prefab, position, Quaternion.identity);
        MonsterBase monster = monsterObj.GetComponent<MonsterBase>();

        if (monster != null)
        {
            activeMonsters.Add(monster);
        }

        return monster;
    }

    // ?�덤 ?�치??몬스???�성
    public MonsterBase SpawnMonsterAtRandomPosition(MonsterType type)
    {
        Vector2 randomPosition = GetRandomSpawnPosition();
        return SpawnMonster(type, randomPosition);
    }

    // 몬스???�거
    public void RemoveMonster(MonsterBase monster)
    {
        if (monster != null)
        {
            activeMonsters.Remove(monster);
        }
    }

    // 모든 몬스???�거
    public void ClearAllMonsters()
    {
        foreach (var monster in activeMonsters.ToArray())
        {
            if (monster != null)
            {
                Destroy(monster.gameObject);
            }
        }
        activeMonsters.Clear();
    }

    // ?�덤 ?�폰 ?�치 계산
    private Vector2 GetRandomSpawnPosition()
    {
        if (mainCamera == null) return Vector2.zero;

        Vector2 cameraPosition = mainCamera.transform.position;
        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(minSpawnDistance, spawnRadius);

        return cameraPosition + new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad) * distance,
            Mathf.Sin(angle * Mathf.Deg2Rad) * distance
        );
    }

    // 몬스?????�에 ?�른 ?�리??반환
    private GameObject GetMonsterPrefab(MonsterType type)
    {
        return type switch
        {
            MonsterType.EarlyNormal => earlyNormalMonsterPrefab,
            MonsterType.RangedNormal => rangedNormalMonsterPrefab,
            MonsterType.MidNormal => midNormalMonsterPrefab,
            MonsterType.LateNormal => lateNormalMonsterPrefab,
            MonsterType.DamageUnique => damageUniqueMonsterPrefab,
            MonsterType.CrowdControlUnique => crowdControlUniqueMonsterPrefab,
            MonsterType.TankUnique => tankUniqueMonsterPrefab,
            _ => null
        };
    }

    public int GetActiveMonsterCount() => activeMonsters.Count;

    /// <summary>
    /// 범위 사이에 있는 몬스터를 가까운 순서로 리스트화 정렬 반환
    /// </summary>
    /// <param name="minRange"></param>
    /// <param name="maxRange"></param>
    /// <returns>거리순으로 정렬된 몬스터 리스트</returns>
    public List<MonsterBase> GetMonstersInRange(float minRange, float maxRange)
    {
        var monstersInRange = activeMonsters.FindAll(monster =>
        {
            if (monster == null) return false;
            float distance = Vector2.Distance(currentPlayer.transform.position, monster.transform.position);
            return distance >= minRange && distance <= maxRange;
        });

        monstersInRange.Sort((a, b) =>
        {
            float distanceA = Vector2.Distance(currentPlayer.transform.position, a.transform.position);
            float distanceB = Vector2.Distance(currentPlayer.transform.position, b.transform.position);
            return distanceA.CompareTo(distanceB);
        });

        return monstersInRange;
    }

    /// <summary>
    /// 몬스터 리스트에서 가장 가까운 몬스터 반환
    /// </summary>
    public MonsterBase GetNearestMonster()
    {
        MonsterBase nearestMonster = null;
        float nearestDistance = float.MaxValue;

        foreach (var monster in activeMonsters)
        {
            if (monster == null) continue;

            float distance = Vector2.Distance(currentPlayer.transform.position, monster.transform.position);
            if (distance < nearestDistance)
            {
                nearestMonster = monster;
                nearestDistance = distance;
            }
        }
        return nearestMonster;
    }

    //public List<MonsterBase> GetMonstersInMinMaxRange(Vector2 position)
    //{
    //    return activeMonsters.FindAll(monster => 
    //    {
    //        if (monster == null) return false;
    //        float distance = Vector2.Distance(position, monster.transform.position);
    //        return distance >= minRange && distance <= maxRange;
    //    });
    //}

    //public MonsterBase GetNearestMonsterInMinMaxRange(Vector2 position)
    //{
    //    MonsterBase nearestMonster = null;
    //    float nearestDistance = float.MaxValue;

    //    foreach (var monster in activeMonsters)
    //    {
    //        if (monster == null) continue;

    //        float distance = Vector2.Distance(position, monster.transform.position);
    //        if (distance >= minRange && distance <= maxRange && distance < nearestDistance)
    //        {
    //            nearestMonster = monster;
    //            nearestDistance = distance;
    //        }
    //    }

    //    return nearestMonster;
    //}
}

public enum MonsterType
{
    EarlyNormal,
    RangedNormal,
    MidNormal,
    LateNormal,
    DamageUnique,
    CrowdControlUnique,
    TankUnique
}



