using UnityEngine;

public class ManagerMain : MonoBehaviour
{
    public MapMain mapMain;
    public EntitiesManager entitiesManager;

    private void Awake()
    {
        BroadcastMessage("InitManager", this);
    }
}
