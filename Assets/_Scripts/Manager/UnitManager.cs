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

    [Header("?꾨━???ㅼ젙")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject earlyNormalMonsterPrefab;
    [SerializeField] private GameObject rangedNormalMonsterPrefab;
    [SerializeField] private GameObject midNormalMonsterPrefab;
    [SerializeField] private GameObject lateNormalMonsterPrefab;
    [SerializeField] private GameObject damageUniqueMonsterPrefab;
    [SerializeField] private GameObject crowdControlUniqueMonsterPrefab;
    [SerializeField] private GameObject tankUniqueMonsterPrefab;

    [Header("?ㅽ룿 ?ㅼ젙")]
    [SerializeField] private float spawnRadius = 15f;
    [SerializeField] private float minSpawnDistance = 8f;
    [SerializeField] private float spawnInterval = 3f;

    [Header("寃뚯엫 ?쒓컙 ?ㅼ젙")]
    [SerializeField] private float earlyGameDuration = 180f;  // 3遺?
    [SerializeField] private float midGameDuration = 420f;    // 7遺?
    [SerializeField] private float lateGameDuration = 600f;   // 10遺?

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
        // ??긽 ?먭굅由?紐ъ뒪???ㅽ룿
        SpawnMonsterAtRandomPosition(MonsterType.RangedNormal);

        // ?쒓컙?蹂?紐ъ뒪???ㅽ룿
        if (gameTime <= earlyGameDuration)  // 0~3遺?
        {
            SpawnMonsterAtRandomPosition(MonsterType.EarlyNormal);
        }
        else if (gameTime <= midGameDuration)  // 3~7遺?
        {
            SpawnMonsterAtRandomPosition(MonsterType.MidNormal);
        }
        else if (gameTime <= lateGameDuration)  // 7~10遺?
        {
            SpawnMonsterAtRandomPosition(MonsterType.LateNormal);
        }
    }

    //?뚮젅?댁뼱 ?앹꽦愿??
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


    // 紐ъ뒪???앹꽦 硫붿꽌??
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

    // ?쒕뜡 ?꾩튂??紐ъ뒪???앹꽦
    public MonsterBase SpawnMonsterAtRandomPosition(MonsterType type)
    {
        Vector2 randomPosition = GetRandomSpawnPosition();
        return SpawnMonster(type, randomPosition);
    }

    // 紐ъ뒪???쒓굅
    public void RemoveMonster(MonsterBase monster)
    {
        if (monster != null)
        {
            activeMonsters.Remove(monster);
        }
    }

    // 紐⑤뱺 紐ъ뒪???쒓굅
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

    // ?쒕뜡 ?ㅽ룿 ?꾩튂 怨꾩궛
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

    // 紐ъ뒪????낆뿉 ?곕Ⅸ ?꾨━??諛섑솚
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

    // ?쒖꽦?붾맂 紐ъ뒪????諛섑솚
    public int GetActiveMonsterCount() => activeMonsters.Count;

    // ?뱀젙 踰붿쐞 ?댁쓽 紐ъ뒪??李얘린
    public List<MonsterBase> GetMonstersInRange(Vector2 position, float range)
    {
        return activeMonsters.FindAll(monster =>
            Vector2.Distance(position, monster.transform.position) <= range);
    }


    public float GetGameTime() => gameTime;
    public Player GetPlayer() => currentPlayer;
}

// 紐ъ뒪??????닿굅??
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



