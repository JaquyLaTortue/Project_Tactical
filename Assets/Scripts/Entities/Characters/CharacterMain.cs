using UnityEngine;

public class CharacterMain : Entity
{
    [Header("Character Components")]
    public CharacterCapacity CharacterCapacity;
    public CharacterHealth CharacterHealth;
    public CharacterBase CharacterBase;

    private void Awake()
    {
        hpMax = CharacterBase.hpMax;
        hpCurrent = CharacterBase.hpMax;
        paMax = CharacterBase.paMax;
        paCurrent = CharacterBase.paMax;
        atk = CharacterBase.atk;
        def = CharacterBase.def;
    }
}
