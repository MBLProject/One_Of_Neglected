using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bless_Panel : Panel
{
    public List<Button> attack_Left;
    public List<Button> attack_Right;
    public List<Button> deffence_Left;
    public List<Button> deffence_Right;
    public List<Button> utility_Left;
    public List<Button> utility_Right;
    public Queue<Button> revertTarget = new Queue<Button>();

    private void Awake()
    {

        Dic_Setting(attack_Left);
        Dic_Setting(attack_Right);
        Dic_Setting(deffence_Right);
        Dic_Setting(deffence_Left);
        Dic_Setting(utility_Right);
        Dic_Setting(utility_Left);
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

    //딕셔너리 초기화 및 불러온 데이터에 따라 노드활성화
    private void Dic_Setting(List<Button> buttons)
    {
        foreach (Button button in buttons)
        {
            if (DataManager.Instance.bless_Dic.ContainsKey(button) == false)
                DataManager.Instance.bless_Dic.Add(button, false);
            else
            {
                if (DataManager.Instance.bless_Dic[button])
                {
                    button.interactable = true;
                }
            }
        }
    }

    //노드별 리스너 구독
    private void ButtonInit(List<Button> m_Buttons, Button bro_BTN)
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
                m_Buttons[k].onClick.AddListener(CheckBroNode(m_Buttons, bro_BTN));
            }
            if (k == 14)
            {
                m_Buttons[k].onClick.AddListener(ActiveNextNode(m_Buttons[k + 1]));
            }
            m_Buttons[k].onClick.AddListener(ChangeDicVal(m_Buttons[k], true));
        }
    }

    //형제노드 활성화에 따라 다음 노드 활성화 체크
    private UnityAction CheckBroNode(List<Button> m_Buttons, Button bro_BTN)
    {
        return () => m_Buttons[14].interactable = DataManager.Instance.bless_Dic[bro_BTN];
    }

    //다음 노드 활성화
    private UnityAction ActiveNextNode(Button nextNode)
    {
        return () => nextNode.interactable = true;
    }

    //노드 활성화 시 딕셔너리 밸류 변경
    private UnityAction ChangeDicVal(Button clickedNode, bool value)
    {
        return () => DataManager.Instance.bless_Dic[clickedNode] = value;
    }

}
