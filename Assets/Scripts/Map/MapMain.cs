using System.Collections.Generic;
using UnityEngine;

public class MapMain : MonoBehaviour
{
    public MapConfig config;
    public InitMap init;
    public AStar aStar;

    private void Awake()
    {
        InitGameObject();
    }

    private void Start()
    {
        Debug.Log(UseAStar());
    }

    private void InitGameObject()
    {
        SendMessage("Init", this);
        config.CreateWaypoint();
    }

    public List<WayPoint> UseAStar()
    {
        WayPoint start = config.allWayPoints[Random.Range(0, config.allWayPoints.Count)];
        Debug.Log(start.name);
        WayPoint end = config.allWayPoints[Random.Range(0, config.allWayPoints.Count)];
        Debug.Log(end.name);
        return aStar.GiveThePath(start, end);
    }
}
