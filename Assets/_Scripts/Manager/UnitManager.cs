using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Header("??醫롫윥?????醫롫윪??")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject earlyNormalMonsterPrefab;
    [SerializeField] private GameObject rangedNormalMonsterPrefab;
    [SerializeField] private GameObject midNormalMonsterPrefab;
    [SerializeField] private GameObject lateNormalMonsterPrefab;
    [SerializeField] private GameObject damageUniqueMonsterPrefab;
    [SerializeField] private GameObject crowdControlUniqueMonsterPrefab;
    [SerializeField] private GameObject tankUniqueMonsterPrefab;

    [Header("??醫?????醫롫윪??")]
    [SerializeField] private float spawnRadius = 15f;
    [SerializeField] private float minSpawnDistance = 8f;
    [SerializeField] private float spawnInterval = 3f;

    [Header("?롪퍓?????醫롫윞????醫롫윪??")]
    [SerializeField] private float earlyGameDuration = 180f;  // 3??
    [SerializeField] private float midGameDuration = 420f;    // 7??
    [SerializeField] private float lateGameDuration = 600f;   // 10??

    [Header("???꾪뀬 ?뺢퀡??????깆젧")]
    [SerializeField] private float minRange = 0.5f;  // 嶺뚣끉裕?????꾪뀬 ????濾곌쑨???
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
        // ???醫롫짗????醫롫윞?대겭??嶺뚮ㄳ?낅츩????醫???
        SpawnMonsterAtRandomPosition(MonsterType.RangedNormal);

        // ??醫롫윞?????嶺뚮ㄳ?낅츩????醫???
        if (gameTime <= earlyGameDuration)  // 0~3??
        {
            SpawnMonsterAtRandomPosition(MonsterType.EarlyNormal);
        }
        else if (gameTime <= midGameDuration)  // 3~7??
        {
            SpawnMonsterAtRandomPosition(MonsterType.MidNormal);
        }
        else if (gameTime <= lateGameDuration)  // 7~10??
        {
            SpawnMonsterAtRandomPosition(MonsterType.LateNormal);
        }
    }

    //??醫롫윥???醫롫윪????醫롫윪??????
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


    // 嶺뚮ㄳ?낅츩????醫롫윪??嶺뚮∥?꾥땻??
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

    // ??醫롫윥????醫롫윪???嶺뚮ㄳ?낅츩????醫롫윪??
    public MonsterBase SpawnMonsterAtRandomPosition(MonsterType type)
    {
        Vector2 randomPosition = GetRandomSpawnPosition();
        return SpawnMonster(type, randomPosition);
    }

    // 嶺뚮ㄳ?낅츩????醫롫윞??
    public void RemoveMonster(MonsterBase monster)
    {
        if (monster != null)
        {
            activeMonsters.Remove(monster);
        }
    }

    // 嶺뚮ㅄ維獄?嶺뚮ㄳ?낅츩????醫롫윞??
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

    // ??醫롫윥????醫?????醫롫윪????ｌ뫒亦?
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

    // 嶺뚮ㄳ?낅츩??????醫롫윪????醫롫윥????醫롫윥????꾩룇瑗??
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
    /// ?뺢퀡???????????덈츎 嶺뚮ㄳ?낅츩??? ?띠럾?濚밸Ŧ?????戮?맋???洹먮봾裕?筌뤿굞???筌먲퐣議??꾩룇瑗??
    /// </summary>
    /// <param name="minRange"></param>
    /// <param name="maxRange"></param>
    /// <returns>濾곌쑨????戮곕さ???筌먲퐣議??嶺뚮ㄳ?낅츩???洹먮봾裕??/returns>
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
    /// 嶺뚮ㄳ?낅츩???洹먮봾裕?筌뤾쑬????띠럾????띠럾?濚밸Ŧ???嶺뚮ㄳ?낅츩???꾩룇瑗??
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

    public List<Vector3> GetMonsterPositionsInRange(float minRange, float maxRange)
    {
        return activeMonsters
            .Where(monster => monster != null)
            .Select(monster => new { monster.transform.position, distance = Vector3.Distance(currentPlayer.transform.position, monster.transform.position) })
            .Where(data => data.distance >= minRange && data.distance <= maxRange)
            .OrderBy(data => data.distance)
            .Select(data => data.position)
            .ToList();
    }

    /// <summary>
    /// 揶쎛??揶쎛繹먮슣??筌뤣딅뮞?怨쀬벥 ?袁⑺뒄 獄쏆꼹??
    /// </summary>
    public Vector3? GetNearestMonsterPosition()
    {
        var nearestMonster = activeMonsters
            .Where(monster => monster != null)
            .Select(monster => new { monster.transform.position, distance = Vector3.Distance(currentPlayer.transform.position, monster.transform.position) })
            .OrderBy(data => data.distance)
            .FirstOrDefault();

        return nearestMonster?.position;
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



