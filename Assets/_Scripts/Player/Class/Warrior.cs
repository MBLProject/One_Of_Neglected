using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class Warrior : Player
{
    protected int level = 1;
    
    protected override void Awake()
    {
        base.Awake();
    }
     
    public virtual void LevelUp()
    {
        level++;
    }

    protected override void InitializeStats()
    {
        stats = new PlayerStats();
        stats.maxHealth = 100;
        stats.attackPower = 10;
        stats.moveSpeed = 1f;  
        stats.damageReduction = 0.1f;
        stats.healthRegen = 1f;
        stats.regenInterval = 1f;
        
        stats.InitializeStats();
    }

    protected override void InitializeStateHandler()
    {
        stateHandler = new StateHandler<Player>(this);

        stateHandler.RegisterState(new WarriorIdleState(stateHandler));
        stateHandler.RegisterState(new WarriorMoveState(stateHandler));
        stateHandler.RegisterState(new WarriorDashState(stateHandler));
        stateHandler.RegisterState(new WarriorAttackState(stateHandler));

        stateHandler.ChangeState(typeof(WarriorIdleState));
    }

    protected override void HandleSkillInput()
    {
        stats.UpadateHealthRegen(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateHandler.ChangeState(typeof(WarriorDashState));
        }
    }

    protected override void InitializeClassType()
    {
        ClassType = ClassType.Warrior;
    }
}
