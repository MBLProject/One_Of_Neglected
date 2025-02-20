using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Aura : ActiveSkill
{
    private bool hasSpawned = false;

    public Aura() : base(Enums.SkillName.Aura) { }

    protected override async UniTask StartSkill()
    {
        isSkillActive = true;

        while (true)
        {
            if (!GameManager.Instance.isPaused && isSkillActive)
            {
                if (!hasSpawned)
                {
                    Fire();
                    hasSpawned = true;
                }
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            else
            {
                hasSpawned = false;
                await UniTask.Yield();
            }
        }
    }
}