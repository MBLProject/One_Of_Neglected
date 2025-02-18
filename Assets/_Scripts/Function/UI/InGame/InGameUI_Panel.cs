using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI_Panel : Panel
{
    bool isOptionActive;
    private void Update()
    {
        //TODO : 키코드 입력받는거 몰아넣기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOptionActive = UI_Manager.Instance.panel_Dic["Option_Panel"].gameObject.activeSelf;
            if (isOptionActive == false)
            {

                UI_Manager.Instance.panel_Dic["Option_Panel"].PanelOpen();
                return;
            }
            if (isOptionActive)
            {
                UI_Manager.Instance.panel_Dic["Option_Panel"].PanelClose();
                return;
            }
        }
    }

}
