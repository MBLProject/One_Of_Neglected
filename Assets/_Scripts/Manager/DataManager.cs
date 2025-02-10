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
    public DicDataTable blessDataTable = new DicDataTable();
    string path = "Assets/Resources/SaveFile/";
    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }
    public void SaveData()
    {
        Debug.Log("저장");
        blessDataTable.bless_Table = UI_Manager.Instance.m_Bless_Dic;
        string blessData = DictionaryJsonUtility.ToJson(blessDataTable.bless_Table);
        File.WriteAllText(path + "BlessData", blessData);
    }

    public void LoadData()
    {
        string fromJsonData_Bless = File.ReadAllText(path + "BlessData");
        blessDataTable.bless_Table =
        DictionaryJsonUtility.FromJson<Button, bool>(fromJsonData_Bless);
    }

}
