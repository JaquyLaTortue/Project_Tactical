using System.Collections.Generic;
using UnityEngine;

public class CharacterCapacity : MonoBehaviour
{
    [SerializeField]
    private CharacterMain _characterMain;

    [SerializeField]
    private Capacity _capacity;

    [SerializeField]
    private MapMain _map;

    private bool _hasAttacked = false;
    private bool _hasMoved = false;
    private bool _hasSpecial = false;

    public void Attack(MonsterMain target)
    {
        // Attack with normal attack
        if (_hasAttacked)
        {
            return;
        }

        Debug.Log("Attack: " + target);
        if (this._characterMain.PaCurrent > 0)
        {
            if (target.Position.casePosition[0] <= _characterMain.Position.casePosition[0] + _characterMain.Range
                || target.Position.casePosition[1] <= _characterMain.Position.casePosition[1] + _characterMain.Range)
            {
                Debug.Log("MonsterHealth : " + target.MonsterHealth);
                Debug.Log("Stats attack : " + _characterMain.Atk);
                target.MonsterHealth.TakeDamage(_characterMain.Atk);
                this._characterMain.PaCurrent--;
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
                for (int i = 0; i < path.Count; i++)
                {
                    this._characterMain.Position = path[i];
                    this.transform.position = path[i].transform.position;
                    this._characterMain.PaCurrent--;
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

    public void Special(Entity target)
    {
        if (_hasSpecial)
        {
            return;
        }

        Debug.Log("Special: " + target);
        if (target is MonsterMain tmp)
        {
            if (this._characterMain.PaCurrent > 0)
            {
                tmp.MonsterHealth.TakeDamage(_capacity.damage);
                this._characterMain.PaCurrent -= _capacity.cost;
                _hasSpecial = true;
            }
            else
            {
                Debug.Log("Not enough PA");
            }
        }
    }
}
