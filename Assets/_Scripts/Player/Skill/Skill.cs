using UnityEngine;
using Cysharp.Threading.Tasks;

public class Skill
{
    public Enums.SkillName skillName;
    public float cooldown;
    private float defaultCooldown;

    private bool isSkillActive = false;

    protected Skill(Enums.SkillName skillName, float defaultCooldown)
    {
        this.skillName = skillName;
        this.defaultCooldown = defaultCooldown;
    }

    private async void Start()
    {
        await StartSkill();
    }

    private async UniTask StartSkill()
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
        Debug.Log($"Fire {skillName.ToString()}!");
        ProjectileManager.Instance.SpawnProjectile(skillName);
    }

    public static async UniTask DelayFloat(float delayInSeconds)
    {
        int delayInMilliseconds = (int)(delayInSeconds * 1000);

        await UniTask.Delay(delayInMilliseconds);
    }
}


