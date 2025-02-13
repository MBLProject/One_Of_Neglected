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

    public NodeReset nodeReset;
    private void Awake()
    {

    }

    private void Start()
    {
        DataManager.Instance.LoadBlessData();
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
                node.m_BTN.onClick?.Invoke();
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
                node.m_BTN.onClick.AddListener(() => bless.ATK_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.ATK_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case ATK_Bless.PROJECTILE_INCREASE:
                node.m_BTN.onClick.AddListener(() => bless.ProjAmount_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.ProjAmount_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case ATK_Bless.ATK_SPEED_INCREASE:
                node.m_BTN.onClick.AddListener(() => bless.ASPD_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.ASPD_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case ATK_Bless.CRITICAL_DAMAGE_INCREASE:
                node.m_BTN.onClick.AddListener(() => bless.CriDamage_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.CriDamage_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case ATK_Bless.CRITICAL_PERCENT_INCREASE:
                node.m_BTN.onClick.AddListener(() => bless.CriRate_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.CriRate_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case ATK_Bless.PROJECTILE_DESTROY:
                node.m_BTN.onClick.AddListener(() => bless.ProjDestroy_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.ProjDestroy_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case ATK_Bless.PROJECTILE_PARRY:
                node.m_BTN.onClick.AddListener(() => bless.ProjParry_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.ProjParry_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case ATK_Bless.GOD_KILL:
                node.m_BTN.onClick.AddListener(() => bless.GodKill_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.GodKill_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
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
                node.m_BTN.onClick.AddListener(() => bless.MaxHP_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.MaxHP_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case DEF_Bless.DEFENSE_INCREASE:
                node.m_BTN.onClick.AddListener(() => bless.Defense_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.Defense_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case DEF_Bless.HP_REGEN_INCREASE:
                node.m_BTN.onClick.AddListener(() => bless.HPRegen_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.HPRegen_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case DEF_Bless.BARRIER_ACTIVATE:
                node.m_BTN.onClick.AddListener(() => bless.Barrier_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.Barrier_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case DEF_Bless.BARRIER_COOLDOWN:
                node.m_BTN.onClick.AddListener(() => bless.BarrierCooldown_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.BarrierCooldown_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case DEF_Bless.INVINCIBILITY:
                node.m_BTN.onClick.AddListener(() => bless.Invincibility_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.Invincibility_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case DEF_Bless.ADVERSARY:
                node.m_BTN.onClick.AddListener(() => bless.Adversary_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.Adversary_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
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
                node.m_BTN.onClick.AddListener(() => bless.ATKRange_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.ATKRange_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case UTI_Bless.DURATION:
                node.m_BTN.onClick.AddListener(() => bless.Duration_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.Duration_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case UTI_Bless.COOLDOWN:
                node.m_BTN.onClick.AddListener(() => bless.Cooldown_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.Cooldown_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case UTI_Bless.RESURRECTION:
                node.m_BTN.onClick.AddListener(() => bless.Revival_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.Revival_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case UTI_Bless.MAGNET:
                node.m_BTN.onClick.AddListener(() => bless.Magnet_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.Magnet_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case UTI_Bless.GROWTH:
                node.m_BTN.onClick.AddListener(() => bless.Growth_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.Growth_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case UTI_Bless.AVARICE:
                node.m_BTN.onClick.AddListener(() => bless.Greed_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.Greed_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            case UTI_Bless.DASH:
                node.m_BTN.onClick.AddListener(() => bless.DashCount_Modify
                (true, ref DataManager.Instance.player_Property.Bless_Point));
                node.revertAction += (x) => bless.DashCount_Modify
                (false, ref DataManager.Instance.player_Property.Bless_Point);
                break;
            default:
                Debug.Log("유틸 메서드가 리스너에 구독 안됨");
                break;
        }
    }

}
