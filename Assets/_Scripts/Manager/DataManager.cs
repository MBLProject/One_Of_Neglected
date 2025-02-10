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
    public Dictionary<Button, bool> bless_Table = new Dictionary<Button, bool>();
}
public class DataManager : Singleton<DataManager>
{
    public DicDataTable dicDataTable = new DicDataTable();
    public Dictionary<Button, bool> bless_Dic = new Dictionary<Button, bool>();

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
        string fromJsonData_Bless = File.ReadAllText(path + "BlessData");
        dicDataTable.bless_Table =
        DictionaryJsonUtility.FromJson<Button, bool>(fromJsonData_Bless);
        bless_Dic = dicDataTable.bless_Table;
    }

}
