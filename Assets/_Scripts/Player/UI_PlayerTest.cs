using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Enums;

public class UI_PlayerTest : MonoBehaviour
{
    [SerializeField] private Player player;
    public Button levelupTest;
    public Button damageTest;
    public Button autoTest;
    public Button selectAug;
    public Button upgradeAug;
    public Button stoptime;
    public Button gotime;
    public Button revive;

    public Slider expSlider;


    private void Start()
    {
        player = UnitManager.Instance.GetPlayer();
        levelupTest.onClick.AddListener(OnLevelupTest);
        damageTest.onClick.AddListener(OnDamageTest);
        autoTest.onClick.AddListener(OnAutoTest);
        selectAug.onClick.AddListener(OnSelectAug);
        upgradeAug.onClick.AddListener(OnUpgradeAug);
        stoptime.onClick.AddListener(OnStopTime);
        gotime.onClick.AddListener(OnGoTime);
        revive.onClick.AddListener(OnRevive);
    }

    private void OnRevive()
    {
        player.ChangePlayerRevive();
    }

    private void OnGoTime()
    {
        Time.timeScale = 1;
    }

    private void OnStopTime()
    {
        Time.timeScale = 0;
    }

    private void OnUpgradeAug()
    {
        player.GetComponent<AugmentSelector>().LevelUpAugment(AugmentName.SwordShield);
        Debug.Log("증강 강화됨");
    
    
    }
    private void OnSelectAug()
    {
        player.GetComponent<AugmentSelector>().ChooseAugment2(AugmentName.SwordShield);
        Debug.Log("증강 선택됨");
    }
    private void Update()
    {
        if(!player)
        {
            player = UnitManager.Instance.GetPlayer();
        }
        expSlider.value = (float)player.Stats.currentExp / player.Stats.CurrentMaxExp;
    }

    private void OnDamageTest()
    {
        player.TakeDamage(1);
    }

    private void OnLevelupTest()
    {
        if (player.Stats != null)
        {
            player.Stats.ModifyStatValue(StatType.ATK, 10f);
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
