using UnityEngine;

public class MonsterMain : Entity
{
    [Header("Character Components")]
    public MonsterCapacity CharacterCapacity;
    public MonsterHealth CharacterHealth;
    public MonsterBase CharacterBase;

    private void Awake()
    {
        hpMax = CharacterBase.HpMax;
        hpCurrent = CharacterBase.HpMax;
        paMax = CharacterBase.PaMax;
        paCurrent = CharacterBase.PaMax;
        atk = CharacterBase.Atk;
        def = CharacterBase.Def;
    }
}
