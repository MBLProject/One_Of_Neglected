using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvas : MonoBehaviour
{
    public CanvasScaler m_CanvasScaler;
    public TextMeshProUGUI inGameTextTMP;
    public TextMeshProUGUI inGameTipTMP;

    private List<string> inGameText = new List<string>();
    private List<string> inGameTip = new List<string>();
    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            switch (i)
            {
                case 0:
                    inGameText.Add("왕국에서 용사가 소환되기까지 버티세요.");
                    inGameTip.Add("대적자는 화신의 공격을 약화시킵니다.");
                    break;
                case 1:
                    inGameText.Add("마왕군의 공세에 왕국은 몰락하기 시작했습니다.");
                    inGameTip.Add("신살은 화신의 상태를 약화시킵니다.");
                    break;
                case 2:
                    inGameText.Add("어린이들은 " + '"' + "용사가 나타나 마왕을 물리쳤어요." + '"' + " \n라는 내용의 동화를 좋아한다.");
                    break;
                case 3:
                    inGameText.Add("주신의 가호아래 왕국을 수호하세요");
                    inGameTip.Add("속도는 생각보다 중요한 역할입니다.");
                    break;
                case 4:
                    inGameText.Add("용사가 있어야 이야기가 시작된다면, \n직접 용사가 되는건 어떨까?");
                    inGameTip.Add("쿨타임 감소는 DPS에 가장 중요한 역할을 미친다는 사실");
                    break;
                case 5:
                    inGameText.Add("국민은 언제 소환될지 모를 용사보다 \n눈 앞의 병사를 믿고 있습니다.");
                    break;
            }
        }
    }
    private void Start()
    {
        int idx = UnityEngine.Random.Range(0, 6);
        inGameTextTMP.text = inGameText[idx];

        idx = UnityEngine.Random.Range(0, 4);
        inGameTipTMP.text = inGameTip[idx];

    }
}
