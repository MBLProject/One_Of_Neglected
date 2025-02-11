using System;
using System.Collections;
using System.Collections.Generic;
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
public enum ATKNode
{
    Power,
    Proj,
    ATKSpeed,
    CriDamage,
    CriPercent,
    Proj_Destroy,
    Proj_Parry,
    God_Kill
}
public enum DEFNode
{
    MAXHp,
    DEF,
    HPRegen,
    Barrier,
    BarrierRegen,
    Invincibility,
    Adversary
}
public enum UILNode
{
    ATKRange,
    Duration,
    Colldown,
    Resurrection,
    Magnet,
    Growth,
    Avarice,
    MovingSkill
}
public class Bless_Panel : Panel
{
    public List<Node> attack_Left;
    public List<Node> attack_Right;
    public List<Node> deffence_Left;
    public List<Node> deffence_Right;
    public List<Node> utility_Left;
    public List<Node> utility_Right;
    public Queue<Button> revertTarget = new Queue<Button>();

    //공통된걸 등록해줄 리스트
    public List<Node> ATK_Node_List;
    public List<Node> DEF_Node_List;
    public List<Node> UIL_Node_List;

    public LinkedList<Button> test;

    private void Awake()
    {
        // Dic_Setting(attack_Left);
        // Dic_Setting(attack_Right);
        // Dic_Setting(deffence_Right);
        // Dic_Setting(deffence_Left);
        // Dic_Setting(utility_Right);
        // Dic_Setting(utility_Left);
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
    private void Dic_Setting(List<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            if (DataManager.Instance.bless_Dic.ContainsKey(node) == false)
                DataManager.Instance.bless_Dic.Add(node, false);
            else
            {
                if (DataManager.Instance.bless_Dic[node])
                {
                    node.can_Interactable = true;
                }
            }
        }

    }

    //노드별 리스너 구독
    private void ButtonInit(List<Node> nodes, Node bro_Node)
    {
        for (int i = 0; i < nodes.Count - 3; i++)
        {
            nodes[i].m_BTN.onClick.AddListener(ActiveNextNode(nodes[i + 1]));
            nodes[i].m_BTN.onClick.AddListener(ChangeDicVal(nodes[i], true));
        }

        for (int k = nodes.Count - 3; k < nodes.Count; k++)
        {
            if (k == 13)
            {
                nodes[k].m_BTN.onClick.AddListener(CheckBroNode(nodes, bro_Node));
            }
            if (k == 14)
            {
                nodes[k].m_BTN.onClick.AddListener(ActiveNextNode(nodes[k + 1]));
            }
            nodes[k].m_BTN.onClick.AddListener(ChangeDicVal(nodes[k], true));
        }
    }

    //형제노드 활성화에 따라 다음 노드 활성화 체크
    private UnityAction CheckBroNode(List<Node> nodes, Node bro_Node)
    {
        return () => nodes[14].m_BTN.interactable = DataManager.Instance.bless_Dic[bro_Node];
    }

    //다음 노드 활성화
    private UnityAction ActiveNextNode(Node nextNode)
    {
        return () => nextNode.m_BTN.interactable = true;
    }

    //노드 활성화 시 딕셔너리 밸류 변경
    private UnityAction ChangeDicVal(Node clickedNode, bool value)
    {
        return () => DataManager.Instance.bless_Dic[clickedNode] = value;
    }

}
