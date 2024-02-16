using UnityEngine;

/// <summary>
/// Permet les refenrences de base pour les monstres.
/// </summary>
public class MonsterMain : Entity
{
    [field: SerializeField]
    public MonsterCapacity MonsterCapacity { get; private set; }

    [field: SerializeField]
    public MonsterHealth MonsterHealth { get; private set; }

    [field: SerializeField]
    public MonsterBase MonsterBase { get; private set; }

    private ManagerMain _managerMain;

    public void InitMonster(ManagerMain managerMain)
    {
        _managerMain = managerMain;
        MonsterCapacity._mapMain = _managerMain.mapMain;
        EntitiesManager = _managerMain.entitiesManager;
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
        EntitiesManager.RemoveEntity(this);
        _managerMain.turnManager.Target = null;
    }
}
