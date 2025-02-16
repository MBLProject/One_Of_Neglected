using UnityEngine;
using static Enums;

public class Archer : Player
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
        stateHandler.RegisterState(new ArcherIdleState(stateHandler));
        stateHandler.RegisterState(new ArcherMoveState(stateHandler));
        stateHandler.RegisterState(new ArcherDashState(stateHandler));
        stateHandler.RegisterState(new ArcherAttackState(stateHandler));

        stateHandler.ChangeState(typeof(ArcherIdleState));
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
        statViewer.MaxHp = 80;
        statViewer.Hp = 80;
        statViewer.HpRegen = 0;
        statViewer.Defense = 0;
        statViewer.Mspd = 1.2f;
        statViewer.ATK = 1;
        statViewer.Aspd = 1.2f;
        statViewer.CriRate = 5;
        statViewer.CriDamage = 150;
        statViewer.ProjAmount = 0;
        statViewer.ATKRange = 1.5f;
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
        
        stats.InitializeStats();
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
