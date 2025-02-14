using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : Singleton<UI_Manager>
{
    public Dictionary<string, Panel> panel_Dic = new Dictionary<string, Panel>();
    public List<Panel> panel_List = new List<Panel>();
    public Player p;

    public Upgrade_Panel upgrade_Panel;

    public Button ctrlPanel_CloseBTN;

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
        if (upgrade_Panel == null)
            upgrade_Panel = panel_Dic["Upgrade_Panel"].GetComponentInChildren<Upgrade_Panel>();

        ctrlPanel_CloseBTN.onClick.AddListener(CtrlPanelOff);
    }

    private void Start()
    {

    }
    private void OnEnable()
    {
        DataManager.Instance.LoadBlessData();

    }

    private void CtrlPanelOff()
    {
        panel_Dic["Control_Panel"].PanelClose();
        panel_Dic["Main_Panel"].PanelOpen();
    }
}
