using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    [Header("?꾨━???ㅼ젙")]
    [SerializeField] private GameObject earlyNormalMonsterPrefab;
    [SerializeField] private GameObject rangedNormalMonsterPrefab;
    [SerializeField] private GameObject midNormalMonsterPrefab;
    [SerializeField] private GameObject lateNormalMonsterPrefab;
    [SerializeField] private GameObject damageUniqueMonsterPrefab;
    [SerializeField] private GameObject crowdControlUniqueMonsterPrefab;
    [SerializeField] private GameObject tankUniqueMonsterPrefab;
    [SerializeField] private GameObject bossMonsterPrefab;

    [Header("?ㅽ룿 ?ㅼ젙")]
    [SerializeField] private float spawnRadius = 15f;
    [SerializeField] private float minSpawnDistance = 8f;
    [SerializeField] private float spawnInterval = 0.5f;
    private float nextSpawnTime = 0f;

    private BossMonster currentBoss;
    private bool isGameStarted = false;
    private Player currentPlayer;
    private List<MonsterBase> activeMonsters = new List<MonsterBase>();
    private Camera mainCamera;

    public Player GetPlayer() => currentPlayer;

    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;

        if (TimeManager.Instance != null)
        {
            //TimeManager.Instance.OnThirtySecondsPassed += SpawnUniqueMonster;
            TimeManager.Instance.OnMinutePassed += SpawnStrongMonsters;
        }

        if (DataManager.Instance.classSelect_Num == 0)
        {
            DataManager.Instance.classSelect_Num = 1;
        }
        SpawnPlayerByType(DataManager.Instance.classSelect_Num);
    }

    private MonsterType currentNormalMonsterType = MonsterType.EarlyNormal;


    private void Update()
    {
        if (!isGameStarted || GameManager.Instance.isPaused) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnMonsterAtRandomPosition(MonsterType.RangedNormal);
            SpawnMonsterAtRandomPosition(currentNormalMonsterType);

            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void OnEnable()
    {
        if (TimeManager.Instance != null)
        {
            //TimeManager.Instance.OnThirtySecondsPassed += SpawnUniqueMonster;
            TimeManager.Instance.OnMinutePassed += SpawnStrongMonsters;
        }
    }

    private void OnDisable()
    {
        if (TimeManager.Instance != null)
        {
            //TimeManager.Instance.OnThirtySecondsPassed -= SpawnUniqueMonster;
            TimeManager.Instance.OnMinutePassed -= SpawnStrongMonsters;
        }
    }

    //private void SpawnUniqueMonster()
    //{
    //    float gameTime = TimeManager.Instance.GameTime;
    //    MonsterType monsterType;

    //    if (gameTime <= 180f)        // 0~3遺?
    //    {
    //        monsterType = MonsterType.DamageUnique;
    //    }
    //    else if (gameTime <= 420f)   // 3~7遺?
    //    {
    //        monsterType = MonsterType.CrowdControlUnique;
    //    }
    //    else                         // 7遺??댄썑
    //    {
    //        monsterType = MonsterType.TankUnique;
    //    }

    //    SpawnMonsterAtRandomPosition(monsterType);
    //}

    private void SpawnStrongMonsters()
    {
        float gameTime = TimeManager.Instance.GameTime;

        if (gameTime <= 180f)        // 0~3遺?
        {
            currentNormalMonsterType = MonsterType.EarlyNormal;
            Debug.Log("[UnitManager] 紐ъ뒪?????蹂寃? EarlyNormal");
        }
        else if (gameTime <= 420f)   // 3~7遺?
        {
            currentNormalMonsterType = MonsterType.MidNormal;
            Debug.Log("[UnitManager] 紐ъ뒪?????蹂寃? MidNormal");
        }
        else if (gameTime <= 600f)   // 7~10遺?
        {
            currentNormalMonsterType = MonsterType.LateNormal;
        }
        else if (gameTime >= 600f)   // 10遺??댁긽
        {
            Debug.Log("[UnitManager] 蹂댁뒪 ?섏씠利??쒖옉!");
            ClearAllMonsters();

            Vector2 spawnPosition = GetBossSpawnPosition();
            GameObject bossObj = Instantiate(bossMonsterPrefab, spawnPosition, Quaternion.identity);

            isGameStarted = false;
        }
    }

    public Player SpawnPlayerByType(int PlayerType)
    {
        if (currentPlayer != null)
        {
            return currentPlayer;
        }

        GameObject _player;
        //Enum泥섎━ ?대룄 ?좉굅媛숆릿 ??
        if (PlayerType == 1)
        {
            // 1. ?꾩궗
            _player = Resources.Load<GameObject>("Using/Player/Warrior");
        }
        else if (PlayerType == 2)
        {
            // 2. 沅곸닔
            _player = Resources.Load<GameObject>("Using/Player/Archer");
        }
        else
        {
            // 3. 踰뺤궗
            _player = Resources.Load<GameObject>("Using/Player/Magician");
        }


        GameObject playerObj = Instantiate(_player, Vector2.zero, Quaternion.identity);
        playerObj.AddComponent<SkillDispenser>();
        currentPlayer = playerObj.GetComponent<Player>();

        return currentPlayer;
    }

    public void StartGame()
    {
        isGameStarted = true;
        ClearAllMonsters();
        currentNormalMonsterType = MonsterType.EarlyNormal;
        nextSpawnTime = Time.time;
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnMinutePassed += SpawnStrongMonsters;
        }
    }

    public void PauseGame()
    {
        isGameStarted = false;
    }

    public void ResumeGame()
    {
        isGameStarted = true;
    }
    private Vector2 GetBossSpawnPosition()
    {
        if (currentPlayer == null) return Vector2.zero;

        float angle = Random.Range(0f, 360f);
        float distance = spawnRadius;  // 理쒕? 嫄곕━???뚰솚

        return (Vector2)currentPlayer.transform.position + new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad) * distance,
            Mathf.Sin(angle * Mathf.Deg2Rad) * distance
        );
    }
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

    public MonsterBase SpawnMonsterAtRandomPosition(MonsterType type)
    {
        Vector2 randomPosition = GetRandomSpawnPosition();
        return SpawnMonster(type, randomPosition);
    }

    public void RemoveMonster(MonsterBase monster)
    {
        if (monster != null)
        {
            activeMonsters.Remove(monster);
        }
    }

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

    private GameObject GetMonsterPrefab(MonsterType type)
    {
        if (type == MonsterType.EarlyNormal)
            return earlyNormalMonsterPrefab;
        else if (type == MonsterType.RangedNormal)
            return rangedNormalMonsterPrefab;
        else if (type == MonsterType.MidNormal)
            return midNormalMonsterPrefab;
        else if (type == MonsterType.LateNormal)
            return lateNormalMonsterPrefab;
        else if (type == MonsterType.DamageUnique)
            return damageUniqueMonsterPrefab;
        else if (type == MonsterType.CrowdControlUnique)
            return crowdControlUniqueMonsterPrefab;
        else if (type == MonsterType.TankUnique)
            return tankUniqueMonsterPrefab;
        else
            return null;
    }

    public int GetActiveMonsterCount() => activeMonsters.Count;

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

    public List<Vector3> GetMonsterPositionsInRange(float minRange, float maxRange)
    {
        List<Vector3> positions = new List<Vector3>();

        foreach (var monster in activeMonsters)
        {
            if (monster == null) continue;

            float distance = Vector2.Distance(currentPlayer.transform.position, monster.transform.position);
            if (distance >= minRange && distance <= maxRange)
            {
                positions.Add(monster.transform.position);
            }
        }

        // 嫄곕━?쒖쑝濡??뺣젹
        positions.Sort((a, b) =>
        {
            float distanceA = Vector2.Distance(currentPlayer.transform.position, a);
            float distanceB = Vector2.Distance(currentPlayer.transform.position, b);
            return distanceA.CompareTo(distanceB);
        });

        return positions;
    }

    public Vector3? GetNearestMonsterPosition()
    {
        Vector3? nearestPosition = null;
        float nearestDistance = float.MaxValue;

        foreach (var monster in activeMonsters)
        {
            if (monster == null) continue;

            float distance = Vector2.Distance(currentPlayer.transform.position, monster.transform.position);
            if (distance < nearestDistance)
            {
                nearestPosition = monster.transform.position;
                nearestDistance = distance;
            }
        }

        return nearestPosition;
    }
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


