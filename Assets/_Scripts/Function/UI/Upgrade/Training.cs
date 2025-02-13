using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{
    public void MaxHp_Modify(bool On, int requireGold, int goldIncDec_value, float value = 10)
    {
        if (DataManager.Instance.player_Property.gold - requireGold < 0) return;
        if (On) { }
        else { }

        DataManager.Instance.player_Property.gold += goldIncDec_value;

    }
    public void HpRegen_Modify(bool On, int requireGold, float value = 0.5f)
    {
        if (DataManager.Instance.player_Property.gold - requireGold < 0) return;
        if (On) { }
        else { }
    }
    public void Defense_Modify(bool On, int requireGold, float value = 1f)
    {
        if (DataManager.Instance.player_Property.gold - requireGold < 0) return;
        if (On) { }
        else { }
    }

}
