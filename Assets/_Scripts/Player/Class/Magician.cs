using UnityEngine;
using static Enums;

public class Magician : Player
{
    protected override void Awake()
    {
        base.Awake();
        //InitializeComponents();
        //InitializeStateHandler();
        //InitializeStats();
        //InitializeClassType();
    }

    #region Initialize
    protected override void InitializeStateHandler()
    {
        stateHandler = new StateHandler<Player>(this);
        stateHandler.RegisterState(new WarriorIdleState(stateHandler));
        stateHandler.RegisterState(new WarriorMoveState(stateHandler));
        stateHandler.RegisterState(new WarriorDashState(stateHandler));
        stateHandler.RegisterState(new WarriorAttackState(stateHandler));

        stateHandler.ChangeState(typeof(WarriorIdleState));
    }

    protected override void InitializeStats()
    {
        stats = new PlayerStats();
        stats.CurrentLevel = 1;
        stats.OnLevelUp += HandleLevelUp;
    }

    protected override void InitializeClassType()
    {
        ClassType = ClassType.Archer;
    }

    protected override void InitializeStatViewer()
    {
        statViewer.Level = 1;
        statViewer.MaxExp = 100;
        statViewer.Exp = 0;
        statViewer.MaxHp = 70;  // 마법사는 체력이 가장 낮음
        statViewer.Hp = 70;
        statViewer.HpRegen = 0;
        statViewer.Defense = 0;
        statViewer.Mspd = 0.9f;  // 이동속도가 좀 더 느림
        statViewer.ATK = 1.2f;   // 공격력이 좀 더 높음
        statViewer.Aspd = 0.8f;  // 공격속도가 좀 더 느림
        statViewer.CriRate = 0;
        statViewer.CriDamage = 100;
        statViewer.ProjAmount = 1;  // 기본적으로 투사체 1개
        statViewer.ATKRange = 2f;   // 공격범위가 가장 넓음
        statViewer.Duration = 1;
        statViewer.Cooldown = 1;
        statViewer.Revival = 0;
        statViewer.Magnet = 10;
        statViewer.Growth = 0;
        statViewer.Greed = 0;
        statViewer.Curse = 0;
        statViewer.Reroll = 0;
        statViewer.Banish = 0;
        statViewer.GodKill = false;
        statViewer.Barrier = false;
        statViewer.BarrierCooldown = 0;
        statViewer.Invincibility = false;
        statViewer.DashCount = 3;
        statViewer.Adversary = false;
        statViewer.ProjDestroy = false;
        statViewer.ProjParry = false;

        UpdateStats();
        
    }
    #endregion


    private void HandleLevelUp(int levelAmount)
    {
        UpdateStats();
    }



    private void OnDestroy()
    {
        if (stats != null)
        {
            stats.OnLevelUp -= HandleLevelUp;
        }
    }
}
