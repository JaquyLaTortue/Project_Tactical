using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Entities/Characters", order = 1)]
public class CharacterBase : ScriptableObject
{
    public int Atk;
    public int Def;
    public int Range;

    public int HpMax;
    public int HpCurrent;

    public int PaMax;
    public int PaCurrent;
}
