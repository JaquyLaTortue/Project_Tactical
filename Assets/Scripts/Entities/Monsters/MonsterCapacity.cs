using System.Collections.Generic;
using UnityEngine;

public class MonsterCapacity : MonoBehaviour
{
    [SerializeField]
    private MonsterMain _monsterMain;

    [SerializeField]
    private Capacity _capacity;

    [SerializeField]
    private MapMain _mapMain;

    private bool _hasAttacked = false;
    private bool _hasMoved = false;
    private bool _hasSpecialAttacking = false;

    public void Attack(Entity target)
    {
        if (_hasAttacked)
        {
            Debug.Log("Already attacked");
            return;
        }

        if (target is CharacterMain tmp)
        {
            // Attack with normal attack
            Debug.Log("Attack: " + target);
            if (this._monsterMain.PaCurrent > 0)
            {
                tmp.CharacterHealth.TakeDamage(_monsterMain.Atk);
                this._monsterMain.PaCurrent--;
                _hasAttacked = true;
            }
            else
            {
                Debug.Log("No more PA");
            }
        }
    }

    public void Move(WayPoint destination)
    {
        if (_hasMoved)
        {
            Debug.Log("Already moved");
            return;
        }

        List<WayPoint> path = new List<WayPoint>();
        Debug.Log("Move: " + destination);
        if (this._monsterMain.PaCurrent > 0)
        {
            path = _mapMain.aStar.GiveThePath(this._monsterMain.Position, destination);
            if (path.Count <= this._monsterMain.PaCurrent)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    this._monsterMain.Position = path[i];
                    this.transform.position = path[i].transform.position;
                    this._monsterMain.PaCurrent--;
                }
            }
            else
            {
                Debug.Log("No more PA");
            }

            _hasMoved = true;
        }
        else
        {
            Debug.Log("No more PA");
        }
    }

    public void Special(Entity target)
    {
        if (_hasSpecialAttacking)
        {
            Debug.Log("Already special attacked");
            return;
        }

        Debug.Log("Special: " + target);
        if (target is CharacterMain tmp)
        {
            if (this._monsterMain.PaCurrent > 0)
            {
                tmp.CharacterHealth.TakeDamage(_capacity.damage);
                this._monsterMain.PaCurrent -= _capacity.cost;
                _hasSpecialAttacking = true;
            }
            else
            {
                Debug.Log("No more PA");
            }
        }
    }
}
