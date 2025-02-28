using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvas : MonoBehaviour
{
    public CanvasScaler m_CanvasScaler;
    public List<Panel> inGameCanvasPanels;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            LevelUpBTN();
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            UI_Manager.Instance.panel_Dic["LevelUp_Panel"].GetComponent<LevelUp_Panel>().AugSelections();
        }
    }

    private void LevelUpBTN()
    {
        UnitManager.Instance.GetPlayer().statViewer.Level++;
        inGameCanvasPanels[1].gameObject.SetActive(true);
    }
}
