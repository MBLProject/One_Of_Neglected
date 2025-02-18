using UnityEngine;
using System;
using static Enums;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;

public static class SkillFactory
{
    public static Skill CreateSkill(SkillName skillName, float defaultCooldown)
    {
        switch (skillName)
        {
            case SkillName.Needle: return new Needle(defaultCooldown);
            case SkillName.Claw: return new Claw(defaultCooldown);
            case SkillName.Javelin: return new Javelin(defaultCooldown);
            case SkillName.Aura: return new Aura(defaultCooldown);
            //case SkillName.Cape: return new Cape(defaultCooldown);
            case SkillName.Shuriken: return new Shuriken(defaultCooldown);
            //case SkillName.Gateway: return new Gateway(defaultCooldown);
            case SkillName.Fireball: return new Fireball(defaultCooldown);
            //case SkillName.Ifrit: return new Ifrit(defaultCooldown);
            //case SkillName.Flow: return new Flow(defaultCooldown);
            case SkillName.PoisonShoes:
                return new PoisonShoes(defaultCooldown);
            //case SkillName.GravityField: return new GravityField(defaultCooldown);
            //case SkillName.Mine: return new Mine(defaultCooldown);
            //case SkillName.Blood: return new Blood(defaultCooldown);
            //case SkillName.Water: return new Water(defaultCooldown);
            //case SkillName.Shield: return new Shield(defaultCooldown);
            //case SkillName.Shoes: return new Shoes(defaultCooldown);
            //case SkillName.Fist: return new Fist(defaultCooldown);
            //case SkillName.Ring: return new Ring(defaultCooldown);
            //case SkillName.Book: return new Book(defaultCooldown);
            //case SkillName.Bracelet: return new Bracelet(defaultCooldown);
            //case SkillName.Clock: return new Clock(defaultCooldown);
            //case SkillName.Magnet: return new Magnet(defaultCooldown);
            //case SkillName.Crown: return new Crown(defaultCooldown);
            //case SkillName.Meat: return new Meat(defaultCooldown);
            default:
                Debug.LogWarning($"Unknown SkillName: {skillName}");
                return null;
        }
    }

    public static bool IsActiveSkill(SkillName skillName)
    {
        switch (skillName)
        {
            case Enums.SkillName.Needle:
            case Enums.SkillName.Claw:
            case Enums.SkillName.Javelin:
            case Enums.SkillName.Aura:
            case Enums.SkillName.Shuriken:
            case Enums.SkillName.Fireball:
                return true; // 예시로 액티브 스킬을 true로 반환
            case Enums.SkillName.PoisonShoes:
                return false; // 패시브 스킬은 false
            default:
                Debug.LogWarning($"Unknown SkillName: {skillName}");
                return false;
        }
    }
}
