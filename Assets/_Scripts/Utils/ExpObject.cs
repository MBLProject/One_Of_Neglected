using UnityEngine;
using static Enums;
public class ExpObject : MonoBehaviour
{
    public ExpType expType;
    private float expAmount; //여기서 경험치량을 정할지, Player에서 exptype에 맞춰 정할지 ㄱㄱ
    public float ExpAmount => expAmount;

    public void selfDestroy()
    {
        Destroy(gameObject);
    }
}
