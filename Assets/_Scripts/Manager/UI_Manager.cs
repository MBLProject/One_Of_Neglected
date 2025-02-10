using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : Singleton<UI_Manager>
{
    public Dictionary<string, Panel> panel_Dic = new Dictionary<string, Panel>();
    public List<Panel> panel_List = new List<Panel>();

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

    }
}
