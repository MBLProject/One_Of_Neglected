using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class Warrior : Player
{
    protected override void Awake()
    {
        base.Awake();
    }

    #region Initialize
    protected override void InitializeStateHandler()
    {
        stateHandler = new StateHandler<Player>(this);
        ClassType = ClassType.Warrior;
        stateHandler.RegisterState(new WarriorIdleState(stateHandler));
        stateHandler.RegisterState(new WarriorMoveState(stateHandler));
        stateHandler.RegisterState(new WarriorDashState(stateHandler));
        stateHandler.RegisterState(new WarriorAttackState(stateHandler));

        stateHandler.ChangeState(typeof(WarriorIdleState));
    }

    protected override void InitializeStats()
    {
        stats = new PlayerStats();
    }

    protected override void InitializeClassType()
    {
        ClassType = ClassType.Warrior;
    }

    protected override void InitializeStatViewer()
    {
        // 기본스텟 여기서 초기화하시오
        statViewer.Level = 1;
        statViewer.MaxExp = 100;
        statViewer.Exp = 0;
        statViewer.MaxHp = 100 + DataManager.Instance.BTS.MaxHp;
        statViewer.Hp = 100 + DataManager.Instance.BTS.MaxHp;
        statViewer.HpRegen = 0 + DataManager.Instance.BTS.HpRegen;
        statViewer.Defense = 0 + DataManager.Instance.BTS.Defense;
        statViewer.Mspd = 1 + DataManager.Instance.BTS.Mspd;
        statViewer.ATK = 5 + DataManager.Instance.BTS.ATK;
        statViewer.Aspd = 1 + DataManager.Instance.BTS.Aspd;
        statViewer.CriRate = 0 + DataManager.Instance.BTS.CriRate;
        statViewer.CriDamage = 50 + DataManager.Instance.BTS.CriDamage;
        statViewer.ProjAmount = 0 + DataManager.Instance.BTS.ProjAmount;
        statViewer.ATKRange = 1 + DataManager.Instance.BTS.ATKRange;
        statViewer.Duration = 0 + DataManager.Instance.BTS.Duration;
        statViewer.Cooldown = 10 + DataManager.Instance.BTS.Cooldown;
        statViewer.Revival = 0 + DataManager.Instance.BTS.Revival;
        statViewer.Magnet = 0.2f + DataManager.Instance.BTS.Magnet;
        statViewer.Growth = 0 + DataManager.Instance.BTS.Growth;
        statViewer.Greed = 0 + DataManager.Instance.BTS.Greed;
        statViewer.Curse = 0 + DataManager.Instance.BTS.Curse;
        statViewer.Reroll = 0 + DataManager.Instance.BTS.Reroll;
        statViewer.Banish = 0 + DataManager.Instance.BTS.Banish;
        statViewer.GodKill = DataManager.Instance.BTS.GodKill;
        statViewer.Barrier =  !DataManager.Instance.BTS.Barrier;
        statViewer.BarrierCooldown = 5 + DataManager.Instance.BTS.BarrierCooldown;
        statViewer.Invincibility = !DataManager.Instance.BTS.Invincibility;
        statViewer.DashCount = 3 + DataManager.Instance.BTS.DashCount;
        statViewer.Adversary = DataManager.Instance.BTS.Adversary;
        statViewer.ProjDestroy = DataManager.Instance.BTS.ProjDestroy;
        statViewer.ProjParry = DataManager.Instance.BTS.projParry;

        UpdateStats();
        
        stats.InitializeStats();
    }
    #endregion
}
