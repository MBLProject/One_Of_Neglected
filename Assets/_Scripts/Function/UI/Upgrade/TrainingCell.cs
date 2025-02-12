using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrainingCell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private Training_Panel training_Panel;
    private Outline outline;
    [SerializeField]
    private int maxTrainingLevel;

    public Image training_IMG;
    [TextArea]
    public string training_Text;
    public Button m_BTN;
    public int trainingCount = 0;
    public List<Image> tinyCells;
    public bool isDisplayLv;
    public TextMeshProUGUI lvText;
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
        trainingCount = 0;
        Debug.Log("야호");
    }

    private void Training()
    {
        if (maxTrainingLevel > trainingCount || maxTrainingLevel == -2)
        {
            if (isDisplayLv == false) tinyCells[trainingCount].sprite = training_Panel.tinyCellOn_Sprite;

            trainingCount++;

            if (isDisplayLv) lvText.text = "Lv." + trainingCount.ToString();

            //TODO : 재화 소모
            //TODO : 기획에 따라 단련에 필요한 골드 상승

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
            if (training_Panel.trainingCells.Count > 0)
            {
                TrainingCell queueCell = training_Panel.trainingCells.Dequeue();
                queueCell.outline.enabled = false;
                queueCell.m_BTN.interactable = false;
            }
            training_Panel.trainingCells.Enqueue(this);
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {

            if (isDisplayLv == false && trainingCount != 0)
                tinyCells[trainingCount - 1].sprite = training_Panel.tinyCellOff_Sprite;

            if (trainingCount == 0) return;

            trainingCount--;

            if (isDisplayLv)
                lvText.text = "Lv." + trainingCount.ToString();
        }

    }

}
