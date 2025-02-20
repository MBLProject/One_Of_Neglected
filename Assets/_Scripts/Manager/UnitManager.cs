using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    [Header("??��????��")]
    [SerializeField] private GameObject earlyNormalMonsterPrefab;
    [SerializeField] private GameObject rangedNormalMonsterPrefab;
    [SerializeField] private GameObject midNormalMonsterPrefab;
    [SerializeField] private GameObject lateNormalMonsterPrefab;
    [SerializeField] private GameObject damageUniqueMonsterPrefab;
    [SerializeField] private GameObject crowdControlUniqueMonsterPrefab;
    [SerializeField] private GameObject tankUniqueMonsterPrefab;
    [SerializeField] private GameObject bossMonsterPrefab;

    [Header("??�� ??��")]
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
        // �׽�Ʈ�� Ű �Է�
        if (Input.GetKeyDown(KeyCode.U))  // UŰ: ����ũ ���� ��ȯ �׽�Ʈ
        {
            Debug.Log("����ũ ���� ��ȯ �׽�Ʈ ����");
            SpawnUniqueMonster();
        }

        if (Input.GetKeyDown(KeyCode.T))  // TŰ: ��ũ ����ũ ���� ���� �׽�Ʈ
        {
            Debug.Log("��ũ ����ũ ���� ���� �׽�Ʈ ����");
            SpawnTankUniquesInFormation();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))  // 1Ű: ���� ����
        {
            Debug.Log("��ũ ����ũ ���� ���� ���� �׽�Ʈ");
            SpawnVerticalFormation(currentPlayer.transform.position);  // �÷��̾� ��ġ ���
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))  // 2Ű: ���� ����
        {
            Debug.Log("��ũ ����ũ ���� ���� ���� �׽�Ʈ");
            SpawnHorizontalFormation(currentPlayer.transform.position);  // �÷��̾� ��ġ ���
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))  // 3Ű: ���� ����
        {
            Debug.Log("��ũ ����ũ ���� ���� ���� �׽�Ʈ");
            SpawnCircularFormation(currentPlayer.transform.position);  // �÷��̾� ��ġ ���
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

        if (formationRoll <= 40)  // 40% Ȯ���� ���� ����
        {
            SpawnVerticalFormation(spawnCenter);
            Debug.Log("��ũ ����ũ ���� ���� ���� ��ȯ");
        }
        else if (formationRoll <= 80)  // 40% Ȯ���� ���� ����
        {
            SpawnHorizontalFormation(spawnCenter);
            Debug.Log("��ũ ����ũ ���� ���� ���� ��ȯ");
        }
        else  // 20% Ȯ���� ���� ����
        {
            SpawnCircularFormation(spawnCenter);
            Debug.Log("��ũ ����ũ ���� ���� ���� ��ȯ");
        }
    }
    private void SpawnVerticalFormation(Vector2 playerPos)
    {
        float spacing = 0.5f; // ���� �� ����
        int monstersPerLine = 8; // �� �ٴ� ���� ��
        float distanceFromPlayer = 4f; // �÷��̾�κ����� �Ÿ�

        // ���� ���⿡�� 8���� ��ȯ (���� �Ǵ� ������)
        int side = Random.Range(0, 2) * 2 - 1; // -1 �Ǵ� 1
        float xOffset = side * distanceFromPlayer;

        for (int i = 0; i < monstersPerLine; i++)
        {
            float yOffset = (i - (monstersPerLine - 1) / 2f) * spacing;
            Vector2 spawnPos = playerPos + new Vector2(xOffset, yOffset);
            MonsterBase monster = SpawnMonster(MonsterType.TankUnique, spawnPos);
            if (monster != null)
            {
                Debug.Log($"��ũ ���� ���� - ��ġ: {spawnPos}, �÷��̾���� �Ÿ�: {Vector2.Distance(playerPos, spawnPos)}");
            }
        }
    }

    private void SpawnHorizontalFormation(Vector2 playerPos)
    {
        float spacing = 0.5f; // ���� �� ����
        int monstersPerLine = 8; // �� �ٴ� ���� ��
        float distanceFromPlayer = 4f; // �÷��̾�κ����� �Ÿ�

        // ���� ���⿡�� 8���� ��ȯ (�� �Ǵ� �Ʒ�)
        int side = Random.Range(0, 2) * 2 - 1; // -1 �Ǵ� 1
        float yOffset = side * distanceFromPlayer;

        for (int i = 0; i < monstersPerLine; i++)
        {
            float xOffset = (i - (monstersPerLine - 1) / 2f) * spacing;
            Vector2 spawnPos = playerPos + new Vector2(xOffset, yOffset);
            MonsterBase monster = SpawnMonster(MonsterType.TankUnique, spawnPos);
            if (monster != null)
            {
                Debug.Log($"��ũ ���� ���� - ��ġ: {spawnPos}, �÷��̾���� �Ÿ�: {Vector2.Distance(playerPos, spawnPos)}");
            }
        }
    }

    private void SpawnCircularFormation(Vector2 playerPos)
    {
        int monsterCount = 8; // �� 16����
        float radius = 4f; // ���� ������ ������ (�÷��̾�κ����� �Ÿ�)

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
                Debug.Log($"��ũ ���� ���� - ��ġ: {spawnPos}, �÷��̾���� �Ÿ�: {Vector2.Distance(playerPos, spawnPos)}");
            }
        }
    }
    private void SpawnStrongMonsters()
    {
        float gameTime = TimeManager.Instance.GameTime;

        if (gameTime <= 180f)
        {
            currentNormalMonsterType = MonsterType.EarlyNormal;
            Debug.Log("[UnitManager] ��?????????? EarlyNormal");
        }
        else if (gameTime <= 420f)
        {
            currentNormalMonsterType = MonsterType.MidNormal;
            Debug.Log("[UnitManager] ��?????????? MidNormal");
        }
        else if (gameTime <= 600f)
        {
            currentNormalMonsterType = MonsterType.LateNormal;
        }
        else if (gameTime >= 600f)
        {
            Debug.Log("[UnitManager] ���� ??��????��!");
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
        //Enumó�� ??�� ??�Ű��� ??
        if (PlayerType == 1)
        {
            // 1. ??��
            _player = Resources.Load<GameObject>("Using/Player/Warrior");
        }
        else if (PlayerType == 2)
        {
            // 2. �ü�
            _player = Resources.Load<GameObject>("Using/Player/Archer");
        }
        else
        {
            // 3. ����
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

