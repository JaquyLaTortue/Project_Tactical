using UnityEngine;

public class CastActions : MonoBehaviour
{
    [SerializeField] private TurnManager _turnManager;

    public void Move()
    {
        Debug.Log("Move");
        _turnManager.Character.Move();
    }

    public void Attack()
    {
        _turnManager.Character.Attack(_turnManager.Target);
    }

    public void Special()
    {
        _turnManager.Character.Special(_turnManager.Target);
    }
}
