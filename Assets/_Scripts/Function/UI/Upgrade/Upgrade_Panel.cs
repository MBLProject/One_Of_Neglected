using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Upgrade_Panel : Panel
{
    public TextMeshProUGUI upgradePanel_TMP;
    public Toggle bless_Toggle;
    public Toggle training_Toggle;
    public List<GameObject> helpPopup_BlessElements;
    public List<GameObject> helpPopup_TrainingElements;
    private void Awake()
    {
        buttons[0].onClick.AddListener(BlessReset_BTN);
        buttons[1].onClick.AddListener(Return_BTN);
        bless_Toggle.onValueChanged.AddListener(ToggleEvents);
        bless_Toggle.interactable = false;
        training_Toggle.onValueChanged.AddListener(ToggleEvents);

    }
    private void OnEnable()
    {
        if (UI_Manager.Instance.panel_Dic.ContainsKey("Bless_Panel"))
            UI_Manager.Instance.panel_Dic["Bless_Panel"].PanelOpen();
    }
    //토글 제어 메서드
    private void ToggleEvents(bool arg0)
    {
        if (training_Toggle.isOn)
        {
            HelpElemets(helpPopup_TrainingElements, helpPopup_BlessElements);

            bless_Toggle.interactable = true;
            training_Toggle.interactable = false;

            buttons[0].onClick.RemoveAllListeners();
            buttons[0].onClick.AddListener(TrainingReset_BTN);

            UI_Manager.Instance.panel_Dic["Bless_Panel"].PanelClose();
            UI_Manager.Instance.panel_Dic["Training_Panel"].PanelOpen();
        }
        else
        {
            HelpElemets(helpPopup_BlessElements, helpPopup_TrainingElements);

            bless_Toggle.interactable = false;
            training_Toggle.interactable = true;

            buttons[0].onClick.RemoveAllListeners();
            buttons[0].onClick.AddListener(BlessReset_BTN);

            UI_Manager.Instance.panel_Dic["Training_Panel"].PanelClose();
            UI_Manager.Instance.panel_Dic["Bless_Panel"].PanelOpen();
        }
    }

    private void BlessReset_BTN()
    {
        //TODO : 스킬셋 초기화
        Debug.Log("가호 리셋");
    }
    private void TrainingReset_BTN()
    {
        Debug.Log("단련 리셋");
    }
    //메인 패널로 돌아가는 메서드
    private void Return_BTN()
    {
        UI_Manager.Instance.panel_Dic["Main_Panel"].PanelOpen();
        PanelClose();
    }

    private void HelpElemets(List<GameObject> enableElements, List<GameObject> disableElements)
    {
        foreach (GameObject obj in enableElements)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in disableElements)
        {
            obj.SetActive(false);
        }
    }
}
