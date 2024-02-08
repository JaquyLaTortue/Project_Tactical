using UnityEngine;

public class MonsterCapacity : MonoBehaviour
{
    [SerializeField]
    private MonsterMain _monsterMain;

    public void Attack(CharacterMain target)
    {
        // Attack with normal attack
        Debug.Log("Attack: " + target);
        target.CharacterHealth.TakeDamage(_monsterMain.atk);
    }

    public void Move(int distance)
    {
        // Move to a new position
        Debug.Log("Move: " + distance);
    }

    public void Special(Entity target)
    {
        // Attack with special ability
        Debug.Log("Special: " + target);
    }
}
