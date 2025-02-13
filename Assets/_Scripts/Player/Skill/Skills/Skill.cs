using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

[Serializable]
public class Skill
{
    public Enums.SkillName skillName;
    public float cooldown;
    protected float defaultCooldown;
    protected float damage = 1f;
    protected int level = 1;
    protected int pierceCount = 0;
    protected int shotCount = 1;
    protected int projectileCount = 1;

    protected bool isSkillActive = false;

    protected Skill(Enums.SkillName skillName, float defaultCooldown)
    {
        this.skillName = skillName;
        this.defaultCooldown = defaultCooldown;
    }

    public virtual async void StartMainTask()
    {
        await StartSkill();
    }

    protected virtual async UniTask StartSkill()
    {
        isSkillActive = true;

        while (isSkillActive)
        {
            if (!GameManager.Instance.isPaused)
            {
                //Debug.Log($"Fire! : {skillName}");
                for (int i = 0; i < shotCount; ++i)
                {
                    for (int j = 0; j < projectileCount; ++j)
                    {
                        Fire();
                        await DelayFloat(0.25f);
                    }
                    await DelayFloat(0.5f);
                }

                float adjustedCooldown = defaultCooldown - (shotCount * 0.5f + projectileCount * 0.25f);
                if (adjustedCooldown > 0) await DelayFloat(adjustedCooldown);
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }

    protected virtual void Fire()
    {
        ProjectileManager.Instance.SpawnProjectile(skillName, damage, level);
    }

    public static async UniTask DelayFloat(float delayInSeconds)
    {
        int delayInMilliseconds = (int)(delayInSeconds * 1000);

        await UniTask.Delay(delayInMilliseconds);
    }
}

