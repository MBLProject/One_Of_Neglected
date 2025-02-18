using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossMonsterBase : MonsterBase
{
    [Header("보스 설정")]
    [SerializeField] protected BossType[] possibleStats;       
    [SerializeField] protected GameObject attackEffectPrefab;  
    [SerializeField] protected GameObject skillEffectPrefab;   

    protected bool isInvulnerable;                              // 무적 상태
    protected List<AttackPattern> attackPatterns;               // 공격 패턴
    protected int currentPatternIndex;                          // 현재 패턴

    public AttackPattern CurrentPattern => attackPatterns[currentPatternIndex];

    protected override void Awake()
    {
        base.Awake();
        SetupBoss();
    }
    protected override void InitializeComponents()
    {
        base.InitializeComponents(); 
    }

    // 보스 초기 설정
    private void SetupBoss()
    {
        // 플레이어 스킬 상태에 따른 스탯 설정
        BTS playerSkills = DataManager.Instance.BTS;
        int statIndex;

        if (!playerSkills.Adversary && !playerSkills.GodKill)
        {
            statIndex = 0;           // 스킬 없음 - 무적
            isInvulnerable = true;
        }
        else if (playerSkills.Adversary && !playerSkills.GodKill)
        {
            statIndex = 1;           // Adversary만
            isInvulnerable = false;
        }
        else if (!playerSkills.Adversary && playerSkills.GodKill)
        {
            statIndex = 2;           // GodKill만
            isInvulnerable = false;
        }
        else
        {
            statIndex = 3;           // 둘 다 있음
            isInvulnerable = false;
        }
        BossType selectedStats = possibleStats[statIndex];
        stats = new MonsterStats(
            selectedStats.health,
            selectedStats.moveSpeed,
            selectedStats.damage,
            selectedStats.attackSpeed,
            selectedStats.defense,
            selectedStats.healthRegen
        );
    }

    public override void TakeDamage(float damage)
    {
        if (isInvulnerable) return;
        base.TakeDamage(damage);
    }
}

[Serializable]
public struct BossType
{
    public string typeName;      // 보스 타입 이름 (God Incarnate 등)
    public float health;         // 체력
    public float damage;         // 공격력
    public float defense;        // 방어력
    public float moveSpeed;      // 이동속도
    public float attackSpeed;    // 공격속도
    public float healthRegen;    // 초당 체력 회복량
}

[Serializable]
public struct AttackPattern
{
    public string name;          // 패턴 이름
    public float damage;         // 공격력
    public float range;          // 범위
    public float duration;       // 지속시간
    public GameObject effectPrefab;  // 이펙트 프리팹

    public AttackPattern(string name, float damage, float range, float duration, GameObject effectPrefab)
    {
        this.name = name;
        this.damage = damage;
        this.range = range;
        this.duration = duration;
        this.effectPrefab = effectPrefab;
    }
}