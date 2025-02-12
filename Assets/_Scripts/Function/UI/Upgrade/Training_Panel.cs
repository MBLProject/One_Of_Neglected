using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
delegate int Method(int num);
public class Training_Panel : Panel, IPointerExitHandler
{
    public Image info_IMG;
    public TextMeshProUGUI info_Text;
    public RectTransform trainingInfo;
    public Queue<TrainingCell> trainingCells = new();
    int b;
    int A(int num)
    {
        int b = 10;
        return b;
    }

    private void Start()
    {
        Method c = new Method(A);
        c += A;

        for (int i = 0; i < 19; i++)
        {

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        trainingInfo.gameObject.SetActive(false);
    }

}
