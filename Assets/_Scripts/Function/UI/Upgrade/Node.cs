using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerClickHandler
{
    private Button m_BTN;

    private void Awake()
    {
        m_BTN = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            m_BTN.interactable = false;
            DataManager.Instance.bless_Dic[m_BTN] = false;
        }
    }
}