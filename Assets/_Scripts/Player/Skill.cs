using UnityEngine;

public class Skill
{
    public SkillName skillName;
    public float cooldown;
    private float currentCooldown;

    public Skill(SkillName name, float cd)
    {
        skillName = name;
        cooldown = cd;
        currentCooldown = cd;
    }

    public void UpdateSkill(float deltaTime)
    {
        currentCooldown -= deltaTime;
        if (currentCooldown <= 0)
        {
            Fire(); // 스킬 발사
            currentCooldown = cooldown; // 쿨타임 리셋
        }
    }

    private void Fire()
    {
        Debug.Log($"Fire {skillName.ToString()}!");
        ProjectileManager.Instance.SpawnProjectile(skillName);
    }
}


public enum SkillName
{
    None,
    Needle,
    Clow,
    Javelin,
    Aura,
    Cape,
    Shuriken,
    Gateway,
    Fireball,
    Ifrit,
    Flow,
    PoisonShoes,
    GravityField,
    Mine,
}