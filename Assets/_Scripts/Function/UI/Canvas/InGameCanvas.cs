using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvas : MonoBehaviour
{
    public CanvasScaler m_CanvasScaler;
    public List<Panel> inGameCanvasPanels;
    public Button cheat;

    private void Awake()
    {
        cheat.onClick.AddListener(LevelUpBTN);
    }

    private void LevelUpBTN()
    {
        UnitManager.Instance.GetPlayer().statViewer.Level++;
        inGameCanvasPanels[1].gameObject.SetActive(true);
    }
}
