using UnityEngine;

public class MonsterCapacity : MonoBehaviour
{
    [SerializeField]
    private MonsterMain _monsterMain;

    public void Attack(CharacterMain target)
    {
        // Attack with normal attack
        Debug.Log("Attack: " + target);
        target.CharacterHealth.TakeDamage(_monsterMain.Atk);
    }

    public void Move(WayPoint destination)
    {
        // Move to a new position
        Debug.Log("Move: " + destination);
    }

    public void Special(Entity target)
    {
        // Attack with special ability
        Debug.Log("Special: " + target);
    }
}
