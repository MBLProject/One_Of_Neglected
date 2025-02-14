using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Bless_Panel bless_Panel;
    public NodeDefine nodeDefine;
    public ATK_Bless ATK_Bless;
    public DEF_Bless DEF_Bless;
    public UTI_Bless UTI_Bless;
    //다음 노드
    public List<Node> next_Nodes;
    //이전 노드
    public List<Node> prev_Nodes;
    public Button m_BTN;
    public bool can_Interactable = false;
    public bool can_Revert = false;

    public bool clicked = false;
    ColorBlock colorBlock_Origin;
    ColorBlock colorBlock_Temp;

    public event Action<bool> methodAction;
    public event Action baseNodeAction;

    private void Awake()
    {

        m_BTN = GetComponent<Button>();
        if (bless_Panel == null) bless_Panel = GetComponentInParent<Bless_Panel>();
        bless_Panel.nodeReset += NodeReset;
        m_BTN.onClick.AddListener(BTN_Clicked);

        if (prev_Nodes.Count == 0) m_BTN.interactable = true;

        colorBlock_Origin = m_BTN.colors;
        colorBlock_Temp = colorBlock_Origin;

        SetNextNode(next_Nodes);

    }

    private void SetNextNode(List<Node> next_Nodes)
    {
        foreach (Node node in next_Nodes)
        {
            m_BTN.onClick.AddListener(() => Check_PrevNodes_Of_NextNode(node));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (can_Revert)
            {
                BTN_Reverted();
                CtrlAroundNodes(next_Nodes, false);
            }
        }
    }
    private void CtrlAroundNodes(List<Node> nodes, bool interactable)
    {
        if (nodes.Count > 0)
        {
            foreach (Node node in nodes)
            {
                node.m_BTN.interactable = interactable;
            }
        }
    }
    public void BTN_Clicked()
    {
        if (DataManager.Instance.player_Property.Bless_Point == 0) return;

        clicked = true;
        can_Revert = true;
        m_BTN.interactable = false;

        colorBlock_Temp.disabledColor = colorBlock_Origin.normalColor;
        m_BTN.colors = colorBlock_Temp;

        foreach (Node prevNode in prev_Nodes)
        {
            prevNode.can_Revert = false;
        }
        DataManager.Instance.bless_Dic[this] = true;

    }
    public void BTN_Reverted()
    {
        clicked = false;
        can_Revert = false;
        m_BTN.interactable = true;
        colorBlock_Temp.disabledColor = colorBlock_Origin.disabledColor;
        m_BTN.colors = colorBlock_Temp;
        foreach (Node prevNode in prev_Nodes)
        {
            prevNode.can_Revert = true;
        }
        DataManager.Instance.bless_Dic[this] = false;
        methodAction?.Invoke(false);
        baseNodeAction?.Invoke();
    }
    private void Check_PrevNodes_Of_NextNode(Node node)
    {
        if (DataManager.Instance.player_Property.Bless_Point == 0) return;
        methodAction?.Invoke(true);
        baseNodeAction?.Invoke();
        if (node.prev_Nodes.Count == 1)
        {
            node.m_BTN.interactable = true;
            return;
        }
        if (node.prev_Nodes.Count > 1)
        {
            foreach (Node prevNode in node.prev_Nodes)
            {
                if (prevNode.clicked == false)
                {
                    return;
                }
            }
            node.m_BTN.interactable = true;
        }
    }
    private void NodeReset()
    {
        if (clicked) methodAction?.Invoke(false);
        clicked = false;
        can_Revert = false;
        m_BTN.colors = colorBlock_Origin;
        if (prev_Nodes.Count > 0) m_BTN.interactable = false;
        else m_BTN.interactable = true;

        DataManager.Instance.bless_Dic[this] = false;
        baseNodeAction?.Invoke();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //TODO 툴팁 활성화
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TODO 툴팁 비활성화
    }

}