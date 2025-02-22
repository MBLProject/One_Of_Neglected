using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using DG.Tweening.Plugins;
using DG.Tweening.Core.Easing;

[Serializable]
public class DicDataTable
{
    public Dictionary<Node, bool> bless_Table = new Dictionary<Node, bool>();
}

[Serializable]
public class PlayerProperty
{
    public int gold;
    public int bless_Point;
    public int remnants_Point;
    public List<bool> class_Unlocked =
    new List<bool>() { true, false, false };
    [HideInInspector] public int MaxHp_TrainingCount;
    [HideInInspector] public int HpRegen_TrainingCount;
    [HideInInspector] public int Defense_TrainingCount;
    [HideInInspector] public int Mspd_TrainingCount;
    [HideInInspector] public int ATK_TrainingCount;
    [HideInInspector] public int Aspd_TrainingCount;
    [HideInInspector] public int CriRate_TrainingCount;
    [HideInInspector] public int CriDamage_TrainingCount;
    [HideInInspector] public int ProjAmount_TrainingCount;
    [HideInInspector] public int ATKRange_TrainingCount;
    [HideInInspector] public int Duration_TrainingCount;
    [HideInInspector] public int Cooldown_TrainingCount;
    [HideInInspector] public int Revival_TrainingCount;
    [HideInInspector] public int Magnet_TrainingCount;
    [HideInInspector] public int Growth_TrainingCount;
    [HideInInspector] public int Greed_TrainingCount;
    [HideInInspector] public int Curse_TrainingCount;
    [HideInInspector] public int Reroll_TrainingCount;
    [HideInInspector] public int Banish_TrainingCount;
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

public class DamageInfo
{
    public float damage;
    public string projectileName;
}

public static class DamageTracker
{
    public static Action<DamageInfo> OnDamageDealt;
}

[Serializable]
public class ProjectileDamageCheck
{
    public string projectileName;
    public float damage;
    //시간처리 어케하지?? 첫 데미지 입힌시간으로??? 흠??
    public float time;
}

[Serializable]
public class DamageStats
{
    public float totalDamage;

    public List<ProjectileDamageCheck> projectileDamages = new List<ProjectileDamageCheck>();

    private Dictionary<string, float> _damageByProjectile = new Dictionary<string, float>();

    public Dictionary<string, float> damageByProjectile
    {
        get
        {
            if (_damageByProjectile == null || _damageByProjectile.Count == 0)
            {
                _damageByProjectile = new Dictionary<string, float>();
                foreach (var data in projectileDamages)
                {
                    _damageByProjectile[data.projectileName] = data.damage;
                }
            }
            return _damageByProjectile;
        }
    }

    public void UpdateLists()
    {
        projectileDamages.Clear();
        foreach (var kvp in _damageByProjectile)
        {
            projectileDamages.Add(new ProjectileDamageCheck
            {
                projectileName = kvp.Key,
                damage = kvp.Value
            });
        }
    }
}
[Serializable]
public struct InGameValue
{
    public int killCount;
    public int gold;
    public int remnents;
}
public class DataManager : Singleton<DataManager>
{

    public DicDataTable dicDataTable = new DicDataTable();
    public Dictionary<Node, bool> bless_Dic = new Dictionary<Node, bool>();
    public PlayerProperty player_Property = new PlayerProperty();

    public BTS BTS = new BTS();

    string path = "Assets/Resources/SaveFile/";

    public int classSelect_Num;

    public DamageStats currentRunDamageStats = new DamageStats();
    public InGameValue inGameValue;

    protected override void Awake()
    {
        base.Awake();
        string fromJsonData;
        fromJsonData = Load_JsonUtility<PlayerProperty>("PlayerProperty", player_Property);
        player_Property = JsonUtility.FromJson<PlayerProperty>(fromJsonData);

        fromJsonData = Load_JsonUtility<BTS>("BTS", BTS);
        BTS = JsonUtility.FromJson<BTS>(fromJsonData);
        LoadBlessData();

        DamageTracker.OnDamageDealt += TrackDamage;
    }
    private void Start()
    {
    }

    public void SaveData()
    {
        Debug.Log("저장");

        dicDataTable.bless_Table = bless_Dic;
        string Data = DictionaryJsonUtility.ToJson(dicDataTable.bless_Table);
        File.WriteAllText(path + "BlessData", Data);

        Save_JsonUtility<PlayerProperty>("PlayerProperty", player_Property, true);
        Save_JsonUtility<BTS>("BTS", BTS, true);

        // Data = JsonUtility.ToJson(playerProperty);
        // File.WriteAllText(path + "PlayerProperty", Data);

        // Data = JsonUtility.ToJson(BTS);
        // File.WriteAllText(path + "BTS", Data);
    }
    public void Save_JsonUtility<T>(string fileName, T data, bool pretty = false)
    {
        string Data = JsonUtility.ToJson(data, pretty);
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

    private void TrackDamage(DamageInfo info)
    {
        currentRunDamageStats.totalDamage += info.damage;

        //없으면 0으로 리스트에 넣고 데미지 누적시작
        if (!currentRunDamageStats.damageByProjectile.ContainsKey(info.projectileName))
        {
            currentRunDamageStats.damageByProjectile[info.projectileName] = 0;
        }
        currentRunDamageStats.damageByProjectile[info.projectileName] += info.damage;

        currentRunDamageStats.UpdateLists();
    }
}
