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

    public Slider expSlider;


    private void Start()
    {
        player = UnitManager.Instance.GetPlayer();
        levelupTest.onClick.AddListener(OnLevelupTest);
        damageTest.onClick.AddListener(OnDamageTest);
        autoTest.onClick.AddListener(OnAutoTest);
        selectAug.onClick.AddListener(OnSelectAug);
        upgradeAug.onClick.AddListener(OnUpgradeAug);
    }

    private void OnUpgradeAug()
    {
        player.GetComponent<AugmentSelector>().LevelUpAugment(AugmentName.SwordShield);
    }
    private void OnSelectAug()
    {
        player.GetComponent<AugmentSelector>().ChooseAugment2(AugmentName.SwordShield);
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
            player.LevelUp();
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
