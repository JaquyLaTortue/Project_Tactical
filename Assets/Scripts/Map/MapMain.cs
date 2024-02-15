using System.Collections.Generic;
using UnityEngine;

public class MapMain : MonoBehaviour
{
    public MapConfig config;
    public InitMap init;
    public AStar aStar;

    private ManagerMain managerMain;

    public WayPoint wayPointStart;

    // ClickedCase n'existera pas à la fin du projet, cet un outil de test
    public ClickedCase _clickedCase;

    public int PAMax;

    public void InitManager(ManagerMain MM)
    {
        MM.mapMain = this;
        managerMain = MM;
    }

    /// <summary>
    /// Cette fonction ne sert que pour les tests de début de projet, à utiliser que pour le astar.
    /// </summary>
    /// <param name="end">La fin de notre chemin.</param>
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

    public void UseAStarToPremoveThePlayer(WayPoint end)
    {
        if (wayPointStart == end)
        {
            Debug.Log("Les deux Waypoints sont identiques, donc on utilise pas le astar");
        }
        else
        {
            List<WayPoint> aStartWaypoint = new List<WayPoint>();
            aStartWaypoint = aStar.GiveThePath(wayPointStart, end);
            _clickedCase.ChangeColorOfWaypointToOld(wayPointStart.meshRenderer);
            for (int i = 0; i < aStartWaypoint.Count; i++)
            {
                if (i > PAMax)
                {
                    _clickedCase.ChangeColorOfWaypointToRed(aStartWaypoint[i].meshRenderer);
                }
                else
                {
                    _clickedCase.ChangeColorOfWaypointToBlue(aStartWaypoint[i].meshRenderer);
                }
            }

           /* foreach (WayPoint wayPoint in aStartWaypoint)
            {
                _clickedCase.ChangeColorOfWaypointToRed(wayPoint.meshRenderer);
            }*/
        }
    }

    public List<WayPoint> UseAStar(WayPoint start, WayPoint end)
    {
        /*WayPoint start = config.allWayPoints[Random.Range(0, config.allWayPoints.Count)];
        Debug.Log(start.name);
        WayPoint end = config.allWayPoints[Random.Range(0, config.allWayPoints.Count)];
        Debug.Log(end.name);*/
        wayPointStart = start;
        List<WayPoint> aStartWaypoint = new List<WayPoint>();
        aStartWaypoint = aStar.GiveThePath(start, end);
        _clickedCase.ChangeColorOfWaypointToOld(wayPointStart.meshRenderer);
        wayPointStart = aStartWaypoint[aStartWaypoint.Count - 1];
        //foreach (WayPoint wayPoint in aStartWaypoint)
        //{
        //    _clickedCase.ChangeColorOfWaypointToRed(wayPoint.meshRenderer);
        //}

        return aStartWaypoint;
    }

    private void Awake()
    {
        InitGameObject();
    }

    private void Start()
    {
        _clickedCase = GetComponent<ClickedCase>();
        _clickedCase.Init(this);
        managerMain.entitiesManager.InitEntities();
    }

    private void InitGameObject()
    {
        SendMessage("Init", this);
        config.CreateWaypoint();
    }
}
