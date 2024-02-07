using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public int atk;
    public int def;
    public int hpMax;
    public int hpCurrent;
    public int paMax;
    public int paCurrent;
    public WayPoint position;

    public void Attack(Entity target)
    {
        // Attack with normal attack
    }

    public void Move(int distance)
    {
        // Move to a new position
    }

    public void Special(Entity target)
    {
        // Attack with special ability
    }
}
