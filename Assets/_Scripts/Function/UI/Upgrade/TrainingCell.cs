using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TrainingCell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private Training_Panel training_Panel;
    private Outline outline;
    [SerializeField]
    private int maxTrainingLevel;
    private Button m_BTN;
    public Image training_IMG;

    public Training_Category training_Category;
    [TextArea]
    public string training_Text;
    public int trainingCount = 0;
    public List<Image> tinyCells;
    public bool isDisplayLv;
    public TextMeshProUGUI lvText;
    public event Action<bool, int, int> method_Action;
    public event Action baseCellAction;
    public List<int> requireGold_List;
    private int currentRequireGold;
    private void Awake()
    {
        training_Panel = GetComponentInParent<Training_Panel>();
        m_BTN = GetComponent<Button>();
        m_BTN.interactable = false;
        outline = GetComponent<Outline>();
        m_BTN.onClick.AddListener(Training);
    }
    private void Start()
    {
        training_Panel.cellReset += CellReset;

    }

    public void CellReset()
    {
        foreach (Image image in tinyCells)
        {
            image.sprite = training_Panel.tinyCellOff_Sprite;
            image.color = Color.white;
        }
        if (isDisplayLv == false)
        {
            for (int i = trainingCount; i > 0; i--)
            {
                method_Action?.Invoke(false, requireGold_List[i - 1], 0);
            }
        }
        else
        {
            for (int i = trainingCount; i > 9; i--)
            {
                method_Action?.Invoke(false, 10000, 0);
            }
            for (int i = 9; i >= 0; i--)
            {
                method_Action?.Invoke(false, requireGold_List[i], 0);
            }
        }
        trainingCount = 0;
        currentRequireGold = requireGold_List[trainingCount];
        if (isDisplayLv) lvText.text = "Lv." + trainingCount.ToString();
        baseCellAction?.Invoke();
    }

    private void Training()
    {
        if (DataManager.Instance.player_Property.gold - currentRequireGold < 0) return;
        if (maxTrainingLevel > trainingCount || isDisplayLv)
        {
            if (isDisplayLv == false && maxTrainingLevel < 10)
                tinyCells[trainingCount].sprite = training_Panel.tinyCellOn_Sprite;
            else if (isDisplayLv == false)
            {
                if (trainingCount < 10)
                    tinyCells[trainingCount].sprite = training_Panel.tinyCellOn_Sprite;
                else
                {
                    tinyCells[trainingCount - 10].color = Color.yellow;
                }
            }
            trainingCount++;
            method_Action?.Invoke(true, -requireGold_List[trainingCount - 1], trainingCount);
            baseCellAction?.Invoke();
            if (isDisplayLv == false)
            {
                if (trainingCount != maxTrainingLevel)
                    currentRequireGold = requireGold_List[trainingCount];
            }
            else
            {
                if (trainingCount < 10)
                {
                    currentRequireGold = requireGold_List[trainingCount];
                }
                else
                {
                    currentRequireGold += 10000;
                }
                lvText.text = "Lv." + trainingCount.ToString();
            }
        }
        else
        {
            Debug.Log("최대 레벨 도달");
            return;
        }
    }
    public void ByTrainingCount()
    {
        if (isDisplayLv)
        {
            lvText.text = "Lv." + trainingCount.ToString();
            currentRequireGold = requireGold_List[requireGold_List.Count - 1];
            for (int i = requireGold_List.Count; i < trainingCount; i++)
            {

                currentRequireGold += 10000;
            }
        }
        else
        {
            for (int i = 0; i < trainingCount; i++)
            {
                tinyCells[i].sprite = training_Panel.tinyCellOn_Sprite;
            }

            if (maxTrainingLevel != trainingCount) currentRequireGold = requireGold_List[trainingCount];
            else currentRequireGold = DataManager.Instance.player_Property.gold + 1;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (outline.enabled) return;

            outline.enabled = true;
            m_BTN.interactable = true;

            if (training_Panel.ClickedCell_Queue.Count > 0)
            {
                TrainingCell queueCell = training_Panel.ClickedCell_Queue.Dequeue();
                queueCell.outline.enabled = false;
                queueCell.m_BTN.interactable = false;
            }

            training_Panel.ClickedCell_Queue.Enqueue(this);

        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (outline.enabled == false) return;

            if (isDisplayLv == false && trainingCount != 0 && maxTrainingLevel <= 10)
                tinyCells[trainingCount - 1].sprite = training_Panel.tinyCellOff_Sprite;
            else if (isDisplayLv == false)
            {
                if (trainingCount > 10)
                {
                    tinyCells[trainingCount - 11].color = Color.white;
                }
                else
                {
                    if (trainingCount != 0)
                        tinyCells[trainingCount - 1].sprite = training_Panel.tinyCellOff_Sprite;
                }
            }

            if (trainingCount == 0) return;

            trainingCount--;
            if (isDisplayLv == false)
            {
                method_Action?.Invoke(false, requireGold_List[trainingCount], trainingCount);
            }
            else
            {
                if (trainingCount > 9)
                {
                    method_Action?.Invoke(false, 10000, trainingCount);
                }
                else
                {
                    method_Action?.Invoke(false, requireGold_List[trainingCount], trainingCount);
                }
            }
            baseCellAction?.Invoke();
            currentRequireGold = requireGold_List[trainingCount];

            if (isDisplayLv)
                lvText.text = "Lv." + trainingCount.ToString();

        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter)
        {
            training_Panel.trainingInfo.gameObject.SetActive(true);
            training_Panel.info_Text.text = training_Text;
            training_Panel.info_IMG.sprite = training_IMG.sprite;
        }
    }

}
