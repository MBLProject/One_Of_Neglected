using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrainingCell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    Training_Panel training_Panel;
    public Image training_IMG;
    [TextArea]
    public string training_Text;
    public Button m_BTN;
    private Outline outline;
    public int trainingCount = 0;
    private void Awake()
    {
        training_Panel = GetComponentInParent<Training_Panel>();
        m_BTN = GetComponent<Button>();
        outline = GetComponent<Outline>();
        m_BTN.onClick.AddListener(OutlineActivate);
        m_BTN.onClick.AddListener(Training);
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
    private void OutlineActivate()
    {
        if (outline.enabled) return;
        outline.enabled = true;
        if (training_Panel.trainingCells.Count > 0)
        {
            training_Panel.trainingCells.Dequeue().outline.enabled = false;
        }
        training_Panel.trainingCells.Enqueue(this);
    }
    private void Training()
    {
        trainingCount++;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {

        }
    }
}
