using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Needle : Skill
{
    public Needle(float defaultCooldown) : base(Enums.SkillName.Needle, defaultCooldown) { projectileCount = 10; }
}
