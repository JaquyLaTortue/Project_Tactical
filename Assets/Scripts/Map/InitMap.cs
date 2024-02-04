using System.Collections.Generic;
using UnityEngine;

public class InitMap : MonoBehaviour
{
    public List<WayPoint> InitTheMap(int length, int width)
    {
        List<WayPoint> map = new List<WayPoint>();
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                WayPoint newWaypoint = new WayPoint();
                newWaypoint.casePosition = new int[2] { i, j };
                map.Add(newWaypoint);
                foreach (WayPoint wayPoint in map)
                {
                    FindNeighbour(newWaypoint, wayPoint);
                }
            }
        }

        return map;
    }

    public void Init(MapMain main)
    {
        main.init = this;
    }

    private void FindNeighbour(WayPoint newWaypoint, WayPoint potentialNeighbour)
    {
        if (((potentialNeighbour.casePosition[0] == newWaypoint.casePosition[0] - 1
            ||
            potentialNeighbour.casePosition[0] == newWaypoint.casePosition[0] + 1)
            &&
            potentialNeighbour.casePosition[1] == newWaypoint.casePosition[1])
            ||
            ((potentialNeighbour.casePosition[1] == newWaypoint.casePosition[1] - 1
            ||
            potentialNeighbour.casePosition[1] == newWaypoint.casePosition[1] + 1)
            &&
            potentialNeighbour.casePosition[0] == newWaypoint.casePosition[0]))
        {
            newWaypoint.neighbour.Add(potentialNeighbour);
            potentialNeighbour.neighbour.Add(newWaypoint);
        }
    }
}
