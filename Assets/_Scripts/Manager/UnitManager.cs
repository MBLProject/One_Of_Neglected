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
    //[SerializeField] private List<GameObject> monsterPrefabs;

    //[Header("스폰 설정")]
    //[SerializeField] private float spawnRadius = 15f;
    //[SerializeField] private float minSpawnDistance = 8f;

    private Player currentPlayer;
    //private List<Monster> activeMonsters = new List<Monster>();
    //private Camera mainCamera;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        //mainCamera = Camera.main; 
    }
    //플레이어 생성관련
    public Player SpawnPlayer(Vector2 position)
    {
        if (currentPlayer != null)
        {
            return currentPlayer;
        }

        GameObject playerObj = Instantiate(playerPrefab, position, Quaternion.identity);
        currentPlayer = playerObj.GetComponent<Player>();
        return currentPlayer;
    }
    public Player GetPlayer() => currentPlayer;



}
