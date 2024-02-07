using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCapacity : MonoBehaviour
{
    [SerializeField]
    private MonsterMain _characterMain;

    public void Attack(Entity target)
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
