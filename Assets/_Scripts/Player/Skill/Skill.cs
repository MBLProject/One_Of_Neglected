using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
[Serializable]
public class Skill
{
    public Enums.SkillName skillName;
    public float cooldown;
    private float defaultCooldown;
    private float damage = 1f;
    private int level = 1;

    private bool isSkillActive = false;

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
                Debug.Log($"Fire! : {skillName}");
                Fire();
                await DelayFloat(defaultCooldown);
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


