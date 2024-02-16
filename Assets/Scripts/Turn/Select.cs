using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    [SerializeField]
    private TurnManager _turnManager;

    [Header("Masks & raycast Variables")]
    [SerializeField]
    private LayerMask mapMask;

    [SerializeField]
    private LayerMask ennemiesMask;

    [SerializeField]
    private LayerMask alliesMask;

    private Ray _ray;
    private RaycastHit _hit;

    /// <summary>
    /// Event triggered when the player click the input to select. Depending on the current phase, it will select the character, the target, the destination or the ally.
    /// </summary>
    /// <param name="ctx">The callbackContext for the method.</param>
    public void OnInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started && _turnManager.PlayerTurn)
        {
            if (_turnManager.CharacterSelectionToMove || _turnManager.CharacterSelectionToAttack || _turnManager.CharacterSelection)
            {
                SelectCharacter();
            }
            else if (_turnManager.TargetSelection)
            {
                SelectTarget();
            }
            else if (_turnManager.DestinationSelection)
            {
                SelectDestination();
            }
            else if (_turnManager.AllySelection)
            {
                SelectAlly();
            }

            return;
        }
    }

    /// <summary>
    /// Event triggered when the player move the mouse. It will check if the player is in the destination selection phase and if so, it will use the A* algorithm to premove the player.
    /// </summary>
    public void OnMoveTheMouse()
    {
        if (_turnManager.DestinationSelection)
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, 1000, mapMask))
            {
                if (_hit.collider.tag == "Map")
                {
                    _turnManager.ManagerMain.mapMain.PAMax = _turnManager.Character.PaCurrent;
                    _turnManager.ManagerMain.mapMain.UseAStarToPremoveThePlayer(_hit.collider.GetComponent<WayPoint>());
                }
            }
        }
    }

    /// <summary>
    /// Select the character with a raycast to check if the target is an ally.
    /// </summary>
    public void SelectCharacter()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, 1000, alliesMask))
        {
            GameObject current = _hit.transform.gameObject;
            if (!current.CompareTag("Character"))
            {
                _turnManager.SetUIText("This is not a character");
                return;
            }

            switch (_turnManager.Character)
            {
                case null:
                    _turnManager.SetCharacter(current);
                    break;
                case not null:
                    if (_turnManager.Character.gameObject != current)
                    {
                        _turnManager.SetCharacter(current);
                    }

                    break;
            }
        }
    }

    /// <summary>
    /// Select the target with a raycast to check if the target is an ennemy.
    /// </summary>
    public void SelectTarget()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, 1000, ennemiesMask))
        {
            GameObject current = _hit.transform.gameObject;
            if (!current.CompareTag("Ennemy"))
            {
                _turnManager.SetUIText("This is not an ennemy");
                return;
            }

            switch (_turnManager.Target)
            {
                case null:
                    _turnManager.SetTarget(current);
                    break;
                case not null:
                    if (_turnManager.Target.gameObject != current)
                    {
                        _turnManager.SetTarget(current);
                    }

                    _turnManager.EndTargetSelectionPhase();
                    break;
            }
        }
    }

    /// <summary>
    /// Select the destination with a raycast to check if the target is a map.
    /// </summary>
    public void SelectDestination()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, 1000, mapMask))
        {
            GameObject current = _hit.transform.gameObject;
            if (current.CompareTag("Map"))
            {
                switch (_turnManager.Destination)
                {
                    case null:
                        _turnManager.SetDestination(current.GetComponent<WayPoint>());
                        break;
                    case not null:
                        if (_turnManager.Destination.gameObject != current)
                        {
                            _turnManager.SetDestination(current.GetComponent<WayPoint>());
                        }

                        _turnManager.EndDestinationSelectionPhase();
                        break;
                }
            }
            else
            {
                _turnManager.SetUIText("This is not a case of the map");
                return;
            }
        }
    }

    /// <summary>
    /// Select the ally with a raycast to check if the target is an ally.
    /// </summary>
    public void SelectAlly()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, 1000, alliesMask))
        {
            GameObject current = _hit.transform.gameObject;
            if (current.CompareTag("Character"))
            {
                switch (_turnManager.Ally)
                {
                    case null:
                        _turnManager.SetAlly(current);
                        break;
                    case not null:
                        if (_turnManager.Ally.gameObject != current)
                        {
                            _turnManager.SetAlly(current);
                        }

                        _turnManager.EndAllySelectionPhase();
                        break;
                }
            }
            else
            {
                _turnManager.SetUIText("This is not an ally");
                return;
            }
        }
    }
}
