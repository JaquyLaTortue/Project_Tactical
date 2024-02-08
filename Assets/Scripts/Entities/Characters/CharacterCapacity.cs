using UnityEngine;

public class CharacterCapacity : MonoBehaviour
{
    [SerializeField]
    private CharacterMain _characterMain;

    [SerializeField]
    private Capacity _capacity;

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
        // Move to a new position
        Debug.Log("Move to : " + destination);
    }

    public void Special(MonsterMain target)
    {
        // Attack with special ability
        Debug.Log("Special: " + target);
        if (this._characterMain.PaCurrent > 0)
        {
            target.MonsterHealth.TakeDamage(_capacity.damage);
            this._characterMain.PaCurrent -= _capacity.cost;
        }
        else
        {
            Debug.Log("Not enough PA");
        }
    }
}
