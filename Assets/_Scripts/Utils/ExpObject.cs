using UnityEngine;
using static Enums;
public class ExpObject : MonoBehaviour
{
    public ExpType expType;
    private float expAmount; 
    public float ExpAmount => expAmount;

    public void selfDestroy()
    {
        UnitManager.Instance.RemoveExp(this);
        Destroy(gameObject);
    }
}
