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

    protected float projectileDelay = 0.1f;
    protected float shotDelay = 0.5f;

    protected bool isSkillActive = false;

    public bool IsActive { get; protected set; }

    protected Skill(Enums.SkillName skillName, float defaultCooldown, float pierceDelay = 0.1f, float shotDelay = 0.5f)
    {
        this.skillName = skillName;
        this.defaultCooldown = defaultCooldown;
        this.projectileDelay = pierceDelay;
        this.shotDelay = shotDelay;
    }

    public virtual void StartMainTask()
    {
    }

    public virtual void StopMainTask()
    {
    }

    public virtual void InitSkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay)
    {
        this.damage = damage;
        this.level = level;
        this.pierceCount = pierceCount;
        this.shotCount = shotCount;
        this.projectileCount = projectileCount;
        this.projectileDelay = projectileDelay;
        this.shotDelay = shotDelay;
    }
}
