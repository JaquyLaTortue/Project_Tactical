using System.Collections.Generic;
using UnityEngine;

public class MapConfig : MonoBehaviour
{
    [SerializeField]
    private int _length;

    [SerializeField]
    private int _width;

    public List<WayPoint> allWayPoints = new List<WayPoint>();

    private MapMain _main;

    public void CreateWaypoint()
    {
        allWayPoints = _main.init.InitTheMap(_length, _width);
        foreach (WayPoint waypoint in allWayPoints)
        {
            Debug.Log("MAin" + waypoint.casePosition[0] + "" + waypoint.casePosition[1]);
            foreach (WayPoint way in waypoint.neighbour)
            {
                Debug.Log(way.casePosition[0] + "" + way.casePosition[1]);
            }
        }
    }

    public void Init(MapMain main)
    {
        main.config = this;
        _main = main;
    }
}
