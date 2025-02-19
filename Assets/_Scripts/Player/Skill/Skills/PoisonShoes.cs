using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Linq;
using System;

public class PoisonShoes : ActiveSkill
{
    public PoisonShoes(float defaultCooldown) : base(Enums.SkillName.PoisonShoes, defaultCooldown) { }

}
