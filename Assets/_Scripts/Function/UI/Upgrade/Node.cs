using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
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

    private void Awake()
    {
        m_BTN = GetComponent<Button>();
        if (prev_Nodes.Count == 0)
        {
            m_BTN.interactable = true;
        }
        m_BTN.onClick.AddListener(BTN_Clicked);

        SetNextNode(next_Nodes);
    }

    private void Start()
    {
        if (can_Interactable)
        {
            m_BTN.interactable = can_Interactable;
        }
        if (clicked)
        {
            can_Revert = true;
            can_Interactable = false;
        }
    }
    private void OnEnable()
    {

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
            Debug.Log("감지");
            if (can_Revert)
            {
                Debug.Log("되돌림");
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
        m_BTN.interactable = false;
        can_Revert = true;
        clicked = true;
        foreach (Node prevNode in prev_Nodes)
        {
            prevNode.can_Revert = false;
        }
        DataManager.Instance.bless_Dic[this] = true;
    }
    public void BTN_Reverted()
    {
        can_Revert = false;
        clicked = false;
        m_BTN.interactable = true;
        foreach (Node prevNode in prev_Nodes)
        {
            prevNode.can_Revert = true;
        }
        DataManager.Instance.bless_Dic[this] = false;
    }
    private void Check_PrevNodes_Of_NextNode(Node node)
    {

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        //TODO 툴팁 활성화
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TODO 툴팁 비활성화
    }

}