using System.Collections.Generic;
using UnityEngine;

public class MapMain : MonoBehaviour
{
    public MapConfig config;
    public InitMap init;
    private AStar aStar;

    private void Start()
    {
        InitGameObject();
    }

    private void InitGameObject()
    {
        SendMessage("Init", this);
        config.CreateWaypoint();
    }

    public List<WayPoint> UseAStar()
    {
        if (aStar == null)
        {
            aStar = new AStar();
        }

        WayPoint start = new WayPoint();
        WayPoint end = new WayPoint();
        return aStar.GiveThePath(start, end);
    }
}
