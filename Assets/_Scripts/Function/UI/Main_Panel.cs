using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Panel : Panel
{
    private void Awake()
    {
        buttons[0].onClick.AddListener(Start_BTN);
        buttons[1].onClick.AddListener(Upgrade_BTN);
    }

    private void Start_BTN()
    {
        //TODO 다음 씬으로~
        UI_Manager.Instance.panel_Dic["Main_Panel"].PanelClose();
        GameSceneManager.SceneLoad("Game");

    }
    private void Upgrade_BTN()
    {
        UI_Manager.Instance.panel_Dic["Upgrade_Panel"].PanelOpen();
        PanelClose();
    }

}
