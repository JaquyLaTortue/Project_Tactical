using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private WayPoint _target;
    private List<WayPoint> _path = new List<WayPoint>();
    private List<WayPoint> _openWaypoints = new List<WayPoint>();
    private List<WayPoint> _closeWaypoints = new List<WayPoint>();
    private MapMain _map;

    private void InitAStar(WayPoint waypointToStart)
    {
        _closeWaypoints.Add(waypointToStart);
        foreach (WayPoint waypoint in waypointToStart.neighbour)
        {
            _openWaypoints.Add(waypoint);
            waypoint.caseCost = 1;
            waypoint.parent = waypointToStart;
        }

        SearchRoad();
    }

    public List<WayPoint> GiveThePath(WayPoint waypointToStart, WayPoint waypointToFinish)
    {
        _target = waypointToFinish;
        InitAStar(waypointToStart);
        return _openWaypoints;
    }

    private void SearchRoad()
    {
        //A changer en mettant un init de astar en passant directement dans le map main le open 
        _openWaypoints = _map.config.allWayPoints;
        List<WayPoint> _wayPointToCheck = _openWaypoints;
        while (_openWaypoints.Count > 0)
        {
            WayPoint _currentWaypoint = _openWaypoints[0];

            for (int i = 0; i < _openWaypoints.Count; i++)
            {
                /*if (_openWaypoints[i] == waypointChosen)
                {

                }

                if (_openWaypoints[i].fCost < _currentWaypoint.fCost || (openList[i].fCost == _currentWaypoint.fCost && openList[i].hCase < _currentWaypoint.hCase))
                {
                    _currentWaypoint = _openWaypoints[i];
                }*/
                if (_openWaypoints[i] == _currentWaypoint)
                {
                    //ne rien faire
                }

                if (_openWaypoints[i].caseCost < _currentWaypoint.caseCost)
                {
                    _currentWaypoint = _openWaypoints[i];
                }
            }

            /*if (_currentWaypoint == waypointChosen.GetComponent<WayPoint>())
            {
                FindThePath();
            }*/

            _closeWaypoints.Add(_currentWaypoint);
            _openWaypoints.Remove(_currentWaypoint);

            for (int i = 0; i < _wayPointToCheck.Count - 1; i++)
            {
                CheckNeighbour(_currentWaypoint);
            }
        }
    }

    private void CheckNeighbour(WayPoint _currentWaypoint)
    {
        foreach (WayPoint waypoint in _currentWaypoint.neighbour)
        {
            if (_closeWaypoints.Contains(waypoint))
            {
            }
            else if (!_openWaypoints.Contains(waypoint))
            {
                _openWaypoints.Add(waypoint);
                ChangeParent(waypoint, _currentWaypoint);
            }
            else if (waypoint.caseCost > _currentWaypoint.caseCost)
            {
                ChangeParent(waypoint, _currentWaypoint);
            }
        }
    }

    private void ChangeParent(WayPoint newWaypoint, WayPoint parent)
    {
        newWaypoint.parent = parent;
        newWaypoint.caseCost = parent.caseCost;
    }
}
