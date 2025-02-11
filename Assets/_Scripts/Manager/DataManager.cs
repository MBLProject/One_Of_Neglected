using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
[Serializable]
public class DicDataTable
{
    public Dictionary<Node, bool> bless_Table = new Dictionary<Node, bool>();
}
public class DataManager : Singleton<DataManager>
{
    public DicDataTable dicDataTable = new DicDataTable();
    public Dictionary<Node, bool> bless_Dic = new Dictionary<Node, bool>();

    string path = "Assets/Resources/SaveFile/";
    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }
    public void SaveData()
    {
        Debug.Log("저장");
        dicDataTable.bless_Table = bless_Dic;
        string blessData = DictionaryJsonUtility.ToJson(dicDataTable.bless_Table);
        File.WriteAllText(path + "BlessData", blessData);
    }

    public void LoadData()
    {
        if (!File.Exists(path + "BlessData"))
        {
            SaveData();
        }
        string fromJsonData_Bless = File.ReadAllText(path + "BlessData");
        try
        {
            dicDataTable.bless_Table =
            DictionaryJsonUtility.FromJson<Node, bool>(fromJsonData_Bless);
            bless_Dic = dicDataTable.bless_Table;
        }
        catch
        {
            bless_Dic = dicDataTable.bless_Table;
        }

    }

}
