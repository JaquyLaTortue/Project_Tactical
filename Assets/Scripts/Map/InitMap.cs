using System.Collections.Generic;
using UnityEngine;

public class InitMap : MonoBehaviour
{
    [SerializeField]
    private int _length;

    [SerializeField]
    private int _width;

    [SerializeField]
    private GameObject _prefabCase;

    public List<WayPoint> InitTheMap(int lengthCase, int widthCase)
    {
        BoxCollider collision = _prefabCase.GetComponent<BoxCollider>();
        float prefabWidth = collision.size.z;
        float prefabLength = collision.size.x;

        List<WayPoint> map = new List<WayPoint>();
        for (int i = 0; i < lengthCase; i++)
        {
            for (int j = 0; j < widthCase; j++)
            {
                GameObject newGameObject = Instantiate(_prefabCase);
                newGameObject.transform.position += new Vector3((i + 1) * prefabLength, 0, (j + 1) * prefabWidth);
                WayPoint newWaypoint = newGameObject.GetComponent<WayPoint>();
                newWaypoint.casePosition = new int[2] { i, j };
                newWaypoint.name = newWaypoint.casePosition[0] + " " + newWaypoint.casePosition[1];
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
