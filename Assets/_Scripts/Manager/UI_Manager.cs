using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : Singleton<UI_Manager>
{
    public Dictionary<string, Panel> panel_Dic = new Dictionary<string, Panel>();
    public List<Panel> panel_List = new List<Panel>();

    public Dictionary<Button, bool> m_Bless_Dic =
    new Dictionary<Button, bool>();
    protected override void Awake()
    {
        base.Awake();
        foreach (Panel panel in panel_List)
        {
            panel_Dic.Add(panel.gameObject.name, panel);
            if (panel.gameObject.name == "Main_Panel"
            || panel.gameObject.name == "Bless_Panel")
                panel.gameObject.SetActive(true);
            else panel.gameObject.SetActive(false);
        }

    }

    private void Start()
    {
        m_Bless_Dic = DataManager.Instance.blessDataTable.bless_Table;
        foreach (Panel panel in panel_List)
        {
            Debug.Log($"panel name : {panel.name}\npanel_Dic Contain? : {panel_Dic.ContainsKey(panel.name)}");
        }

    }

}
