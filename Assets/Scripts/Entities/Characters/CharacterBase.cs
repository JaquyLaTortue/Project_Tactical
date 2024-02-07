using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Entities/Characters", order = 1)]
public class CharacterBase : ScriptableObject
{
    [SerializeField]
    public int atk;
    [SerializeField]
    public int def;

    [SerializeField]
    public int hpMax;
    public int HpCurrent;

    [SerializeField]
    public int paMax;
    public int PaCurrent;

    public WayPoint position;
}
