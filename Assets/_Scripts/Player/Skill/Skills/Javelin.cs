using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javelin : Skill
{
    public Javelin(float defaultCooldown) : base(Enums.SkillName.Javelin, defaultCooldown) { }

    public override void StartMainTask()
    {
        base.StartMainTask();
        //Debug.Log("Start! : Javelin");
    }

}
