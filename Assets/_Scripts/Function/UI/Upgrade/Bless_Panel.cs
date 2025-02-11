using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum NodeDefine
{
    ATK,
    DEF,
    UIL
}
public enum ATK_Bless
{
    ATK_INCREASE,
    PROJECTILE_INCREASE,
    ATK_SPEED_INCREASE,
    CRITICAL_DAMAGE_INCREASE,
    CRITICAL_PERCENT_INCREASE,
    PROJECTILE_DESTROY,
    PROJECTILE_PARRY,
    GOD_KILL
}
public enum DEF_Bless
{
    MAX_HP_INCREASE,
    DEFENSE_INCREASE,
    HP_REGEN_INCREASE,
    BARRIER_ACTIVATE,
    BARRIER_COOLDOWN,
    INVINCIBILITY,
    ADVERSARY
}
public enum UIL_Bless
{
    ATK_RANGE,
    DURATION,
    COOLDOWN,
    RESURRECTION,
    MAGNET,
    GROWTH,
    AVARICE,
    DASH
}
public class Bless_Panel : Panel
{
    //공통된걸 등록해줄 리스트
    public List<Node> ATK_Node_List;
    public List<Node> DEF_Node_List;
    public List<Node> UIL_Node_List;

    private void Awake()
    {
        Node_Initialize(ATK_Node_List);
        Node_Initialize(DEF_Node_List);
        Node_Initialize(UIL_Node_List);
    }

    private void Start()
    {

    }

    //딕셔너리 초기화 및 불러온 데이터에 따라 노드활성화
    private void Node_Initialize(List<Node> nodes)
    {
        foreach (Node node in nodes)
        {   //딕셔너리에 없으면 추가
            if (DataManager.Instance.bless_Dic.ContainsKey(node) == false)
            {
                DataManager.Instance.bless_Dic.Add(node, false);
            }
            if (DataManager.Instance.bless_Dic[node] == true)
            {
                node.clicked = true;
                node.can_Revert = true;
            }

        }
    }

    private void AddListener_Bless()
    {

    }

    // //노드별 리스너 구독
    // private void ButtonInit(List<Node> nodes, Node bro_Node)
    // {
    //     for (int i = 0; i < nodes.Count - 3; i++)
    //     {
    //         nodes[i].m_BTN.onClick.AddListener(ActiveNextNode(nodes[i + 1]));
    //         nodes[i].m_BTN.onClick.AddListener(ChangeDicVal(nodes[i], true));
    //     }

    //     for (int k = nodes.Count - 3; k < nodes.Count; k++)
    //     {
    //         if (k == 13)
    //         {
    //             nodes[k].m_BTN.onClick.AddListener(CheckBroNode(nodes, bro_Node));
    //         }
    //         if (k == 14)
    //         {
    //             nodes[k].m_BTN.onClick.AddListener(ActiveNextNode(nodes[k + 1]));
    //         }
    //         nodes[k].m_BTN.onClick.AddListener(ChangeDicVal(nodes[k], true));
    //     }
    // }

    // //형제노드 활성화에 따라 다음 노드 활성화 체크
    // private UnityAction CheckBroNode(List<Node> nodes, Node bro_Node)
    // {
    //     return () => nodes[14].m_BTN.interactable = DataManager.Instance.bless_Dic[bro_Node];
    // }

    // //다음 노드 활성화
    // private UnityAction ActiveNextNode(Node nextNode)
    // {
    //     return () => nextNode.m_BTN.interactable = true;
    // }

    // //노드 활성화 시 딕셔너리 밸류 변경
    // private UnityAction ChangeDicVal(Node clickedNode, bool value)
    // {
    //     return () => DataManager.Instance.bless_Dic[clickedNode] = value;
    // }

}
