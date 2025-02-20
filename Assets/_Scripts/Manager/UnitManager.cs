using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    [Header("??리????정")]
    [SerializeField] private GameObject earlyNormalMonsterPrefab;
    [SerializeField] private GameObject rangedNormalMonsterPrefab;
    [SerializeField] private GameObject midNormalMonsterPrefab;
    [SerializeField] private GameObject lateNormalMonsterPrefab;
    [SerializeField] private GameObject damageUniqueMonsterPrefab;
    [SerializeField] private GameObject crowdControlUniqueMonsterPrefab;
    [SerializeField] private GameObject tankUniqueMonsterPrefab;
    [SerializeField] private GameObject bossMonsterPrefab;

    [Header("??폰 ??정")]
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
        // 테스트용 키 입력
        if (Input.GetKeyDown(KeyCode.U))  // U키: 유니크 몬스터 소환 테스트
        {
            Debug.Log("유니크 몬스터 소환 테스트 시작");
            SpawnUniqueMonster();
        }

        if (Input.GetKeyDown(KeyCode.T))  // T키: 탱크 유니크 몬스터 진형 테스트
        {
            Debug.Log("탱크 유니크 몬스터 진형 테스트 시작");
            SpawnTankUniquesInFormation();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))  // 1키: 세로 진형
        {
            Debug.Log("탱크 유니크 몬스터 세로 진형 테스트");
            SpawnVerticalFormation(currentPlayer.transform.position);  // 플레이어 위치 사용
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))  // 2키: 가로 진형
        {
            Debug.Log("탱크 유니크 몬스터 가로 진형 테스트");
            SpawnHorizontalFormation(currentPlayer.transform.position);  // 플레이어 위치 사용
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))  // 3키: 원형 진형
        {
            Debug.Log("탱크 유니크 몬스터 원형 진형 테스트");
            SpawnCircularFormation(currentPlayer.transform.position);  // 플레이어 위치 사용
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnOneMinFiftySecondsPassed -= SpawnUniqueMonster;
            TimeManager.Instance.OnMinutePassed -= SpawnStrongMonsters;
        }
    }

    private void SpawnUniqueMonster()
    {
        int randomValue = UnityEngine.Random.Range(1, 101);
        MonsterType monsterType;

        if (randomValue <= 40)
        {
            monsterType = MonsterType.DamageUnique;
            SpawnMonsterAtRandomPosition(monsterType);
            Debug.Log("[UnitManager] ?????? ????? ???? ?????");
        }
        else if (randomValue <= 80)
        {
            monsterType = MonsterType.CrowdControlUnique;
            SpawnMonsterAtRandomPosition(monsterType);
            Debug.Log("[UnitManager] CC ????? ???? ?????");
        }
        else
        {
            SpawnTankUniquesInFormation();
        }
    }
    private void SpawnTankUniquesInFormation()
    {
        if (currentPlayer == null) return;

        int formationRoll = Random.Range(1, 101);
        Vector2 spawnCenter = currentPlayer.transform.position;

        if (formationRoll <= 40)  // 40% 확률로 세로 진형
        {
            SpawnVerticalFormation(spawnCenter);
            Debug.Log("탱크 유니크 몬스터 세로 진형 소환");
        }
        else if (formationRoll <= 80)  // 40% 확률로 가로 진형
        {
            SpawnHorizontalFormation(spawnCenter);
            Debug.Log("탱크 유니크 몬스터 가로 진형 소환");
        }
        else  // 20% 확률로 원형 진형
        {
            SpawnCircularFormation(spawnCenter);
            Debug.Log("탱크 유니크 몬스터 원형 진형 소환");
        }
    }
    private void SpawnVerticalFormation(Vector2 playerPos)
    {
        float spacing = 0.5f; // 몬스터 간 간격
        int monstersPerLine = 8; // 한 줄당 몬스터 수
        float distanceFromPlayer = 4f; // 플레이어로부터의 거리

        // 한쪽 방향에만 8마리 소환 (왼쪽 또는 오른쪽)
        int side = Random.Range(0, 2) * 2 - 1; // -1 또는 1
        float xOffset = side * distanceFromPlayer;

        for (int i = 0; i < monstersPerLine; i++)
        {
            float yOffset = (i - (monstersPerLine - 1) / 2f) * spacing;
            Vector2 spawnPos = playerPos + new Vector2(xOffset, yOffset);
            MonsterBase monster = SpawnMonster(MonsterType.TankUnique, spawnPos);
            if (monster != null)
            {
                Debug.Log($"탱크 몬스터 생성 - 위치: {spawnPos}, 플레이어와의 거리: {Vector2.Distance(playerPos, spawnPos)}");
            }
        }
    }

    private void SpawnHorizontalFormation(Vector2 playerPos)
    {
        float spacing = 0.5f; // 몬스터 간 간격
        int monstersPerLine = 8; // 한 줄당 몬스터 수
        float distanceFromPlayer = 4f; // 플레이어로부터의 거리

        // 한쪽 방향에만 8마리 소환 (위 또는 아래)
        int side = Random.Range(0, 2) * 2 - 1; // -1 또는 1
        float yOffset = side * distanceFromPlayer;

        for (int i = 0; i < monstersPerLine; i++)
        {
            float xOffset = (i - (monstersPerLine - 1) / 2f) * spacing;
            Vector2 spawnPos = playerPos + new Vector2(xOffset, yOffset);
            MonsterBase monster = SpawnMonster(MonsterType.TankUnique, spawnPos);
            if (monster != null)
            {
                Debug.Log($"탱크 몬스터 생성 - 위치: {spawnPos}, 플레이어와의 거리: {Vector2.Distance(playerPos, spawnPos)}");
            }
        }
    }

    private void SpawnCircularFormation(Vector2 playerPos)
    {
        int monsterCount = 8; // 총 16마리
        float radius = 4f; // 원형 진형의 반지름 (플레이어로부터의 거리)

        for (int i = 0; i < monsterCount; i++)
        {
            float angle = i * (360f / monsterCount);
            Vector2 spawnPos = playerPos + new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );
            MonsterBase monster = SpawnMonster(MonsterType.TankUnique, spawnPos);
            if (monster != null)
            {
                Debug.Log($"탱크 몬스터 생성 - 위치: {spawnPos}, 플레이어와의 거리: {Vector2.Distance(playerPos, spawnPos)}");
            }
        }
    }
    private void SpawnStrongMonsters()
    {
        float gameTime = TimeManager.Instance.GameTime;

        if (gameTime <= 180f)
        {
            currentNormalMonsterType = MonsterType.EarlyNormal;
            Debug.Log("[UnitManager] 몬스?????????? EarlyNormal");
        }
        else if (gameTime <= 420f)
        {
            currentNormalMonsterType = MonsterType.MidNormal;
            Debug.Log("[UnitManager] 몬스?????????? MidNormal");
        }
        else if (gameTime <= 600f)
        {
            currentNormalMonsterType = MonsterType.LateNormal;
        }
        else if (gameTime >= 600f)
        {
            Debug.Log("[UnitManager] 보스 ??이????작!");
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
        //Enum처리 ??도 ??거같긴 ??
        if (PlayerType == 1)
        {
            // 1. ??사
            _player = Resources.Load<GameObject>("Using/Player/Warrior");
        }
        else if (PlayerType == 2)
        {
            // 2. 궁수
            _player = Resources.Load<GameObject>("Using/Player/Archer");
        }
        else
        {
            // 3. 법사
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
            TimeManager.Instance.OnOneMinFiftySecondsPassed += SpawnUniqueMonster;
            TimeManager.Instance.OnMinutePassed += SpawnStrongMonsters;
        }
    }

    public void PauseGame()
    {
        isGameStarted = false;

        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnOneMinFiftySecondsPassed -= SpawnUniqueMonster;
            TimeManager.Instance.OnMinutePassed -= SpawnStrongMonsters;
        }
    }

    public void ResumeGame()
    {
        isGameStarted = true;
    }
    private Vector2 GetBossSpawnPosition()
    {
        if (currentPlayer == null) return Vector2.zero;

        float angle = Random.Range(0f, 360f);
        float distance = spawnRadius;

        return (Vector2)currentPlayer.transform.position + new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad) * distance,
            Mathf.Sin(angle * Mathf.Deg2Rad) * distance
        );
    }
    public MonsterBase SpawnMonsterAtRandomPosition(MonsterType type)
    {
        Vector2 randomPosition = GetRandomSpawnPosition();
        return SpawnMonster(type, randomPosition);
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

        positions.Sort((a, b) =>
        {
            float distanceA = Vector2.Distance(currentPlayer.transform.position, a);
            float distanceB = Vector2.Distance(currentPlayer.transform.position, b);
            return distanceA.CompareTo(distanceB);
        });

        return positions;
    }
    public void TakeAllDamage(float damage)
    {
        if (activeMonsters != null)
        {
            // 由ъ뒪�듃瑜� 蹂듭궗�븯�뿬 �닚�쉶
            var monstersToUpdate = activeMonsters.ToList();
            foreach (MonsterBase monster in monstersToUpdate)
            {
                if (monster != null)
                {
                    monster.TakeDamage(damage);
                }
            }
        }
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

