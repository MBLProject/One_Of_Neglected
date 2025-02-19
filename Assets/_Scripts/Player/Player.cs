using System;
using System.Collections.Generic;
using UnityEngine;
using static Enums;
using Random = UnityEngine.Random;

[Serializable]
public class StatViewer
{
    [Tooltip("레벨")] public int Level;
    [Tooltip("최대 경험치")] public int MaxExp;
    [Tooltip("경험치")] public float Exp;
    [Tooltip("최대체력")] public int MaxHp;
    [Tooltip("체력")] public float Hp;
    [Tooltip("체력회복량")] public float HpRegen;
    [Tooltip("방어력")] public int Defense;
    [Tooltip("이동속도")] public float Mspd;
    [Tooltip("공격력")] public float ATK;
    [Tooltip("공격속도")] public float Aspd;
    [Tooltip("치명타 확률")] public float CriRate;
    [Tooltip("치명타 데미지")] public float CriDamage;
    [Tooltip("투사체 개수")] public int ProjAmount;
    [Tooltip("공격범위")] public float ATKRange;
    [Tooltip("지속시간")] public float Duration;
    [Tooltip("쿨타임")] public float Cooldown;
    [Tooltip("부활 횟수")] public int Revival;
    [Tooltip("재화 습득범위")] public float Magnet;
    [Tooltip("성장")] public float Growth;
    [Tooltip("탐욕")] public float Greed;
    [Tooltip("저주")] public float Curse;
    [Tooltip("새로고침 횟수")] public int Reroll;
    [Tooltip("스탯 지우기")] public int Banish;
    [Tooltip("신 처치")] public bool GodKill;
    [Tooltip("방어막")] public bool Barrier;
    [Tooltip("방어막 쿨타임")] public float BarrierCooldown;
    [Tooltip("피격시 무적")] public bool Invincibility;
    [Tooltip("대시 횟수")] public int DashCount;
    [Tooltip("대적자")] public bool Adversary;
    [Tooltip("투사체 파괴")] public bool ProjDestroy;
    [Tooltip("투사체 반사")] public bool ProjParry;
}

public abstract class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dashEffect;
    [SerializeField] public StatViewer statViewer;
    [SerializeField] public SpriteRenderer modelRenderer;
    [SerializeField] private GameObject barrierEffect;
    [SerializeField] private CircleCollider2D magnetCollider;

    // 자동사냥 모드!
    public bool isAuto = false;

    protected StateHandler<Player> stateHandler;
    protected bool isSkillInProgress = false;
    protected bool isDashing = false;
    protected bool isBarrier = false;

    protected PlayerStats stats;

    public Vector2 targetPosition;
    private Vector2 savedTargetPosition;

    protected float moveThreshold = 0.1f;

    #region DashSettings
    protected float dashRechargeTime = 5f;
    protected float dashRechargeTimer = 0f;
    protected int currentDashCount;

    public float DashRechargeTimer => dashRechargeTimer;
    public float DashRechargeTime => dashRechargeTime;
    public int CurrentDashCount => currentDashCount;
    public int MaxDashCount => stats.CurrentDashCount;
    #endregion

    #region BarrierSettings
    protected float barrierRechargeTimer = 0f;
    protected bool hasBarrierCharge = false;

    public float BarrierRechargeTimer => barrierRechargeTimer;
    public float BarrierRechargeTime => stats.CurrentBarrierCooldown;
    public bool HasBarrierCharge => hasBarrierCharge;
    #endregion

    #region InvincibilitySettings
    private bool isInvincible = false;
    private float invincibilityDuration = 0.1f;
    private float invincibilityTimer = 0f;
    #endregion

    public ClassType ClassType { get; protected set; }
    public Animator Animator => animator;
    public PlayerStats Stats
    {
        get { return stats; }
        protected set { stats = value; }
    }
    public ParticleSystem DashEffect => dashEffect;

    protected AugmentSelector augmentSelector;
    public event Action OnPlayerAttack; 


    protected virtual void Awake()
    {
        InitializeStateHandler();
        InitializeStats();
        InitializeClassType();
        InitializeStatViewer();

        SubscribeToStatEvents();

        currentDashCount = stats.CurrentDashCount;

        // 베리어 초기화
        isBarrier = stats.CurrentBarrier;
        hasBarrierCharge = isBarrier;
        if (barrierEffect != null)
        {
            barrierEffect.SetActive(hasBarrierCharge);
        }

        if (dashEffect != null)
        {
            dashEffect.Stop();
        }
        if (magnetCollider != null)
        {
            magnetCollider.radius = stats.CurrentMagnet;
            magnetCollider.isTrigger = true;
        }

        augmentSelector = gameObject.AddComponent<AugmentSelector>();
        augmentSelector.Initialize(this);
    }

    private void Update()
    {
        stateHandler.Update();
        UpdateDashRecharge();
        UpdateBarrierRecharge();
        UpdateInvincibility();
        UpdateHpRegen();
    }

    protected abstract void InitializeStats();
    protected abstract void InitializeStateHandler();
    protected abstract void InitializeClassType();
    protected abstract void InitializeStatViewer();

    private void SubscribeToStatEvents()
    {
        // 모든 스탯 변경 이벤트 구독
        stats.OnLevelUp += (value) => statViewer.Level = value;
        stats.OnMaxExpChanged += (value) => statViewer.MaxExp = value;
        stats.OnExpChanged += (value) => statViewer.Exp = value;
        stats.OnMaxHpChanged += (value) => statViewer.MaxHp = value;
        stats.OnHpChanged += (value) => statViewer.Hp = value;
        stats.OnHpRegenChanged += (value) => statViewer.HpRegen = value;
        stats.OnDefenseChanged += (value) => statViewer.Defense = value;
        stats.OnMspdChanged += (value) => statViewer.Mspd = value;
        stats.OnATKChanged += (value) => statViewer.ATK = value;
        stats.OnAspdChanged += (value) => statViewer.Aspd = value;
        stats.OnCriRateChanged += (value) => statViewer.CriRate = value;
        stats.OnCriDamageChanged += (value) => statViewer.CriDamage = value;
        stats.OnProjAmountChanged += (value) => statViewer.ProjAmount = value;
        stats.OnATKRangeChanged += (value) => statViewer.ATKRange = value;
        stats.OnDurationChanged += (value) => statViewer.Duration = value;
        stats.OnCooldownChanged += (value) => statViewer.Cooldown = value;
        stats.OnRevivalChanged += (value) => statViewer.Revival = value;
        stats.OnMagnetChanged += (value) =>
        {
            statViewer.Magnet = value;
            if (magnetCollider != null)
            {
                magnetCollider.radius = value;
            }
        };
        stats.OnGrowthChanged += (value) => statViewer.Growth = value;
        stats.OnGreedChanged += (value) => statViewer.Greed = value;
        stats.OnCurseChanged += (value) => statViewer.Curse = value;
        stats.OnRerollChanged += (value) => statViewer.Reroll = value;
        stats.OnBanishChanged += (value) => statViewer.Banish = value;
        stats.OnGodKillChanged += (value) => statViewer.GodKill = value;
        stats.OnBarrierChanged += (value) => statViewer.Barrier = value;
        stats.OnBarrierCooldownChanged += (value) => statViewer.BarrierCooldown = value;
        stats.OnInvincibilityChanged += (value) => statViewer.Invincibility = value;
        stats.OnDashCountChanged += (value) => statViewer.DashCount = value;
        stats.OnAdversaryChanged += (value) => statViewer.Adversary = value;
        stats.OnProjDestroyChanged += (value) => statViewer.ProjDestroy = value;
        stats.OnProjParryChanged += (value) => statViewer.ProjParry = value;
    }

    #region Dash
    private void UpdateDashRecharge()
    {
        if (currentDashCount < stats.CurrentDashCount)
        {
            dashRechargeTimer += Time.deltaTime;
            if (dashRechargeTimer >= dashRechargeTime * stats.CurrentCooldown)
            {
                dashRechargeTimer = 0f;
                currentDashCount++;
            }
        }
    }
    public bool CanDash()
    {
        return currentDashCount > 0;
    }
    public void ConsumeDash()
    {
        if (currentDashCount > 0)
        {
            currentDashCount--;
        }
    }
    public void SetDashing(bool dashing)
    {
        isDashing = dashing;
    }
    #endregion

    #region move
    public void MoveTo(Vector2 destination)
    {
        if (isDashing) return;

        if (Vector2.Distance(transform.position, destination) > moveThreshold)
        {
            Vector2 direction = (destination - (Vector2)transform.position).normalized;
            Vector2 newPosition = Vector2.MoveTowards(transform.position
                , destination, stats.CurrentMspd * Time.deltaTime);

            transform.SetPositionAndRotation(new Vector3(newPosition.x, newPosition.y, transform.position.z), transform.rotation);

            if (direction.x != 0)
            {
                FlipModel(direction.x < 0);
            }
        }
    }
    public bool IsAtDestination()
    {
        return Vector2.Distance(transform.position, targetPosition) < moveThreshold;
    }
    public void SetCurrentPositionAsTarget()
    {
        targetPosition = transform.position;
        savedTargetPosition = targetPosition;
    }
    #endregion

    #region Utils
    public void SetSkillInProgress(bool inProgress, bool savePrevPosition = true)
    {
        isSkillInProgress = inProgress;
        if (inProgress && savePrevPosition)
        {
            savedTargetPosition = targetPosition;
        }
    }
    public void LookAtTarget(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        FlipModel(direction.x < 0);
    }
    public void FlipModel(bool isLeft)
    {
        if (modelRenderer != null)
        {
            modelRenderer.flipX = isLeft;
        }

        if (dashEffect != null)
        {
            Vector3 localScale = dashEffect.transform.localScale;
            localScale.x = isLeft ? -Mathf.Abs(localScale.x) : Mathf.Abs(localScale.x);
            dashEffect.transform.localScale = localScale;
        }
    }
    public virtual void TakeDamage(float damage)
    {
        // 순서 어떻게 할지에 따라 무적상태에서 피격시 베리어 깨지게 할 수 있읍니다.
        // 무적 체크
        if (isInvincible) return;

        // 베리어 체크
        if (isBarrier && hasBarrierCharge)
        {
            hasBarrierCharge = false;
            if (barrierEffect != null)
            {
                barrierEffect.SetActive(false);
            }
            return;
        }

        bool isCritical = false;
        if (Random.Range(0f, 100f) <= stats.CurrentCriRate)
        {
            damage = Mathf.RoundToInt(damage * stats.CurrentCriDamage);
            isCritical = true;
        }

        stats.currentHp -= damage;
        ShowDamageFont(transform.position, damage, transform, isCritical);

        // Invincibility가 true인 경우 데미지를 받은 후 무적 상태 적용
        if (stats.CurrentInvincibility)
        {
            isInvincible = true;
            invincibilityTimer = 0f;
        }

        if (stats.currentHp <= 0)
        {
            //ToDO 플레이어 사망처리
            Debug.Log("플레이어 주금");
        }
    }
    public void ShowDamageFont(Vector2 pos, float damage, Transform parent, bool isCritical = false)
    {
        GameObject go = Resources.Load<GameObject>("DamageText");
        if (go != null)
        {
            Vector2 spawnPosition = (Vector2)transform.position + Vector2.up * 0.2f;

            GameObject instance = Instantiate(go, spawnPosition, Quaternion.identity);
            ShowDamage damageText = instance.GetComponent<ShowDamage>();
            if (damageText != null)
            {
                damageText.SetInfo(spawnPosition, damage, parent, isCritical);
            }
        }
    }
    #endregion

    #region stat
    /// <summary>
    /// 기존 스탯에 value만큼 더함
    /// </summary>
    /// <param name="statType">스탯 타입</param>
    /// <param name="value">더해지는 값</param>
    public void ModifyStat(StatType statType, float value)
    {
        stats.ModifyStatValue(statType, value);
        UpdateStatViewer();
    }
    /// <summary>
    /// 기존 스탯을 value값으로 바꿈
    /// </summary>
    /// <param name="statType">스탯 타입</param>
    /// <param name="value">바꾸는 값</param>
    public void SetStat(StatType statType, float value)
    {
        stats.SetStatValue(statType, value);
        UpdateStatViewer();
    }
    private void UpdateStatViewer()
    {
        statViewer.Level = stats.CurrentLevel;
        statViewer.MaxHp = stats.CurrentMaxHp;
        statViewer.Hp = stats.currentHp;
        statViewer.MaxExp = stats.CurrentMaxExp;
        statViewer.Exp = stats.currentExp;
        statViewer.MaxHp = stats.CurrentMaxHp;
        statViewer.HpRegen = stats.CurrentHpRegen;
        statViewer.Defense = stats.CurrentDefense;
        statViewer.Mspd = stats.CurrentMspd;
        statViewer.ATK = stats.CurrentATK;
        statViewer.Aspd = stats.CurrentAspd;
        statViewer.CriRate = stats.CurrentCriRate;
        statViewer.CriDamage = stats.CurrentCriDamage;
        statViewer.ProjAmount = stats.CurrentProjAmount;
        statViewer.ATKRange = stats.CurrentATKRange;
        statViewer.Duration = stats.CurrentDuration;
        statViewer.Cooldown = stats.CurrentCooldown;
        statViewer.Revival = stats.CurrentRevival;
        statViewer.Magnet = stats.CurrentMagnet;
        statViewer.Growth = stats.CurrentGrowth;
        statViewer.Greed = stats.CurrentGreed;
        statViewer.Curse = stats.CurrentCurse;
        statViewer.Reroll = stats.CurrentReroll;
        statViewer.Banish = stats.CurrentBanish;
        statViewer.GodKill = stats.CurrentGodKill;
        statViewer.Barrier = stats.CurrentBarrier;
        statViewer.BarrierCooldown = stats.CurrentBarrierCooldown;
        statViewer.Invincibility = stats.CurrentInvincibility;
        statViewer.DashCount = stats.CurrentDashCount;
        statViewer.Adversary = stats.CurrentAdversary;
        statViewer.ProjDestroy = stats.CurrentProjDestroy;
        statViewer.ProjParry = stats.CurrentProjParry;
    }
    protected void UpdateStats()
    {
        if (stats != null)
        {
            stats.CurrentMaxExp = statViewer.MaxExp;
            stats.currentExp = statViewer.Exp;
            stats.CurrentMaxHp = statViewer.MaxHp;
            stats.currentHp = statViewer.Hp;
            stats.CurrentHpRegen = statViewer.HpRegen;
            stats.CurrentDefense = statViewer.Defense;
            stats.CurrentMspd = statViewer.Mspd;
            stats.CurrentATK = statViewer.ATK;
            stats.CurrentAspd = statViewer.Aspd;
            stats.CurrentCriRate = statViewer.CriRate;
            stats.CurrentCriDamage = statViewer.CriDamage;
            stats.CurrentProjAmount = statViewer.ProjAmount;
            stats.CurrentATKRange = statViewer.ATKRange;
            stats.CurrentDuration = statViewer.Duration;
            stats.CurrentCooldown = statViewer.Cooldown;
            stats.CurrentRevival = statViewer.Revival;
            stats.CurrentMagnet = statViewer.Magnet;
            stats.CurrentGrowth = statViewer.Growth;
            stats.CurrentGreed = statViewer.Greed;
            stats.CurrentCurse = statViewer.Curse;
            stats.CurrentReroll = statViewer.Reroll;
            stats.CurrentBanish = statViewer.Banish;
            stats.CurrentGodKill = statViewer.GodKill;
            stats.CurrentBarrier = statViewer.Barrier;
            stats.CurrentBarrierCooldown = statViewer.BarrierCooldown;
            stats.CurrentInvincibility = statViewer.Invincibility;
            stats.CurrentDashCount = statViewer.DashCount;
            stats.CurrentAdversary = statViewer.Adversary;
            stats.CurrentProjDestroy = statViewer.ProjDestroy;
            stats.CurrentProjParry = statViewer.ProjParry;
        }
    }
    #endregion

    private void UpdateBarrierRecharge()
    {
        if (!isBarrier) return;

        if (!hasBarrierCharge)
        {
            barrierRechargeTimer += Time.deltaTime;
            if (barrierRechargeTimer >= stats.CurrentBarrierCooldown * stats.CurrentCooldown)
            {
                barrierRechargeTimer = 0f;
                hasBarrierCharge = true;
                if (barrierEffect != null)
                {
                    barrierEffect.SetActive(true);
                }
            }
        }
    }

    private void UpdateInvincibility()
    {
        if (isInvincible)
        {
            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= invincibilityDuration * stats.CurrentDuration)
            {
                isInvincible = false;
                invincibilityTimer = 0f;
            }
        }
    }

    private float regenTimer = 0f;
    private void UpdateHpRegen()
    {
        regenTimer += Time.deltaTime;
        if (regenTimer >= 1)
        {
            regenTimer = 0f;
            stats.currentHp += stats.CurrentHpRegen;

            // 여기에 HP회복 이펙트 등 추가 가능
        }
    }

    private void CollectExp(ExpObject expObject)
    {
        float expAmount = 0;
        switch (expObject.expType)
        {
            case ExpType.White:
                expAmount = 10;
                break;
            case ExpType.Green:
                expAmount = 20;
                break;
            case ExpType.Blue:
                expAmount = 30;
                break;
            case ExpType.Red:
                expAmount = 40;
                break;
            case ExpType.Purple:
                //특정 레벨 이상 처리
                if (stats.CurrentLevel <= 10)
                {
                    stats.currentExp = stats.CurrentMaxExp;
                    expObject.selfDestroy();
                    return;
                }
                else
                {
                    expAmount = stats.currentExp * 0.3f;
                }
                break;
        }

        expAmount *= stats.CurrentGreed;
        stats.currentExp += expAmount;

        while (stats.currentExp >= stats.CurrentMaxExp)
        {
            LevelUp();
        }

        expObject.selfDestroy();
    }

    public void LevelUp()
    {
        stats.currentExp -= stats.CurrentMaxExp;
        stats.CurrentLevel += 1;
        stats.CurrentMaxExp = CalculateNextLevelExp();

        //UI_Manager.Instance.panel_Dic["LevelUp_Panel"].PanelOpen();
    }

    private int CalculateNextLevelExp()
    {
        return (int)(100 * (1 + (stats.CurrentLevel - 1) * 0.2f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exp"))
        {
            ExpObject expObject = collision.gameObject.GetComponent<ExpObject>();
            if (expObject != null)
            {
                CollectExp(expObject);
            }
            return;
        }
    }

    private void OnValidate()
    {
        if (stats != null)
        {
            UpdateStats();
        }
    }

    protected virtual void OnAttack()
    {
        OnPlayerAttack?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttack();
    }
}
