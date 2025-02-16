using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[Serializable]
public class ClassInfo : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI className_TMP;
    [SerializeField]
    private TextMeshProUGUI level_TMP;
    [SerializeField]
    private Image protrait_IMG;
    [SerializeField]
    private Image weaponIcon_IMG;
    [SerializeField]
    private ClassSelect_Panel classSelect_Panel;

    public string m_className;
    public int m_level;
    public Sprite m_Portrait;
    public Sprite m_WeaponIcon;
    public Button m_BTN;
    public Outline m_Outline;

    private void Awake()
    {
        if (classSelect_Panel == null) classSelect_Panel = GetComponentInParent<ClassSelect_Panel>();
        if (m_BTN == null) m_BTN = GetComponent<Button>();
        if (m_Outline == null) m_Outline = GetComponent<Outline>();
        //TODO : 저장된 m_level변수로 레벨 불러오기

    }
    private void Start()
    {
        className_TMP.text = m_className;
        level_TMP.text = m_level.ToString();
        protrait_IMG.sprite = m_Portrait;
        weaponIcon_IMG.sprite = m_WeaponIcon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (m_Outline.enabled) return;

            m_Outline.enabled = true;
            m_BTN.interactable = true;
            if (classSelect_Panel.selection_Queue.Count > 0)
            {
                ClassInfo classInfo = classSelect_Panel.selection_Queue.Dequeue();
                classInfo.m_BTN.interactable = false;
                classInfo.m_Outline.enabled = false;
            }
            classSelect_Panel.selection_Queue.Enqueue(this);
        }
    }
}
