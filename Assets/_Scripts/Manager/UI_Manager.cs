using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : Singleton<UI_Manager>
{
    public Dictionary<string, Panel> panel_Dic = new Dictionary<string, Panel>();
    public List<Panel> panel_List = new List<Panel>();
    public Upgrade_Panel upgrade_Panel;
    public float sounds_Value;
    public float effects_Value;

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

        SceneManager.sceneLoaded += (x, y) =>
        {
            if (x.name == "Game")
            {
                gameObject.GetComponent<RectTransform>().GetChild(0).
                gameObject.SetActive(false);
            }
        };

    }

    private void Start()
    {
        panel_Dic["Option_Panel"].GetComponent<Option_Panel>().OnStart();
    }

}
