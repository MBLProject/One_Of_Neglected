using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class UnitManager : MonoBehaviour
{
   private  static UnitManager instance;
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

    [Header("프리팹 설정")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject earlyNormalMonsterPrefab;
    [SerializeField] private GameObject rangedNormalMonsterPrefab;
    [SerializeField] private GameObject midNormalMonsterPrefab;
    [SerializeField] private GameObject lateNormalMonsterPrefab;
    [SerializeField] private GameObject damageUniqueMonsterPrefab;
    [SerializeField] private GameObject crowdControlUniqueMonsterPrefab;
    [SerializeField] private GameObject tankUniqueMonsterPrefab;

    [Header("스폰 설정")]
    [SerializeField] private float spawnRadius = 15f;
    [SerializeField] private float minSpawnDistance = 8f;
    [SerializeField] private float spawnInterval = 3f;

    [Header("게임 시간 설정")]
    [SerializeField] private float earlyGameDuration = 180f;  // 3분
    [SerializeField] private float midGameDuration = 420f;    // 7분
    [SerializeField] private float lateGameDuration = 600f;   // 10분

    private float gameTime = 0f;
    private float spawnTimer = 0f;
    private bool isGameStarted = false;

    private Player currentPlayer;
    private List<MonsterBase> activeMonsters = new List<MonsterBase>();
    private Camera mainCamera;

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
        // 항상 원거리 몬스터 스폰
        SpawnMonsterAtRandomPosition(MonsterType.RangedNormal);

        // 시간대별 몬스터 스폰
        if (gameTime <= earlyGameDuration)  // 0~3분
        {
            SpawnMonsterAtRandomPosition(MonsterType.EarlyNormal);
        }
        else if (gameTime <= midGameDuration)  // 3~7분
        {
            SpawnMonsterAtRandomPosition(MonsterType.MidNormal);
        }
        else if (gameTime <= lateGameDuration)  // 7~10분
        {
            SpawnMonsterAtRandomPosition(MonsterType.LateNormal);
        }
    }

    //플레이어 생성관련
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


    // 몬스터 생성 메서드
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

    // 랜덤 위치에 몬스터 생성
    public MonsterBase SpawnMonsterAtRandomPosition(MonsterType type)
    {
        Vector2 randomPosition = GetRandomSpawnPosition();
        return SpawnMonster(type, randomPosition);
    }

    // 몬스터 제거
    public void RemoveMonster(MonsterBase monster)
    {
        if (monster != null)
        {
            activeMonsters.Remove(monster);
        }
    }

    // 모든 몬스터 제거
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

    // 랜덤 스폰 위치 계산
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

    // 몬스터 타입에 따른 프리팹 반환
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

    // 활성화된 몬스터 수 반환
    public int GetActiveMonsterCount() => activeMonsters.Count;

    // 특정 범위 내의 몬스터 찾기
    public List<MonsterBase> GetMonstersInRange(Vector2 position, float range)
    {
        return activeMonsters.FindAll(monster =>
            Vector2.Distance(position, monster.transform.position) <= range);
    }


    public float GetGameTime() => gameTime;
    public Player GetPlayer() => currentPlayer;
}

// 몬스터 타입 열거형
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



