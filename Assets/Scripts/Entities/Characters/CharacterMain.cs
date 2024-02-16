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

    /// <summary>
    /// Initialise le personnage et le lie au manager.
    /// </summary>
    /// <param name="manager">Manager principal.</param>
    /// <param name="stat">Widget de statistique.</param>
    public void InitCharacter(ManagerMain manager, StatPlayer stat)
    {
        ManagerMain = manager;
        CharacterCapacity._map = ManagerMain.mapMain;
        CharacterCapacity._turnManager = ManagerMain.turnManager;
        EntitiesManager = ManagerMain.entitiesManager;
        Spawnpoint = Position;
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
        EntitiesManager.RemoveEntity(this);
        ManagerMain.turnManager.Character = null;
    }
}
