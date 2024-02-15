using UnityEngine;

public class CharacterMain : Entity
{
    [field: SerializeField]
    public CharacterCapacity CharacterCapacity { get; private set; }

    [field: SerializeField]
    public CharacterHealth CharacterHealth { get; private set; }

    [field: SerializeField]
    public CharacterBase CharacterBase { get; private set; }

    public void InitCharacter(MapMain map, EntitiesManager entitiesManager)
    {
        Debug.Log(CharacterCapacity);
        CharacterCapacity._map = map;
        EntitiesManager = entitiesManager;
        Spawnpoint = Position;
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
    }
}
