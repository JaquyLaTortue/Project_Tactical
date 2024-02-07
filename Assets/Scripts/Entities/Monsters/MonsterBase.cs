using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Entities/Monsters", order = 1)]
public class MonsterBase : ScriptableObject
{
    public int Atk;
    public int Def;

    public int HpMax;
    public int HpCurrent;

    public int PaMax;
    public int PaCurrent;

    public WayPoint Position;
}
