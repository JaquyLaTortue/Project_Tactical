using UnityEngine;

[CreateAssetMenu(fileName = "Capacity", menuName = "Capacities")]
public class Capacity : ScriptableObject
{
    public string id;
    public string name;
    public string description;
    public int cost;
    public int damage;
    public int range;
}
