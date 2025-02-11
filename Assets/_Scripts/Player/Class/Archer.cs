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
        statViewer.MaxHp = 100;
        statViewer.Hp = 100;
        statViewer.Recovery = 0;
        statViewer.Armor = 0;
        statViewer.Mspd = 1;
        statViewer.ATK = 1;
        statViewer.Aspd = 1;
        statViewer.Critical = 0;
        statViewer.CATK = 100;
        statViewer.Amount = 0;
        statViewer.Area = 1;
        statViewer.Duration = 1;
        statViewer.Cooldown = 1;
        statViewer.Revival = 0;
        statViewer.Magnet = 10;
        statViewer.Growth = 0;
        statViewer.Greed = 0;
        statViewer.Curse = 0;
        statViewer.Reroll = 0;
        statViewer.Banish = 0;

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
