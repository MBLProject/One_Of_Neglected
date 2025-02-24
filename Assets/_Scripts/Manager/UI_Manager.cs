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
    public Option_Panel option_Panel;
    public float sounds_Value;
    public float effects_Value;
    [SerializeField] private TitleCanvas m_Title_Canvas;
    [SerializeField] private InGameCanvas m_InGame_Canvas;
    protected override void Awake()
    {
        base.Awake();

        SceneManager.sceneLoaded += (x, y) =>
        {
            if (x.name == "Title")
            {
                panel_List.Clear();
                panel_Dic.Clear();
                m_Title_Canvas = FindAnyObjectByType<TitleCanvas>();
                panel_List = m_Title_Canvas.titleCanvasPanels;
                panel_List.Add(option_Panel);
                foreach (Panel panel in panel_List)
                {
                    panel_Dic.Add(panel.gameObject.name, panel);
                }
            }
            if (x.name == "Game")
            {
                panel_List.Clear();
                panel_Dic.Clear();

                m_InGame_Canvas = FindAnyObjectByType<InGameCanvas>();
                panel_List = m_InGame_Canvas.inGameCanvasPanels;
                panel_List.Add(option_Panel);
                foreach (Panel panel in panel_List)
                {
                    panel_Dic.Add(panel.gameObject.name, panel);
                }
            }
        };

    }

    private void Start()
    {
        panel_Dic["Option_Panel"].GetComponent<Option_Panel>().OnStart();

    }

}
