using UnityEngine;
using static Enums;
public class WorldObject : MonoBehaviour
{
    public WorldObjectType objectType;

    public void selfDestroy()
    {
        //UnitManager.Instance.RemoveExp(this);
        Destroy(gameObject);
    }
}
