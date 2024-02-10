using UnityEngine;

public abstract class MonsterBehaviour : MonoBehaviour
{
    [SerializeField]
    protected MonsterMain _monsterMain;

    [SerializeField]
    protected CharacterMain _targetPlayer;

    [SerializeField]
    protected EntitiesManager _entitiesManager;

    [SerializeField]
    protected TurnManager _turnManager;
}
