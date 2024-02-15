using UnityEngine;

public class Entity : MonoBehaviour
{
    public int Atk { get; set; }

    public int Def { get; set; }

    public int HpMax { get; set; }

    public int HpCurrent { get; set; }

    public int PaMax { get; set; }

    public int PaCurrent { get; set; }

    public int Range { get; set; }

    [field: SerializeField]
    public WayPoint Position { get; set; }

    [field: SerializeField]
    public WayPoint Spawnpoint { get; set; }

    public EntitiesManager EntitiesManager;

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
