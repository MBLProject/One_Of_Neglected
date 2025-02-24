using UnityEngine;
using System;
using static Enums;
using System.Collections.Generic;

public static class SkillFactory
{
    public static Skill CreateSkill(SkillName skillName)
    {
        switch (skillName)
        {
            case SkillName.Needle: return new Needle();
            case SkillName.Claw: return new Claw();
            case SkillName.Javelin: return new Javelin();
            case SkillName.Aura: return new Aura();
            //case SkillName.Cape: return new Cape();
            case SkillName.Shuriken: return new Shuriken();
            case SkillName.Gateway: return new Gateway();
            case SkillName.Fireball: return new Fireball();
            //case SkillName.Ifrit: return new Ifrit();
            //case SkillName.Flow: return new Flow();
            case SkillName.PoisonShoes: return new PoisonShoes();
            //case SkillName.GravityField: return new GravityField();
            case SkillName.Mine: return new Mine();
            case SkillName.Blood: return new Blood();
            case SkillName.Water: return new Water();
            case SkillName.Shield: return new Shield();
            case SkillName.Shoes: return new Shoes();
            case SkillName.Fist: return new Fist();
            case SkillName.Ring: return new Ring();
            case SkillName.Book: return new Book();
            case SkillName.Bracelet: return new Bracelet();
            case SkillName.Clock: return new Clock();
            case SkillName.Magnet: return new Magnet();
            case SkillName.Crown: return new Crown();
            case SkillName.Meat: return new Meat();
            case SkillName.Cheese: return new Cheese();
            default:
                Debug.LogWarning($"Unknown SkillName: {skillName}");
                return null;
        }
    }
    public static bool IsActiveSkill(SkillName skillName)
    {
        switch (skillName)
        {
            case SkillName.Needle:
            case SkillName.Claw:
            case SkillName.Javelin:
            case SkillName.Aura:
            case SkillName.Cape:
            case SkillName.Shuriken:
            case SkillName.Gateway:
            case SkillName.Fireball:
            case SkillName.Ifrit:
            case SkillName.Flow:
            case SkillName.PoisonShoes:
            case SkillName.GravityField:
            case SkillName.Mine:
                return true;

            case SkillName.Blood:
            case SkillName.Water:
            case SkillName.Shield:
            case SkillName.Shoes:
            case SkillName.Fist:
            case SkillName.Ring:
            case SkillName.Book:
            case SkillName.Bracelet:
            case SkillName.Clock:
            case SkillName.Magnet:
            case SkillName.Crown:
            case SkillName.Meat:
                return false;

            case SkillName.Cheese:
            case SkillName.Gold:
                return false;

            default:
                Debug.LogWarning($"Unknown SkillName: {skillName}");
                return false;
        }
    }

}

[Serializable]
public struct SkillStats
{
    public float defaultCooldown;
    public float defaultATKRange;
    public float defaultDamage;
    public float aTK;
    public int pierceCount;
    public int shotCount;
    public int projectileCount;
    public float projectileDelay;
    public float shotDelay;
    public float aTKRange;
    public float critical;
    public float cATK;
    public float amount;
    public float lifetime;
    public float cooldown;
    public float Aspd;
    public float projectileSpeed;
}

[Serializable]
public struct ProjectileStats
{
    public SkillName skillName;
    public int level;
    public float finalCooldown;
    public float finalATKRange;
    public float finalDamage;
    public int pierceCount;
    public int shotCount;
    public int projectileCount;
    public float projectileDelay;
    public float shotDelay;
    public float critical;
    public float cATK;
    public float amount;
    public float lifetime;
    public float projectileSpeed;
}
