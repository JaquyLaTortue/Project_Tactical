using System.Collections.Generic;
using UnityEngine;

public class MapMain : MonoBehaviour
{
    public MapConfig config;
    public InitMap init;
    public AStar aStar;

    public WayPoint wayPointStart;

    public ClickedCase _clickedCase;

    private void Awake()
    {
        InitGameObject();
    }

    private void Start()
    {
        wayPointStart = config.allWayPoints[Random.Range(0, config.allWayPoints.Count)];
        _clickedCase = GetComponent<ClickedCase>();
        _clickedCase.Init(this);
    }

    private void InitGameObject()
    {
        SendMessage("Init", this);
        config.CreateWaypoint();
    }

    /// <summary>
    /// Cette fonction ne sert que pour les tests de début de projet, à utiliser que pour le astar
    /// </summary>
    /// <param name="end"></param>
    public void Test(WayPoint end)
    {
        if (wayPointStart == end)
        {
            Debug.Log("Les deux Waypoints sont identiques, donc on utilise pas le astar");
        }
        else
        {
            Debug.Log("Doit commencer par " + wayPointStart.name + " et finir par " + end.name);
            UseAStar(wayPointStart, end);
        }
    }

    public List<WayPoint> UseAStar(WayPoint start, WayPoint end)
    {
        /*WayPoint start = config.allWayPoints[Random.Range(0, config.allWayPoints.Count)];
        Debug.Log(start.name);
        WayPoint end = config.allWayPoints[Random.Range(0, config.allWayPoints.Count)];
        Debug.Log(end.name);*/
        List<WayPoint> aStartWaypoint = new List<WayPoint>();
        aStartWaypoint = aStar.GiveThePath(start, end);
        _clickedCase.ChangeColorOfWaypointToOld(wayPointStart.GetComponent<MeshRenderer>());
        wayPointStart = aStartWaypoint[aStartWaypoint.Count - 1];
        foreach (WayPoint wayPoint in aStartWaypoint)
        {
            _clickedCase.ChangeColorOfWaypointToRed(wayPoint.GetComponent<MeshRenderer>());
        }

        return aStartWaypoint;
    }
}
