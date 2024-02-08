using UnityEngine;

public class MonsterMain : Entity
{
    [Header("Character Components")]
    public MonsterCapacity MonsterCapacity;
    public MonsterHealth MonsterHealth;
    public MonsterBase MonsterBase;

    private void Awake()
    {
        hpMax = MonsterBase.HpMax;
        hpCurrent = MonsterBase.HpMax;
        paMax = MonsterBase.PaMax;
        paCurrent = MonsterBase.PaMax;
        atk = MonsterBase.Atk;
        def = MonsterBase.Def;
    }
}
