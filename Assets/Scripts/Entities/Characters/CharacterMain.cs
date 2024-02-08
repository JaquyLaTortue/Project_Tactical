using UnityEngine;

public class CharacterMain : Entity
{
    public CharacterCapacity CharacterCapacity { get; private set; }

    public CharacterHealth CharacterHealth { get; private set; }

    [field: SerializeField]
    public CharacterBase CharacterBase { get; private set; }

    private void Awake()
    {
        HpMax = CharacterBase.HpMax;
        HpCurrent = CharacterBase.HpMax;
        PaMax = CharacterBase.PaMax;
        PaCurrent = CharacterBase.PaMax;
        Atk = CharacterBase.Atk;
        Def = CharacterBase.Def;
    }
}
