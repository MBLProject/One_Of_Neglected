using UnityEngine;
using UnityEngine.UI;

public class DashCountUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image dashRechargeImage1;
    [SerializeField] private Image dashRechargeImage2;
    [SerializeField] private Image dashRechargeImage3;

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        
        if (player.CurrentDashCount < player.MaxDashCount)
        {
            float fillAmount = player.DashRechargeTimer / player.DashRechargeTime;
            
            switch (player.CurrentDashCount)
            {
                case 0:
                    dashRechargeImage1.fillAmount = fillAmount;
                    dashRechargeImage2.fillAmount = 0;
                    dashRechargeImage3.fillAmount = 0;
                    break;
                case 1:
                    dashRechargeImage1.fillAmount = 1;
                    dashRechargeImage2.fillAmount = fillAmount;
                    dashRechargeImage3.fillAmount = 0;
                    break;
                case 2:
                    dashRechargeImage1.fillAmount = 1;
                    dashRechargeImage2.fillAmount = 1;
                    dashRechargeImage3.fillAmount = fillAmount;
                    break;
            }
        }
        else
        {
            dashRechargeImage1.fillAmount = 1;
            dashRechargeImage2.fillAmount = 1;
            dashRechargeImage3.fillAmount = 1;
        }
    }
} 