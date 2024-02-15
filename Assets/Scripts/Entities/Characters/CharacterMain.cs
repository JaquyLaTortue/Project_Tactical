using UnityEngine;

public class CharacterMain : Entity
{
    [field: SerializeField]
    public CharacterCapacity CharacterCapacity { get; private set; }

    [field: SerializeField]
    public CharacterHealth CharacterHealth { get; private set; }

    [field: SerializeField]
    public CharacterBase CharacterBase { get; private set; }

    [field: SerializeField]
    public ManagerMain ManagerMain { get; private set; }

    public void InitCharacter(ManagerMain manager)
    {
        ManagerMain = manager;
        CharacterCapacity._map = ManagerMain.mapMain;
        CharacterCapacity._turnManager = ManagerMain.turnManager;
        EntitiesManager = ManagerMain.entitiesManager;
        Spawnpoint = Position;
    }

    public void InitCharacter(MapMain map, StatPlayer stat)
    {
        CharacterCapacity._map = map;
        stat.InitUI(this);
    }

    public void GetStat(StatPlayer stat)
    {
        stat._characterMain = this;
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

    private void OnDestroy()
    {
        EntitiesManager.allCharacters.Remove(this);
        ManagerMain.turnManager.Character = null;
    }
}
