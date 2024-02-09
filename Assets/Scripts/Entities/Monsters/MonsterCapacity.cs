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

    public void Attack(Entity target)
    {
        if (target is CharacterMain tmp)
        {
            // Attack with normal attack
            Debug.Log("Attack: " + target);
            if (this._monsterMain.PaCurrent > 0)
            {
                tmp.CharacterHealth.TakeDamage(_monsterMain.Atk);
                this._monsterMain.PaCurrent--;
            }
            else
            {
                Debug.Log("No more PA");
            }
        }
    }

    public void Move(WayPoint destination)
    {
        // Move to a new position
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
        }
        else
        {
            Debug.Log("No more PA");
        }
    }

    public void Special(Entity target)
    {
        // Attack with special ability
        Debug.Log("Special: " + target);
        if (target is CharacterMain tmp)
        {
            if (this._monsterMain.PaCurrent > 0)
            {
                tmp.CharacterHealth.TakeDamage(_capacity.damage);
                this._monsterMain.PaCurrent -= _capacity.cost;
            }
            else
            {
                Debug.Log("No more PA");
            }
        }
    }
}
