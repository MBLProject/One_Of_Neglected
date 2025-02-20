using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : Singleton<UI_Manager>
{
    public Dictionary<string, Panel> panel_Dic = new Dictionary<string, Panel>();
    public List<Panel> panel_List;
    public Upgrade_Panel upgrade_Panel;
    public float sounds_Value;
    public float effects_Value;
    [SerializeField] private RectTransform m_Title_Group;
    [SerializeField] private RectTransform m_InGame_Group;
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
                RectGroup_Activation(false, true);
                panel_Dic["InGameUI_Panel"].PanelOpen();
            }
            if (x.name == "Title")
            {
                RectGroup_Activation(true, false);
                panel_Dic["Main_Panel"].PanelOpen();
            }
        };

    }

    private void Start()
    {
        panel_Dic["Option_Panel"].GetComponent<Option_Panel>().OnStart();
    }
    public void RectGroup_Activation(bool title, bool game)
    {
        m_Title_Group.gameObject.SetActive(title);
        m_InGame_Group.gameObject.SetActive(game);
    }

}
