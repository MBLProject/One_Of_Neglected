using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : ActiveSkill
{
    public Shuriken(float defaultCooldown) : base(Enums.SkillName.Shuriken, defaultCooldown) { pierceCount = 10; }


}
