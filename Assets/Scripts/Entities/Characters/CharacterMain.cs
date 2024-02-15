using UnityEngine;

public class CharacterMain : Entity
{
    [field: SerializeField]
    public CharacterCapacity CharacterCapacity { get; private set; }

    [field: SerializeField]
    public CharacterHealth CharacterHealth { get; private set; }

    [field: SerializeField]
    public CharacterBase CharacterBase { get; private set; }

    public void InitCharacter(MapMain map)
    {
        Debug.Log(CharacterCapacity);
        CharacterCapacity._map = map;
    }

    private void Awake()
    {
        HpMax = CharacterBase.HpMax;
        HpCurrent = CharacterBase.HpMax;
        PaMax = CharacterBase.PaMax;
        PaCurrent = CharacterBase.PaMax;
        Atk = CharacterBase.Atk;
        Def = CharacterBase.Def;
        Range = CharacterBase.Range;
    }
}
