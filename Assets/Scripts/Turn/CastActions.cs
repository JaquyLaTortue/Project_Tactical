using UnityEngine;

public class CastActions : MonoBehaviour
{
    [SerializeField] private TurnManager _turnManager;

    public void Move()
    {
        if (_turnManager.TargetPosition != null)
        {
            Debug.Log("Move");
            _turnManager.Character.CharacterCapacity.Move(_turnManager.TargetPosition);
        }
    }

    public void Attack()
    {
        if (_turnManager.Target != null)
        {
            _turnManager.Character.Attack(_turnManager.Target);
        }
    }

    public void Special()
    {
        if (_turnManager.Target != null)
        {
            _turnManager.Character.Special(_turnManager.Target);
        }
    }
}
