using System.Collections.Generic;
using UnityEngine;

public class CharacterCapacity : MonoBehaviour
{
    [SerializeField]
    private CharacterMain _characterMain;

    [SerializeField]
    private Capacity _capacity;

    public MapMain _map;

    public void Attack(MonsterMain target)
    {
        // Attack with normal attack
        Debug.Log("Attack: " + target);
        if (this._characterMain.PaCurrent > 0)
        {
            Debug.Log("MonsterHealth : " + target.MonsterHealth);
            Debug.Log("Stats attack : " + _characterMain.Atk);
            target.MonsterHealth.TakeDamage(_characterMain.Atk);
            this._characterMain.PaCurrent--;
        }
        else
        {
            Debug.Log("Not enough PA");
        }
    }

    public void Move(WayPoint destination)
    {
        List<WayPoint> path = new List<WayPoint>();
        Debug.Log("Move to : " + destination);
        if (this._characterMain.PaCurrent > 0)
        {
            path = _map.aStar.GiveThePath(_characterMain.Position, destination);
            if (path.Count <= this._characterMain.PaCurrent)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    ChangeWaypoint(path[i]);

                    // this._characterMain.Position = path[i];
                    // this.transform.position = path[i].transform.position;
                    this._characterMain.PaCurrent--;
                }
            }
            else
            {
                Debug.Log("Not enough PA to finish");
            }
        }
        else
        {
            Debug.Log("Not enough PA to start");
        }

    }

    public void ChangeWaypoint(WayPoint waypointToMoveTo)
    {
        this._characterMain.Position = waypointToMoveTo;
        this.transform.position = waypointToMoveTo.transform.position;
    }

    public void Special(Entity target)
    {
        // Attack with special ability
        Debug.Log("Special: " + target);
        if (target is MonsterMain tmp)
        {
            if (this._characterMain.PaCurrent > 0)
            {
                tmp.MonsterHealth.TakeDamage(_capacity.damage);
                this._characterMain.PaCurrent -= _capacity.cost;
            }
            else
            {
                Debug.Log("Not enough PA");
            }
        }
    }
}
