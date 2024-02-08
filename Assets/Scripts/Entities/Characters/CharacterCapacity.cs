using UnityEngine;

public class CharacterCapacity : MonoBehaviour
{
    [SerializeField]
    private CharacterMain _characterMain;

    public void Attack(MonsterMain target)
    {
        // Attack with normal attack
        Debug.Log("Attack: " + target);
        
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
