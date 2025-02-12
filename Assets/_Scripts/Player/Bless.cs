using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bless : MonoBehaviour
{
    #region 공격가호

    public void ATK_Modify()
    {
        Debug.Log("공격력 증가");
    }

    public void ProjAmount_Modify()
    {
        Debug.Log("투사체 증가");
    }

    public void ASPD_Modify()
    {
        Debug.Log("공속 증가");
    }

    public void CriDamage_Modify()
    {
        Debug.Log("치뎀 증가");
    }

    public void CriRate_Modify()
    {
        Debug.Log("치확 증가");
    }

    public void ProjDestroy_Modify()
    {
        Debug.Log("투사체 파괴");
    }

    public void ProjParry_Modify()
    {
        Debug.Log("투사체 패링");
    }

    public void GodKill_Modify()
    {
        Debug.Log("신살");
    }

    #endregion

    #region 방어가호

    public void MaxHP_Modify()
    {
        Debug.Log("최대체력 증가");
    }

    public void Defense_Modify()
    {
        Debug.Log("방어력 증가");
    }

    public void HPRegen_Modify()
    {
        Debug.Log("체력회복 증가");
    }

    public void Barrier_Modify()
    {
        Debug.Log("배리어 생성");
    }

    public void BarrierCooldown_Modify()
    {
        Debug.Log("배리어 회복");
    }

    public void Invincibility_Modify()
    {
        Debug.Log("피격 시 무적");
    }

    public void Adversary_Modify()
    {
        Debug.Log("대적자");
    }

    #endregion

    #region 유틸가호

    public void ATKRange_Modify()
    {
        Debug.Log("공격범위");
    }

    public void Duration_Modify()
    {
        Debug.Log("지속시간");
    }

    public void Cooldown_Modify()
    {
        Debug.Log("쿨타임");
    }

    public void Revival_Modify()
    {
        Debug.Log("부활");
    }

    public void Magnet_Modify()
    {
        Debug.Log("자석");
    }

    public void Growth_Modify()
    {
        Debug.Log("성장");
    }

    public void Greed_Modify()
    {
        Debug.Log("탐욕");
    }

    public void DashCount_Modify()
    {
        Debug.Log("대쉬");
    }

    #endregion
}
