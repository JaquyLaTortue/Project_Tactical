using UnityEngine;

public class MonsterCapacity : MonoBehaviour
{
    [SerializeField]
    private MonsterMain _monsterMain;

    [SerializeField]
    private Capacity _capacity;

    public void Attack(CharacterMain target)
    {
        // Attack with normal attack
        Debug.Log("Attack: " + target);
        if (this._monsterMain.PaCurrent > 0)
        {
            target.CharacterHealth.TakeDamage(_monsterMain.Atk);
            this._monsterMain.PaCurrent--;
        }
        else
        {
            Debug.Log("No more PA");
        }
    }

    public void Move(WayPoint destination)
    {
        // Move to a new position
        Debug.Log("Move: " + destination);
    }

    public void Special(CharacterMain target)
    {
        // Attack with special ability
        Debug.Log("Special: " + target);
        if (this._monsterMain.PaCurrent > 0)
        {
            //target.CharacterHealth.TakeDamage(_capacity.damage);
            this._monsterMain.PaCurrent -= _capacity.cost;
        }
        else
        {
            Debug.Log("No more PA");
        }
    }
}
