using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PlayerTest : MonoBehaviour
{
    public Player player;
    public Button levelupTest;
    public Button damageTest;
    public Button autoTest;

    public Slider expSlider;

    [Header("Stats Text")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI aspdText;
    public TextMeshProUGUI criticalText;
    public TextMeshProUGUI catkText;
    public TextMeshProUGUI mspdText;
    public TextMeshProUGUI dashCountText;

    private void Start()
    {
        levelupTest.onClick.AddListener(OnLevelupTest);
        damageTest.onClick.AddListener(OnDamageTest);
        autoTest.onClick.AddListener(OnAutoTest);
    }


    private void Update()
    {
        UpdateStatsUI();
        expSlider.value = (float)player.Stats.currentExp / player.Stats.CurrentMaxExp;
    }

    private void UpdateStatsUI()
    {
        if (player == null || player.Stats == null) return;

        if (levelText != null)
            levelText.text = $"Level: {player.Stats.CurrentLevel}";
        
        if (hpText != null)
            hpText.text = $"HP: {player.Stats.currentHp:F0}/{player.Stats.CurrentMaxHp}";
        
        if (expText != null)
            expText.text = $"EXP: {player.Stats.currentExp:F0}/{player.Stats.CurrentMaxExp}";
        
        if (atkText != null)
            atkText.text = $"ATK: {player.Stats.CurrentATK:F1}";
        
        if (aspdText != null)
            aspdText.text = $"ASPD: {player.Stats.CurrentAspd:F2}";
        
        if (criticalText != null)
            criticalText.text = $"CRIT: {player.Stats.CurrentCritical:F1}%";
        
        if (catkText != null)
            catkText.text = $"CATK: {player.Stats.CurrentATK * 100:F0}%";
        
        if (mspdText != null)
            mspdText.text = $"MSPD: {player.Stats.CurrentMspd:F1}";
        
        if (dashCountText != null)
            dashCountText.text = $"DASH: {player.CurrentDashCount}/{player.MaxDashCount}";
    }

    private void OnDamageTest()
    {
        player.TakeDamage(1);
    }

    private void OnLevelupTest()
    {
        if (player.Stats != null)
        {
            player.Stats.currentExp += player.Stats.CurrentMaxExp;
        }
    }

    private void OnAutoTest()
    {
        if(player.isAuto == false)
        {
            player.isAuto = true;
            autoTest.image.color = Color.blue;
        }
        else
        {
            player.isAuto = false;
            autoTest.image.color = Color.white;
        }
    }
}
