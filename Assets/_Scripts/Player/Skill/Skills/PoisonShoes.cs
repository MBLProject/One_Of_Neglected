using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Linq;

public class PoisonShoes : Skill
{
    public PoisonShoes(float defaultCooldown) : base(Enums.SkillName.PoisonShoes, defaultCooldown) { }

    protected override async UniTask StartSkill()
    {
        isSkillActive = true;

        while (isSkillActive)
        {
            if (!GameManager.Instance.isPaused)
            {
                // PoisonShoesProjectile을 바닥에 깔도록 요청
                ProjectileManager.Instance.SpawnProjectile(skillName, damage, level, shotCount, projectileCount, projectileDelay, shotDelay);
                await UniTask.Delay(TimeSpan.FromSeconds(defaultCooldown));
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }
}
