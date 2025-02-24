using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class Result_Panel : Panel
{

    [SerializeField] Major_Panel major_Panel;
    [SerializeField] Particular_Panel particular_Panel;

    private void OnEnable()
    {
        buttons[0].interactable = false;
        buttons[1].onClick.AddListener(Title_BTN);
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        buttons[0].interactable = true;
    }
    private void Title_BTN()
    {
        Time.timeScale = 1;
        UI_Manager.Instance.panel_Dic["Main_Panel"].PanelOpen();
        Destroy(GameManager.Instance.gameObject);
        Destroy(UnitManager.Instance.gameObject);
        Destroy(ProjectileManager.Instance.gameObject);
        Destroy(TimeManager.Instance.gameObject);
        GameSceneManager.SceneLoad("Title");
        PanelClose();
    }

}
