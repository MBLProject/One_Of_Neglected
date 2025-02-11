using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public NodeDefine nodeDefine;
    public ATKNode ATK_Node;
    public DEFNode DEF_Node;
    public UILNode UIL_Node;
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (can_Revert)
            {
                m_BTN.interactable = false;
                DataManager.Instance.bless_Dic[this] = false;
            }
        }
    }
    public void Clicked()
    {
        can_Revert = true;
        clicked = true;

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