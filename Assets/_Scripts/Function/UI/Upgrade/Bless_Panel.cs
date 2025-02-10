using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bless_Panel : Panel
{

    public List<Button> attack_Left;
    public List<Button> attack_Right;
    public List<Button> deffence_Left;
    public List<Button> deffence_Right;
    public List<Button> utility_Left;
    public List<Button> utility_Right;

    private void Awake()
    {
        AddDic(attack_Left);
        AddDic(attack_Right);
        AddDic(deffence_Right);
        AddDic(deffence_Left);
        AddDic(utility_Right);
        AddDic(utility_Left);
    }
    private void Start()
    {

        ButtonInit(attack_Left, attack_Right[13]);
        ButtonInit(attack_Right, attack_Left[13]);
        ButtonInit(deffence_Left, deffence_Right[13]);
        ButtonInit(deffence_Right, deffence_Left[13]);
        ButtonInit(utility_Left, utility_Right[13]);
        ButtonInit(utility_Right, utility_Left[13]);
    }
    private void AddDic(List<Button> buttons)
    {
        foreach (Button button in buttons)
        {
            if (UI_Manager.Instance.m_Bless_Dic.ContainsKey(button) == false)
                UI_Manager.Instance.m_Bless_Dic.Add(button, false);
            else
            {
                if (UI_Manager.Instance.m_Bless_Dic[button])
                {
                    button.interactable = true;
                }
            }
        }
    }
    //버튼 활성화 메서드
    private void ButtonInit(List<Button> m_Buttons, Button other)
    {
        for (int i = 0; i < m_Buttons.Count - 3; i++)
        {
            m_Buttons[i].onClick.AddListener(ActiveNextNode(m_Buttons[i + 1]));
            m_Buttons[i].onClick.AddListener(ChangeDicVal(m_Buttons[i], true));
        }
        for (int k = m_Buttons.Count - 3; k < m_Buttons.Count; k++)
        {
            if (k == 13)
            {
                m_Buttons[k].onClick.AddListener(unityAction(m_Buttons, other));
            }
            if (k == 14)
            {
                m_Buttons[k].onClick.AddListener(ActiveNextNode(m_Buttons[k + 1]));
            }
            m_Buttons[k].onClick.AddListener(ChangeDicVal(m_Buttons[k], true));
        }
    }
    private UnityAction unityAction(List<Button> a, Button other)
    {
        return () => a[14].interactable = UI_Manager.Instance.m_Bless_Dic[other];
    }

    private UnityAction ActiveNextNode(Button nextNode)
    {
        return () => nextNode.interactable = true;
    }

    private UnityAction ChangeDicVal(Button clickedNode, bool value)
    {
        return () => UI_Manager.Instance.m_Bless_Dic[clickedNode] = value;
    }

    private void OnDisable()
    {
        DataManager.Instance.blessDataTable.bless_Table = UI_Manager.Instance.m_Bless_Dic;
    }
}
