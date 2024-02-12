using UnityEngine;

public class ManagerMain : MonoBehaviour
{
    public MapMain mapMain;
    public EntitiesManager entitiesManager;
    public TurnManager turnManager;

    private void Awake()
    {
        BroadcastMessage("InitManager", this);
    }
}
