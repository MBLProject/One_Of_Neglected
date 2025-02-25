using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using DG.Tweening.Plugins;
using DG.Tweening.Core.Easing;
using JetBrains.Annotations;
using static Enums;
using UnityEngine.SceneManagement;

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

[Serializable]
public class DamageStats
{
    public float totalDamage;

    // 스킬별 데미지
    public Dictionary<Enums.SkillName, float> skillDamages = new Dictionary<Enums.SkillName, float>();
    // 증강별 데미지
    public Dictionary<Enums.AugmentName, float> augmentDamages = new Dictionary<Enums.AugmentName, float>();

    [Serializable]
    public class DamageRecord
    {
        public string name;
        public float damage;
    }

    public List<DamageRecord> skillDamageList = new List<DamageRecord>();
    public List<DamageRecord> augmentDamageList = new List<DamageRecord>();

    public void UpdateLists()
    {
        // 스킬 데미지 리스트 업데이트
        skillDamageList.Clear();
        foreach (var kvp in skillDamages)
        {
            skillDamageList.Add(new DamageRecord
            {
                name = kvp.Key.ToString(),
                damage = kvp.Value
            });
        }

        // 증강 데미지 리스트 업데이트
        augmentDamageList.Clear();
        foreach (var kvp in augmentDamages)
        {
            augmentDamageList.Add(new DamageRecord
            {
                name = kvp.Key.ToString(),
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
    public Sprite playerIcon;
}

public class DataManager : Singleton<DataManager>
{

    public DicDataTable dicDataTable = new DicDataTable();
    public Dictionary<Node, bool> bless_Dic = new Dictionary<Node, bool>();
    public PlayerProperty player_Property = new PlayerProperty();
    public BTS BTS = new BTS();

    string path = "Assets/Resources/SaveFile/";

    public ClassType classSelect_Type = Enums.ClassType.None;

    public DamageStats currentDamageStats = new DamageStats();
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

        SceneManager.sceneLoaded += (x, y) =>
        {
            if (x.name == "Title")
            {
                player_Property.gold += inGameValue.gold;
                player_Property.remnants_Point += inGameValue.remnents;
                ResetInGameInfo();
            }
        };
    }

    public void ResetInGameInfo()
    {
        inGameValue.gold = 0;
        inGameValue.remnents = 0;
        inGameValue.killCount = 0;
        inGameValue.playerIcon = null;
        currentDamageStats.skillDamages.Clear();
        currentDamageStats.augmentDamages.Clear();
    }

    public void SaveData()
    {
        Debug.Log("저장");

        dicDataTable.bless_Table = bless_Dic;
        string Data = DictionaryJsonUtility.ToJson(dicDataTable.bless_Table);
        File.WriteAllText(path + "BlessData", Data);

        Save_JsonUtility<PlayerProperty>("PlayerProperty", player_Property, true);
        Save_JsonUtility<BTS>("BTS", BTS, true);

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

    public void AddDamageData(float damage, Enums.SkillName skillName)
    {
        currentDamageStats.totalDamage += damage;

        // 스킬 데미지 추적
        if (!currentDamageStats.skillDamages.ContainsKey(skillName))
        {
            currentDamageStats.skillDamages[skillName] = 0;
        }
        currentDamageStats.skillDamages[skillName] += damage;

        currentDamageStats.UpdateLists();
    }

    public void AddDamageData(float damage, Enums.AugmentName augmentName)
    {
        currentDamageStats.totalDamage += damage;

        // 증강 데미지 추적
        if (!currentDamageStats.augmentDamages.ContainsKey(augmentName))
        {
            currentDamageStats.augmentDamages[augmentName] = 0;
        }
        currentDamageStats.augmentDamages[augmentName] += damage;

        currentDamageStats.UpdateLists();
    }
}