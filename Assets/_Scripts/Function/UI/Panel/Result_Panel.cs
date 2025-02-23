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

    }
    private void OnDisable()
    {
        buttons[0].interactable = true;
    }
    private void Title_BTN()
    {
        GameSceneManager.SceneLoad("Title");

        UI_Manager.Instance.panel_Dic["Main_Panel"].PanelOpen();
        PanelClose();
    }

}
