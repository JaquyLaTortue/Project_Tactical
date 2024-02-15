using UnityEngine;

public class MonsterMain : Entity
{
    [field: SerializeField]
    public MonsterCapacity MonsterCapacity { get; private set; }

    [field: SerializeField]
    public MonsterHealth MonsterHealth { get; private set; }

    [field: SerializeField]
    public MonsterBase MonsterBase { get; private set; }

    public void InitMonster(MapMain map)
    {
        MonsterCapacity._mapMain = map;
    }

    private void Awake()
    {
        HpMax = MonsterBase.HpMax;
        HpCurrent = MonsterBase.HpMax;
        PaMax = MonsterBase.PaMax;
        PaCurrent = MonsterBase.PaMax;
        Atk = MonsterBase.Atk;
        Def = MonsterBase.Def;
        Range = MonsterBase.Range;
    }

    private void OnDestroy()
    {
        entitiesManager.allMonsters.Remove(this);
    }
}
