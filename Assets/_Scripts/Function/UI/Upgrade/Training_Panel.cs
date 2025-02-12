using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Training_Panel : Panel, IPointerExitHandler
{
    public Image info_IMG;
    public TextMeshProUGUI info_Text;
    public RectTransform trainingInfo;
    public Queue<TrainingCell> trainingCells = new();
    public Sprite tinyCellOn_Sprite;
    public Sprite tinyCellOff_Sprite;
    public CellReset cellReset;
    private void Start()
    {

        for (int i = 0; i < 19; i++)
        {

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        trainingInfo.gameObject.SetActive(false);
    }

}
