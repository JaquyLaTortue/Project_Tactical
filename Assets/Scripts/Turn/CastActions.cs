using UnityEngine;

public class CastActions : MonoBehaviour
{
    [SerializeField] private TurnManager _turnManager;

    public void Move()
    {
        if (_turnManager.Destination != null)
        {
            Debug.Log("Move");
            _turnManager.Character.CharacterCapacity.Move(_turnManager.Destination);
            _turnManager.ResetVariables();
        }
    }

    public void Attack()
    {
        if (_turnManager.Target != null)
        {
            Debug.Log("Attack");
            _turnManager.Character.CharacterCapacity.Attack(_turnManager.Target);
            _turnManager.ResetVariables();
        }
    }

    public void Special()
    {
        if (_turnManager.Target != null)
        {
            _turnManager.Character.CharacterCapacity.Special(_turnManager.Target);
            _turnManager.ResetVariables();
        }
    }
}
