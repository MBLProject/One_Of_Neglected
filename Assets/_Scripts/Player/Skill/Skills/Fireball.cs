using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ActiveSkill
{
    public Fireball(float defaultCooldown) : base(Enums.SkillName.Fireball, defaultCooldown) { }

}
