using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PlayerTest : MonoBehaviour
{
    [SerializeField] private Player player;
    public Button levelupTest;
    public Button damageTest;
    public Button autoTest;

    public Slider expSlider;


    private void Start()
    {
        player = UnitManager.Instance.GetPlayer();
        levelupTest.onClick.AddListener(OnLevelupTest);
        damageTest.onClick.AddListener(OnDamageTest);
        autoTest.onClick.AddListener(OnAutoTest);
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
