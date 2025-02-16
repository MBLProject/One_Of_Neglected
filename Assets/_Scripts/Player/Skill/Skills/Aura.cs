using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : Skill
{

    protected Aura(Enums.SkillName skillName, float defaultCooldown, float pierceDelay = 0.1F, float shotDelay = 0.5F) : base(skillName, defaultCooldown, pierceDelay, shotDelay) { }

    protected override async UniTask StartSkill()
    {
        isSkillActive = true;

        while (true)
        {
            if (!GameManager.Instance.isPaused)
            {
                if (isSkillActive)
                {
                    Fire();
                    isSkillActive = false;
                }
            }
            else
            {
                isSkillActive = true;
                await UniTask.Yield();
            }
        }
    }
}