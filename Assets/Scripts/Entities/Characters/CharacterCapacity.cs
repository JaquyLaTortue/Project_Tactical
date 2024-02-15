using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCapacity : MonoBehaviour
{
    [SerializeField]
    private CharacterMain _characterMain;

    [SerializeField]
    private Capacity _capacity;

    public MapMain _map;

    public event Action<int> OnPAChanged;

    private bool _hasAttacked = false;
    private bool _hasMoved = false;
    private bool _hasSpecial = false;

    public void Attack(MonsterMain target)
    {
        Debug.Log("Has already attacked: " + _hasAttacked);
        // Attack with normal attack
        if (_hasAttacked)
        {
            return;
        }

        Debug.Log("Attack: " + target);
        List<WayPoint> path = new List<WayPoint>();
        if (this._characterMain.PaCurrent > 0)
        {
            path = _map.aStar.GiveThePath(_characterMain.Position, target.Position);
            Debug.Log("Path count: " + (path.Count - 1));
            Debug.Log("Range: " + this._characterMain.Range);
            if (path.Count - 1 <= this._characterMain.Range)
            {
                Debug.Log("In Range !");
                target.MonsterHealth.TakeDamage(_characterMain.Atk);
                this._characterMain.PaCurrent--;
                OnPAChanged.Invoke(this._characterMain.PaCurrent);
                _hasAttacked = true;
            }
        }
        else
        {
            Debug.Log("Not enough PA");
        }
    }

    public void Move(WayPoint destination)
    {
        if (_hasMoved)
        {
            return;
        }

        List<WayPoint> path = new List<WayPoint>();
        Debug.Log("Move to : " + destination);
        if (this._characterMain.PaCurrent > 0)
        {
            path = _map.aStar.GiveThePath(_characterMain.Position, destination);
            if (path.Count <= this._characterMain.PaCurrent)
            {
                _characterMain.Position.obstacle = false;
                for (int i = 0; i < path.Count; i++)
                {
                    ChangeWaypoint(path[i]);

                    // this._characterMain.Position = path[i];
                    // this.transform.position = path[i].transform.position;
                    this._characterMain.PaCurrent--;
                    OnPAChanged.Invoke(this._characterMain.PaCurrent);
                    if (i == path.Count - 1)
                    {
                        path[i].obstacle = true;
                        path[i].entity = _characterMain;
                    }
                }
            }
            else
            {
                Debug.Log("Not enough PA to finish");
            }
            _hasMoved = true;
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
        if (_hasSpecial)
        {
            return;
        }

        Debug.Log("Special: " + target);
        if (_capacity.isHealing)
        {
            if (target is CharacterMain tmp)
            {
                if (this._characterMain.PaCurrent > 0)
                {
                    tmp.CharacterHealth.HealHealth(_capacity.damage);
                    this._characterMain.PaCurrent -= _capacity.cost;
                    OnPAChanged.Invoke(this._characterMain.PaCurrent);
                    _hasSpecial = true;
                }
                else
                {
                    Debug.Log("Not enough PA");
                }
            }
        }
        else if (_capacity.isShielding)
        {
            if (this._characterMain.PaCurrent > 0)
            {
                _characterMain.Def += _capacity.damage;
                this._characterMain.PaCurrent -= _capacity.cost;
                OnPAChanged.Invoke(this._characterMain.PaCurrent);
                _hasSpecial = true;
            }
            else
            {
                Debug.Log("Not enough PA");
            }
        }
        else
        {
            if (target is MonsterMain tmp)
            {
                if (this._characterMain.PaCurrent > 0)
                {
                    tmp.MonsterHealth.TakeDamage(_capacity.damage);
                    this._characterMain.PaCurrent -= _capacity.cost;
                    OnPAChanged.Invoke(this._characterMain.PaCurrent);
                    _hasSpecial = true;
                }
                else
                {
                    Debug.Log("Not enough PA");
                }
            }
        }
    }
}
