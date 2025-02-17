using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : Skill
{
    public Aura(float defaultCooldown, float pierceDelay = 0.1f, float shotDelay = 0.5f) : base(Enums.SkillName.Aura, defaultCooldown, pierceDelay, shotDelay) { }

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