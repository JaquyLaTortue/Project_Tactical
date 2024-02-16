using UnityEngine;

/// <summary>
/// Class used to cast actions: Move, Attack or Special.
/// </summary>
public class CastActions : MonoBehaviour
{
    [SerializeField] private TurnManager _turnManager;

    /// <summary>
    /// Cast the move action and change the character position & then reset the variables.
    /// </summary>
    public void Move()
    {
        if (_turnManager.Destination != null)
        {
            _turnManager.Character.CharacterCapacity.Move(_turnManager.Destination);
            _turnManager.ResetVariables();
        }

        _turnManager.ResetVariables();
    }

    /// <summary>
    /// Cast the attack action to the target and then reset the variables.
    /// </summary>
    public void Attack()
    {
        if (_turnManager.Target != null)
        {
            _turnManager.Character.CharacterCapacity.Attack(_turnManager.Target);
            _turnManager.ResetVariables();
        }

        _turnManager.ResetVariables();
    }

    /// <summary>
    /// Cast the special action and then switch the phase to match the current character's special capacity & reset the variables.
    /// </summary>
    public void Special()
    {
        switch (_turnManager.Character.CharacterCapacity.Capacity.name)
        {
            case "Heal":
                _turnManager.Character.CharacterCapacity.Special(_turnManager.Ally);
                break;
            case "Shield":
                _turnManager.Character.CharacterCapacity.Special(_turnManager.Character);
                break;
            case "Ultimate Attack":
                _turnManager.Character.CharacterCapacity.Special(_turnManager.Target);
                break;
            default:
                break;
        }

        if (_turnManager.Target != null)
        {
            _turnManager.Character.CharacterCapacity.Special(_turnManager.Target);
            _turnManager.ResetVariables();
        }

        _turnManager.ResetVariables();
    }
}
