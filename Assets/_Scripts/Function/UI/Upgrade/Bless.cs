using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bless : MonoBehaviour
{

    #region 공격가호
    public void ATK_Modify(bool On, ref int point, float value = 5)
    {
        if (point == 0) return;

        Debug.Log("공격력");
        if (On) DataManager.Instance.BTS.ATK += value;
        else DataManager.Instance.BTS.ATK -= value;
        point = On ? point - 1 : point + 1;
    }

    public void ProjAmount_Modify(bool On, ref int point, int value = 1)
    {
        if (point == 0) return;

        Debug.Log("투사체");

        if (On) DataManager.Instance.BTS.ProjAmount += value;
        else DataManager.Instance.BTS.ProjAmount -= value;

        point = On ? point - 1 : point + 1;
    }

    public void ASPD_Modify(bool On, ref int point, float value = 2)
    {
        if (point == 0) return;

        Debug.Log("공속");
        if (On) DataManager.Instance.BTS.Aspd += value;
        else DataManager.Instance.BTS.Aspd -= value;

        point = On ? point - 1 : point + 1;
    }

    public void CriDamage_Modify(bool On, ref int point, float value = 10)
    {
        if (point == 0) return; ;

        Debug.Log("치뎀");
        if (On) DataManager.Instance.BTS.CriDamage += value;
        else DataManager.Instance.BTS.CriDamage -= value;

        point = On ? point - 1 : point + 1;
    }

    public void CriRate_Modify(bool On, ref int point, float value = 15)
    {
        if (point == 0) return;

        Debug.Log("치확 ");
        if (On) DataManager.Instance.BTS.CriRate += value;
        else DataManager.Instance.BTS.CriRate -= value;

        point = On ? point - 1 : point + 1;
    }

    public void ProjDestroy_Modify(bool On, ref int point)
    {
        if (point == 0) return;

        Debug.Log("파괴");
        DataManager.Instance.BTS.ProjDestroy = On;

        point = On ? point - 1 : point + 1;
    }

    public void ProjParry_Modify(bool On, ref int point)
    {
        if (point == 0) return;

        Debug.Log("패링");
        DataManager.Instance.BTS.projParry = On;

        point = On ? point - 1 : point + 1;
    }

    public void GodKill_Modify(bool On, ref int point)
    {
        if (point == 0) return;

        Debug.Log("신살");
        DataManager.Instance.BTS.GodKill = On;

        point = On ? point - 1 : point + 1;
    }

    #endregion

    #region 방어가호

    public void MaxHP_Modify(bool On, ref int point, int value = 10)
    {
        if (point == 0) return;

        Debug.Log("최대체력 ");
        if (On) DataManager.Instance.BTS.MaxHp += value;
        else DataManager.Instance.BTS.MaxHp -= value;

        point = On ? point - 1 : point + 1;
    }

    public void Defense_Modify(bool On, ref int point, int value = 2)
    {
        if (point == 0) return;

        Debug.Log("방어력 ");
        if (On) DataManager.Instance.BTS.Defense += value;
        else DataManager.Instance.BTS.Defense -= value;

        point = On ? point - 1 : point + 1;
    }

    public void HPRegen_Modify(bool On, ref int point, float value = 1)
    {
        if (point == 0) return;

        Debug.Log("체력회복 ");
        if (On) DataManager.Instance.BTS.HpRegen += value;
        else DataManager.Instance.BTS.HpRegen -= value;

        point = On ? point - 1 : point + 1;
    }

    public void Barrier_Modify(bool On, ref int point)
    {
        if (point == 0) return;

        Debug.Log("배리어 ");
        DataManager.Instance.BTS.Barrier = On;

        point = On ? point - 1 : point + 1;
    }

    public void BarrierCooldown_Modify(bool On, ref int point)
    {
        if (point == 0) return;

        Debug.Log("배리어 ");
        if (On)
        {
            if (DataManager.Instance.BTS.BarrierCooldown == 7)
                DataManager.Instance.BTS.BarrierCooldown = 5;

            if (DataManager.Instance.BTS.BarrierCooldown == 0)
                DataManager.Instance.BTS.BarrierCooldown = 7;
        }
        else
        {
            if (DataManager.Instance.BTS.BarrierCooldown == 7)
                DataManager.Instance.BTS.BarrierCooldown = 0;

            if (DataManager.Instance.BTS.BarrierCooldown == 5)
                DataManager.Instance.BTS.BarrierCooldown = 7;
        }
        point = On ? point - 1 : point + 1;
    }

    public void Invincibility_Modify(bool On, ref int point)
    {
        if (point == 0) return;

        Debug.Log("피격 시 무적");
        DataManager.Instance.BTS.Invincibility = On;

        point = On ? point - 1 : point + 1;
    }

    public void Adversary_Modify(bool On, ref int point)
    {
        if (point == 0) return;

        Debug.Log("대적자");
        DataManager.Instance.BTS.Adversary = On;

        point = On ? point - 1 : point + 1;
    }

    #endregion

    #region 유틸가호

    public void ATKRange_Modify(bool On, ref int point, float value = 5)
    {
        if (point == 0) return;

        Debug.Log("공격범위");
        if (On) DataManager.Instance.BTS.ATKRange += value;
        else DataManager.Instance.BTS.ATKRange -= value;

        point = On ? point - 1 : point + 1;
    }

    public void Duration_Modify(bool On, ref int point, float value = 5)
    {
        if (point == 0) return;

        Debug.Log("지속시간");
        if (On) DataManager.Instance.BTS.Duration += value;
        else DataManager.Instance.BTS.Duration -= value;

        point = On ? point - 1 : point + 1;
    }

    public void Cooldown_Modify(bool On, ref int point, float value = 5)
    {
        if (point == 0) return;

        Debug.Log("쿨타임");
        if (On) DataManager.Instance.BTS.Cooldown += value;
        else DataManager.Instance.BTS.Cooldown -= value;

        point = On ? point - 1 : point + 1;
    }

    public void Revival_Modify(bool On, ref int point, int value = 1)
    {
        if (point == 0) return;

        Debug.Log("부활");
        if (On) DataManager.Instance.BTS.Revival += value;
        else DataManager.Instance.BTS.Revival -= value;

        point = On ? point - 1 : point + 1;
    }

    public void Magnet_Modify(bool On, ref int point, float value = 5)
    {
        if (point == 0) return;

        Debug.Log("자석");
        if (On) DataManager.Instance.BTS.Magnet += value;
        else DataManager.Instance.BTS.Magnet -= value;

        point = On ? point - 1 : point + 1;
    }

    public void Growth_Modify(bool On, ref int point, float value = 5)
    {
        if (point == 0) return;

        Debug.Log("성장");
        if (On) DataManager.Instance.BTS.Growth += value;
        else DataManager.Instance.BTS.Growth -= value;

        point = On ? point - 1 : point + 1;
    }

    public void Greed_Modify(bool On, ref int point, float value = 5)
    {
        if (point == 0) return;

        Debug.Log("탐욕");
        if (On) DataManager.Instance.BTS.Greed += value;
        else DataManager.Instance.BTS.Greed -= value;

        point = On ? point - 1 : point + 1;
    }

    public void DashCount_Modify(bool On, ref int point, int value = 1)
    {
        if (point == 0) return;

        Debug.Log("대쉬");
        if (On) DataManager.Instance.BTS.DashCount += value;
        else DataManager.Instance.BTS.DashCount -= value;

        point = On ? point - 1 : point + 1;
    }

    #endregion
}
