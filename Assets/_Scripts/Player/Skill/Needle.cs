using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Needle : Skill
{
    public Needle(float defaultCooldown) : base(Enums.SkillName.Needle, defaultCooldown) { }

    public override void StartMainTask()
    {
        base.StartMainTask();
        Debug.Log("Start! : Needle");
    }


}
