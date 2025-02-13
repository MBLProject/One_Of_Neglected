using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Training_Category
{
    MaxHp,
    HpRegen,
    Defense,
    Mspd,
    ATK,
    Aspd,
    CriRate,
    CriDamage,
    ProjAmount,
    ATKRange,
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

public class Training_Panel : Panel, IPointerExitHandler
{
    public Image info_IMG;
    public TextMeshProUGUI info_Text;
    public RectTransform trainingInfo;
    public Queue<TrainingCell> ClickedCell_Queue = new();
    public Sprite tinyCellOn_Sprite;
    public Sprite tinyCellOff_Sprite;
    public CellReset cellReset;
    public List<TrainingCell> trainingCells_List;
    Training training;
    public Dictionary<TrainingCell, Training_Category> keyValuePairs = new();
    public Dictionary<Training_Category, Action> keyValuePairs2 = new();
    private void Start()
    {

        Cell_Initialize(ref trainingCells_List);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        trainingInfo.gameObject.SetActive(false);
    }

    private void Cell_Initialize(ref List<TrainingCell> cells)
    {
        foreach (TrainingCell cell in cells)
        {
            switch (cell.training_Category)
            {
                case Training_Category.MaxHp:
                    // Handle MaxHp case

                    break;
                case Training_Category.HpRegen:
                    // Handle HpRegen case
                    break;
                case Training_Category.Defense:
                    // Handle Defense case
                    break;
                case Training_Category.Mspd:
                    // Handle Mspd case
                    break;
                case Training_Category.ATK:
                    // Handle ATK case
                    break;
                case Training_Category.Aspd:
                    // Handle Aspd case
                    break;
                case Training_Category.CriRate:
                    // Handle CriRate case
                    break;
                case Training_Category.CriDamage:
                    // Handle CriDamage case
                    break;
                case Training_Category.ProjAmount:
                    // Handle ProjAmount case
                    break;
                case Training_Category.ATKRange:
                    // Handle ATKRange case
                    break;
                case Training_Category.Duration:
                    // Handle Duration case
                    break;
                case Training_Category.Cooldown:
                    // Handle Cooldown case
                    break;
                case Training_Category.Revival:
                    // Handle Revival case
                    break;
                case Training_Category.Magnet:
                    // Handle Magnet case
                    break;
                case Training_Category.Growth:
                    // Handle Growth case
                    break;
                case Training_Category.Greed:
                    // Handle Greed case
                    break;
                case Training_Category.Curse:
                    // Handle Curse case
                    break;
                case Training_Category.Reroll:
                    // Handle Reroll case
                    break;
                case Training_Category.Banish:
                    // Handle Banish case
                    break;
                default:
                    // Handle default case
                    break;
            }
        }
    }

}
