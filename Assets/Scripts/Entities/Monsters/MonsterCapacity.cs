using System.Collections.Generic;
using UnityEngine;

public class MonsterCapacity : MonoBehaviour
{
    [SerializeField]
    private MonsterMain _monsterMain;

    [SerializeField]
    private Capacity _capacity;

    private Entity _target;

    public MapMain _mapMain;

    [HideInInspector]
    public bool HasAttacked = false;
    [HideInInspector]
    public bool HasMoved = false;

    public void Attack(Entity target)
    {
        if (HasAttacked)
        {
            Debug.Log("Already attacked");
            return;
        }

        if (target is CharacterMain tmp)
        {
            if (this._monsterMain.PaCurrent > 0)
            {
                tmp.CharacterHealth.TakeDamage(_monsterMain.Atk);
                this._monsterMain.PaCurrent--;
                HasAttacked = true;
            }
            else
            {
                Debug.Log("No more PA");
            }
        }
    }

    public void Move(WayPoint destination)
    {
        if (HasMoved)
        {
            Debug.Log("Already moved");
            return;
        }

        List<WayPoint> path = new List<WayPoint>();
        if (this._monsterMain.PaCurrent > 0)
        {
            path = _mapMain.aStar.GiveThePath(this._monsterMain.Position, destination);
            if (path.Count <= this._monsterMain.PaCurrent)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    ChangeWaypoint(path[i]);

                    this._monsterMain.Position = path[i];
                    this.transform.position = path[i].transform.position;
                    this._monsterMain.PaCurrent--;
                }
            }
            else
            {
                Debug.Log("No more PA");
            }

            HasMoved = true;
            AttackAfterMove();
        }
        else
        {
            Debug.Log("No more PA");
        }
    }

    private void AttackAfterMove()
    {
        Attack(_target);
    }

    public void ChangeWaypoint(WayPoint waypointToMoveTo)
    {
        this._monsterMain.Position = waypointToMoveTo;
        this.transform.position = waypointToMoveTo.transform.position;
    }
}
