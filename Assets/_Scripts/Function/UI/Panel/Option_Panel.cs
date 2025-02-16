using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option_Panel : Panel
{
    public TMP_Dropdown screenScale_Dropdown;
    public Slider sounds_Slider;
    public Slider effects_Slider;
    public enum ScreenMode
    {
        FullScreen,
        FullScreenWindow,
        Window
    }

    private void Awake()
    {
        buttons[0].onClick.AddListener(OnCheckBTNClick);
        buttons[1].onClick.AddListener(ReturnMainPanel);
    }

    public void OnStart()
    {
        List<string> options = new List<string>
        {
            "전체화면",
            "전체화면(창모드)",
            "창모드"
        };

        screenScale_Dropdown.ClearOptions();
        screenScale_Dropdown.AddOptions(options);
        screenScale_Dropdown.onValueChanged.
        AddListener(index => ChangeFullScreenMode((ScreenMode)index));

        switch (screenScale_Dropdown.value)
        {
            case 0:
                Screen.SetResolution(2560, 1440, FullScreenMode.ExclusiveFullScreen);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                break;
            case 2:
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                break;
        }
    }
    private void ChangeFullScreenMode(ScreenMode mode)
    {

        switch (mode)
        {
            case ScreenMode.FullScreen:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case ScreenMode.FullScreenWindow:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case ScreenMode.Window:
                Screen.fullScreenMode = FullScreenMode.Windowed;

                break;
        }
    }

    private void OnCheckBTNClick()
    {
        UI_Manager.Instance.sounds_Value = sounds_Slider.value;
        UI_Manager.Instance.effects_Value = effects_Slider.value;
    }

    private void ReturnMainPanel()
    {
        PanelClose();
        sounds_Slider.value = UI_Manager.Instance.sounds_Value;
        effects_Slider.value = UI_Manager.Instance.effects_Value;
        UI_Manager.Instance.panel_Dic["Main_Panel"].PanelOpen();
    }
}