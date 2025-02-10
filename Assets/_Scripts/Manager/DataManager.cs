using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class DataTable
{
    //가호 테이블
    public Dictionary<Button, bool> bless_Table;

}
public class DataManager : Singleton<DataManager>
{
    public DataTable dataTable = new();

}
