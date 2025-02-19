using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gateway : ActiveSkill
{
    public Gateway(float defaultCooldown, float pierceDelay = 0.1f, float shotDelay = 0.5f) : base(Enums.SkillName.Gateway, defaultCooldown, pierceDelay, shotDelay) { }


}
