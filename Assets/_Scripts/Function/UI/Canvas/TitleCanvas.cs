using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCanvas : MonoBehaviour
{
    public CanvasScaler m_CanvasScaler;
    public List<Panel> titleCanvasPanels;

    public Button cheat;

    private void Awake()
    {
        cheat.onClick.AddListener(Cheat);
    }

    private void Cheat()
    {
        DataManager.Instance.player_Property.remnants_Point = 15;
        DataManager.Instance.player_Property.gold = 10000000;
        DataManager.Instance.player_Property.bless_Point = 90;
    }
}
