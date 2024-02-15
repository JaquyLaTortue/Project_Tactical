using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private WayPoint _target;
    private List<WayPoint> _path = new List<WayPoint>();
    public List<WayPoint> _openWaypoints = new List<WayPoint>();
    private List<WayPoint> _closeWaypoints = new List<WayPoint>();
    private MapMain _map;
    private bool _findTheTarget;

    /// <summary>
    /// Récupère le message du mapmain.
    /// </summary>
    /// <param name="main"></param>
    public void Init(MapMain main)
    {
        _map = main;
        _map.aStar = this;
    }

    /// <summary>
    /// C'est la fonction qui est appelée par mapmain pour récupérer le chemin du astar.
    /// </summary>
    /// <param name="waypointToStart"></param>
    /// <param name="waypointToFinish"></param>
    /// <returns></returns>
    public List<WayPoint> GiveThePath(WayPoint waypointToStart, WayPoint waypointToFinish)
    {
        _target = waypointToFinish;
        InitAStar(waypointToStart);
        return _path;
    }

    /// <summary>
    /// Ici on va remettre à zéro notre astar puis appelé la fonction qui va s'occuper de trouver le chemin du astar.
    /// </summary>
    /// <param name="waypointToStart"></param>
    private void InitAStar(WayPoint waypointToStart)
    {
        foreach (WayPoint path in _path)
        {
            path.parent = null;
            path.caseCost = 0;
            _map._clickedCase.ChangeColorOfWaypointToOld(path.GetComponent<MeshRenderer>());
        }

        _path.Clear();
        _openWaypoints.Clear();
        _closeWaypoints.Clear();

        _closeWaypoints.Add(waypointToStart);
        _findTheTarget = false;
        foreach (WayPoint waypoint in waypointToStart.neighbour)
        {
            _openWaypoints.Add(waypoint);
            waypoint.caseCost = 1;
            waypoint.parent = waypointToStart;
        }

        SearchRoad();
    }

    /// <summary>
    /// On va chercher tout les waypoints pour faire notre chemin du astar.
    /// </summary>
    private void SearchRoad()
    {
        List<WayPoint> _wayPointToCheck = new List<WayPoint>();
        _wayPointToCheck.AddRange(_openWaypoints);
        while (_openWaypoints.Count > 0 && !_findTheTarget)
        {
            WayPoint _currentWaypoint = _openWaypoints[0];

            for (int i = 0; i < _openWaypoints.Count; i++)
            {
                if (_openWaypoints[i] == _target)
                {
                    _currentWaypoint = _openWaypoints[i];
                    _findTheTarget = true;
                    break;
                }

                if (_openWaypoints[i].caseCost < _currentWaypoint.caseCost)
                {
                    _currentWaypoint = _openWaypoints[i];
                }
            }

            _closeWaypoints.Add(_currentWaypoint);
            _openWaypoints.Remove(_currentWaypoint);

            for (int i = 0; i < _wayPointToCheck.Count - 1; i++)
            {
                CheckNeighbour(_currentWaypoint);
            }
        }

        FindThePath();
    }

    /// <summary>
    /// On va vérifier les voisins des waypoints pour trouver le meilleur chemin.
    /// </summary>
    /// <param name="_currentWaypoint"></param>
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
        newWaypoint.caseCost = parent.caseCost + 1;
    }

    /// <summary>
    /// On va récupérer tout les parents des waypoints qu'on a séléctionner pour créer un chemin.
    /// </summary>
    private void FindThePath()
    {
        _path.Add(_target);
        WayPoint newParent = _target.parent;
        for (int i = 0; i < _target.caseCost-1; i++)
        {
            Debug.Log(newParent.name);
            _path.Add(newParent);
            newParent = newParent.parent;
        }

        /*
        while (newParent.parent != null)
        {
            _path.Add(newParent);
            newParent = newParent.parent;
        }*/
        _path.Add(newParent);
        _path.Reverse();
        foreach (WayPoint waypoint in _path)
        {
            Debug.Log(waypoint);
        }
    }
}
