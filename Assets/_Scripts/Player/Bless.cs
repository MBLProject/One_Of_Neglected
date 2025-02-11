using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bless : MonoBehaviour
{
    #region 공격가호

    public void ATK_Increase()
    {
        Debug.Log("공격력 증가");
    }

    public void Projectile_Increase()
    {
        Debug.Log("투사체 증가");
    }

    public void ATKSpeed_Increase()
    {
        Debug.Log("공속 증가");
    }

    public void CriticalDamage_Increase()
    {
        Debug.Log("치뎀 증가");
    }

    public void CriticalPercent_Increase()
    {
        Debug.Log("치확 증가");
    }

    public void Projectile_Destroy()
    {
        Debug.Log("투사체 파괴");
    }

    public void Projectile_Parry()
    {
        Debug.Log("투사체 패링");
    }

    public void God_Kill()
    {
        Debug.Log("신살");
    }

    #endregion

    #region 방어가호

    public void MaxHP_Increase()
    {
        Debug.Log("최대체력 증가");
    }

    public void Defense_Increase()
    {
        Debug.Log("방어력 증가");
    }

    public void HPRegen_Increase()
    {
        Debug.Log("체력회복 증가");
    }

    public void Barrier_Activate()
    {
        Debug.Log("배리어 생성");
    }

    public void Barrier_Cooldown()
    {
        Debug.Log("배리어 회복");
    }

    public void Invincibility()
    {
        Debug.Log("피격 시 무적");
    }

    public void Adversary()
    {
        Debug.Log("대적자");
    }

    #endregion

    #region 유틸가호

    public void ATK_Range()
    {
        Debug.Log("공격범위");
    }

    public void Duration()
    {
        Debug.Log("지속시간");
    }

    public void Cooldown()
    {
        Debug.Log("쿨타임");
    }

    public void Resurrection()
    {
        Debug.Log("부활");
    }

    public void Magnet()
    {
        Debug.Log("자석");
    }

    public void Growth()
    {
        Debug.Log("성장");
    }

    public void Avarice()
    {
        Debug.Log("탐욕");
    }

    public void Dash()
    {
        Debug.Log("대쉬");
    }

    #endregion
}
