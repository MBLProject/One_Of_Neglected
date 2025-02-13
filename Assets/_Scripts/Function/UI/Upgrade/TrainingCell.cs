using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        }

        for (int i = trainingCount; i > 0; i--)
        {
            method_Action?.Invoke(false, requireGold_List[i - 1], i);
        }
        trainingCount = 0;
        currentRequireGold = requireGold_List[trainingCount];
    }

    private void Training()
    {
        if (DataManager.Instance.player_Property.gold - currentRequireGold < 0) return;
        if (maxTrainingLevel > trainingCount || maxTrainingLevel == -2)
        {
            if (isDisplayLv == false)
                tinyCells[trainingCount].sprite = training_Panel.tinyCellOn_Sprite;

            trainingCount++;
            method_Action?.Invoke(true, -requireGold_List[trainingCount - 1], trainingCount);

            if (trainingCount != maxTrainingLevel)
                currentRequireGold = requireGold_List[trainingCount];

            if (isDisplayLv) lvText.text = "Lv." + trainingCount.ToString();

        }
        else
        {
            Debug.Log("최대 레벨 도달");
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

            if (isDisplayLv == false && trainingCount != 0)
                tinyCells[trainingCount - 1].sprite = training_Panel.tinyCellOff_Sprite;

            if (trainingCount == 0) return;

            trainingCount--;
            method_Action?.Invoke(false, requireGold_List[trainingCount], trainingCount);

            currentRequireGold = requireGold_List[trainingCount];

            if (isDisplayLv)
                lvText.text = "Lv." + trainingCount.ToString();

        }

    }
}
