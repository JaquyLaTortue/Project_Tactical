using UnityEngine;

public class CharacterCapacity : MonoBehaviour
{
    [SerializeField]
    private CharacterMain _characterMain;

    public void Attack(MonsterMain target)
    {
        // Attack with normal attack
        Debug.Log("Attack: " + target);
        target.MonsterHealth.TakeDamage(_characterMain.atk);
    }

    public void Move(WayPoint destination)
    {
        // Move to a new position
        Debug.Log("Move to : " + destination);
    }

    public void Special(Entity target)
    {
        // Attack with special ability
        Debug.Log("Special: " + target);
    }
}
