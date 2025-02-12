public class Enums
{
    public enum ClassType
    {
        None,
        Warrior,
        Log,
        Archer,
        Magician,
        Trapper,
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

    public enum ExpType
    {
        None,
        White,
        Green,
        Blue,
        Red,
        Purple, // 고유 몬스터 처치시 획득, 일정레벨 이하에선 LV+1, 초과시 최대 경험치의 n%만큼 지급
    }

    public enum StatType
    {
        Level,
        Exp,
        MaxHp,
        Hp,
        Recovery,
        Armor,
        Mspd,
        ATK,
        Aspd,
        Critical,
        CATK,
        Amount,
        Area,
        Duration,
        Cooldown,
        Revival,
        Magnet,
        Growth,
        Greed,
        Curse,
        Reroll,
        Banish
    }

}

