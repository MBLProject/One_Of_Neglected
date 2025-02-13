using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using DG.Tweening.Plugins;
[Serializable]
public class DicDataTable
{
    public Dictionary<Node, bool> bless_Table = new Dictionary<Node, bool>();
}
[Serializable]
public class PlayerProperty
{
    public int gold;
    public int blessPoint;
    public int remnants;
}
[Serializable]
public class BTS
{

}

public class DataManager : Singleton<DataManager>
{
    public DicDataTable dicDataTable = new DicDataTable();
    public Dictionary<Node, bool> bless_Dic = new Dictionary<Node, bool>();
    public PlayerProperty playerProperty = new PlayerProperty
    {
        gold = 0,
        blessPoint = 0,
        remnants = 0
    };

    string path = "Assets/Resources/SaveFile/";
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        LoadPlayerProperty();
    }
    public void SaveData()
    {
        Debug.Log("저장");

        dicDataTable.bless_Table = bless_Dic;
        string Data = DictionaryJsonUtility.ToJson(dicDataTable.bless_Table);
        File.WriteAllText(path + "BlessData", Data);

        Data = JsonUtility.ToJson(playerProperty);
        File.WriteAllText(path + "PlayerProperty", Data);
    }

    public void LoadBlessData()
    {
        if (!File.Exists(path + "BlessData"))
        {
            dicDataTable.bless_Table = bless_Dic;
            string Data = DictionaryJsonUtility.ToJson(dicDataTable.bless_Table);
            File.WriteAllText(path + "BlessData", Data);
        }

        string fromJsonData_Bless =
        File.ReadAllText(path + "BlessData");
        dicDataTable.bless_Table =
        DictionaryJsonUtility.FromJson<Node, bool>(fromJsonData_Bless);
        bless_Dic = dicDataTable.bless_Table;
    }
    public void LoadPlayerProperty()
    {
        if (!File.Exists(path + "PlayerProperty"))
        {
            string Data = JsonUtility.ToJson(playerProperty);
            File.WriteAllText(path + "PlayerProperty", Data);
        }
        string fromJsonData = File.ReadAllText(path + "PlayerProperty");
        playerProperty = JsonUtility.FromJson<PlayerProperty>(fromJsonData);

    }
}
