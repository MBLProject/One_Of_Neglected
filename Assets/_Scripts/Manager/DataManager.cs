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
    public int Bless_Point;
    public int remnants;
}
[Serializable]
public class BTS
{
    public int MaxHp;
    public float HpRegen;
    public int Defense;
    public float Mspd;
    public float ATK;
    public float Aspd;
    public float CriRate;
    public float CriDamage;
    public int ProjAmount;
    public float ATKRange;
    public float Duration;
    public float Cooldown;
    public int Revival;
    public float Magnet;
    public float Growth;
    public float Greed;
    public float Curse;
    public int Reroll;
    public int Banish;
    public float BarrierCooldown;
    public int DashCount;
    public bool Barrier;
    public bool ProjDestroy;
    public bool projParry;
    public bool Invincibility;
    public bool Adversary;
    public bool GodKill;
}

public class DataManager : Singleton<DataManager>
{
    public DicDataTable dicDataTable = new DicDataTable();
    public Dictionary<Node, bool> bless_Dic = new Dictionary<Node, bool>();
    public PlayerProperty player_Property = new PlayerProperty();

    public BTS BTS = new BTS();

    string path = "Assets/Resources/SaveFile/";
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        string fromJsonData;
        fromJsonData = Load_JsonUtility<PlayerProperty>("PlayerProperty", player_Property);
        player_Property = JsonUtility.FromJson<PlayerProperty>(fromJsonData);

        fromJsonData = Load_JsonUtility<BTS>("BTS", BTS);
        BTS = JsonUtility.FromJson<BTS>(fromJsonData);
    }

    public void SaveData()
    {
        Debug.Log("저장");

        dicDataTable.bless_Table = bless_Dic;
        string Data = DictionaryJsonUtility.ToJson(dicDataTable.bless_Table);
        File.WriteAllText(path + "BlessData", Data);

        Save_JsonUtility<PlayerProperty>("PlayerProperty", player_Property);
        Save_JsonUtility<BTS>("BTS", BTS);

        // Data = JsonUtility.ToJson(playerProperty);
        // File.WriteAllText(path + "PlayerProperty", Data);

        // Data = JsonUtility.ToJson(BTS);
        // File.WriteAllText(path + "BTS", Data);
    }
    public void Save_JsonUtility<T>(string fileName, T data)
    {
        string Data = JsonUtility.ToJson(data);
        File.WriteAllText(path + fileName, Data);
    }
    public string Load_JsonUtility<T>(string fileName, T data)
    {
        if (!File.Exists(path + fileName))
        {
            string Data = JsonUtility.ToJson(data);
            File.WriteAllText(path + fileName, Data);
        }
        string fromJsonData = File.ReadAllText(path + fileName);
        return fromJsonData;
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

}
