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
    None,
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
    None,
    MAX_HP_INCREASE,
    DEFENSE_INCREASE,
    HP_REGEN_INCREASE,
    BARRIER_ACTIVATE,
    BARRIER_COOLDOWN,
    INVINCIBILITY,
    ADVERSARY
}
public enum UTI_Bless
{
    None,
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
    public Bless bless;
    //공통된걸 등록해줄 리스트
    public List<Node> ATK_Node_List;
    public List<Node> DEF_Node_List;
    public List<Node> UIL_Node_List;

    private void Awake()
    {

    }

    private void Start()
    {
        Node_Initialize(ref ATK_Node_List);
        Node_Initialize(ref DEF_Node_List);
        Node_Initialize(ref UIL_Node_List);
    }

    //딕셔너리 초기화 및 불러온 데이터에 따라 노드활성화
    private void Node_Initialize(ref List<Node> nodes)
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
                node.can_Interactable = false;
            }
            ByNodeDefine(node);
        }
    }
    private void ByNodeDefine(Node node)
    {
        switch (node.nodeDefine)
        {
            case NodeDefine.ATK:
                Add_ATK_Bless(node);
                break;
            case NodeDefine.DEF:
                Add_DEF_Bless(node);
                break;
            case NodeDefine.UIL:
                Add_UTI_Bless(node);
                break;
        }
    }
    private void Add_ATK_Bless(Node node)
    {
        switch (node.ATK_Bless)
        {
            case ATK_Bless.ATK_INCREASE:
                node.m_BTN.onClick.AddListener(bless.ATK_Modify);
                break;
            case ATK_Bless.PROJECTILE_INCREASE:
                node.m_BTN.onClick.AddListener(bless.ProjAmount_Modify);
                break;
            case ATK_Bless.ATK_SPEED_INCREASE:
                node.m_BTN.onClick.AddListener(bless.ASPD_Modify);
                break;
            case ATK_Bless.CRITICAL_DAMAGE_INCREASE:
                node.m_BTN.onClick.AddListener(bless.CriDamage_Modify);
                break;
            case ATK_Bless.CRITICAL_PERCENT_INCREASE:
                node.m_BTN.onClick.AddListener(bless.CriRate_Modify);
                break;
            case ATK_Bless.PROJECTILE_DESTROY:
                node.m_BTN.onClick.AddListener(bless.ProjDestroy_Modify);
                break;
            case ATK_Bless.PROJECTILE_PARRY:
                node.m_BTN.onClick.AddListener(bless.ProjParry_Modify);
                break;
            case ATK_Bless.GOD_KILL:
                node.m_BTN.onClick.AddListener(bless.GodKill_Modify);
                break;
            default:
                Debug.Log("공격 메서드가 리스너에 구독 안됨");
                break;
        }
    }
    private void Add_DEF_Bless(Node node)
    {
        switch (node.DEF_Bless)
        {
            case DEF_Bless.MAX_HP_INCREASE:
                node.m_BTN.onClick.AddListener(bless.MaxHP_Modify);
                break;
            case DEF_Bless.DEFENSE_INCREASE:
                node.m_BTN.onClick.AddListener(bless.Defense_Modify);
                break;
            case DEF_Bless.HP_REGEN_INCREASE:
                node.m_BTN.onClick.AddListener(bless.HPRegen_Modify);
                break;
            case DEF_Bless.BARRIER_ACTIVATE:
                node.m_BTN.onClick.AddListener(bless.Barrier_Modify);
                break;
            case DEF_Bless.BARRIER_COOLDOWN:
                node.m_BTN.onClick.AddListener(bless.BarrierCooldown_Modify);
                break;
            case DEF_Bless.INVINCIBILITY:
                node.m_BTN.onClick.AddListener(bless.Invincibility_Modify);
                break;
            case DEF_Bless.ADVERSARY:
                node.m_BTN.onClick.AddListener(bless.Adversary_Modify);
                break;
            default:
                Debug.Log("방어 메서드가 리스너에 구독 안됨");
                break;
        }
    }

    private void Add_UTI_Bless(Node node)
    {
        switch (node.UTI_Bless)
        {
            case UTI_Bless.ATK_RANGE:
                node.m_BTN.onClick.AddListener(bless.ATKRange_Modify);
                break;
            case UTI_Bless.DURATION:
                node.m_BTN.onClick.AddListener(bless.Duration_Modify);
                break;
            case UTI_Bless.COOLDOWN:
                node.m_BTN.onClick.AddListener(bless.Cooldown_Modify);
                break;
            case UTI_Bless.RESURRECTION:
                node.m_BTN.onClick.AddListener(bless.Revival_Modify);
                break;
            case UTI_Bless.MAGNET:
                node.m_BTN.onClick.AddListener(bless.Magnet_Modify);
                break;
            case UTI_Bless.GROWTH:
                node.m_BTN.onClick.AddListener(bless.Growth_Modify);
                break;
            case UTI_Bless.AVARICE:
                node.m_BTN.onClick.AddListener(bless.Greed_Modify);
                break;
            case UTI_Bless.DASH:
                node.m_BTN.onClick.AddListener(bless.DashCount_Modify);
                break;
            default:
                Debug.Log("유틸 메서드가 리스너에 구독 안됨");
                break;
        }
    }

}
