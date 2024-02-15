using UnityEngine;

/// <summary>
/// Permet les références de base pour les comportements des monstres.
/// </summary>
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

    public void InitBehaviour(ManagerMain mM)
    {
        _entitiesManager = mM.entitiesManager;
        _turnManager = mM.turnManager;
    }
}
