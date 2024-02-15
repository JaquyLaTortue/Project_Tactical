using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    [SerializeField] private TurnManager _turnManager;
    private Ray _ray;
    private RaycastHit _hit;

    public void OnInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started && _turnManager.PlayerTurn)
        {
            if (_turnManager.CharacterSelection)
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

            return;
        }
    }

    public void OnMoveTheMouse()
    {
        if (_turnManager.DestinationSelection)
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit))
            {
                if (_hit.collider.tag == "Map")
                {
                    _turnManager.managerMain.mapMain.PAMax = _turnManager.Character.PaCurrent;
                    _turnManager.managerMain.mapMain.UseAStarToPremoveThePlayer(_hit.collider.GetComponent<WayPoint>());
                }
            }
        }
    }

    public void SelectCharacter()
    {
        Debug.Log("Selecting Player");
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            GameObject current = _hit.transform.gameObject;
            if (!current.CompareTag("Character"))
            {
                Debug.Log("Not a character");
                return;
            }

            switch (_turnManager.Character)
            {
                case null:
                    Debug.Log("Setting character");
                    _turnManager.SetCharacter(current);
                    break;
                case not null:
                    Debug.Log("Changing character");
                    if (_turnManager.Character.gameObject != current)
                    {
                        _turnManager.SetCharacter(current);
                    }

                    _turnManager.EndCharacterSelectionPhase();
                    break;
            }
        }
    }

    public void SelectTarget()
    {
        Debug.Log("Selecting Target");
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            GameObject current = _hit.transform.gameObject;
            if (!current.CompareTag("Ennemy"))
            {
                Debug.Log("Not an ennemy");
                return;
            }

            switch (_turnManager.Target)
            {
                case null:
                    Debug.Log("Setting target");
                    _turnManager.SetTarget(current);
                    break;
                case not null:
                    Debug.Log("Changing target");
                    if (_turnManager.Target.gameObject != current)
                    {
                        _turnManager.SetTarget(current);
                    }

                    _turnManager.EndTargetSelectionPhase();
                    break;
            }
        }
    }

    public void SelectDestination()
    {
        Debug.Log("Selecting Destination");
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            GameObject current = _hit.transform.gameObject;
            if (current.CompareTag("Map"))
            {
                switch (_turnManager.Destination)
                {
                    case null:
                        Debug.Log("Setting destination");
                        _turnManager.SetDestination(current.GetComponent<WayPoint>());
                        break;
                    case not null:
                        Debug.Log("Changing destination");
                        if (_turnManager.Destination.gameObject != current)
                        {
                            _turnManager.SetDestination(current.GetComponent<WayPoint>());
                        }

                        _turnManager.EndDestinationSelectionPhase();
                        break;
                }
            }


            if (!current.CompareTag("Ennemy"))
            {
                Debug.Log("Not an ennemy");
                return;
            }

            switch (_turnManager.Target)
            {
                case null:
                    Debug.Log("Setting target");
                    _turnManager.SetTarget(current);
                    break;
                case not null:
                    Debug.Log("Changing target");
                    if (_turnManager.Target.gameObject != current)
                    {
                        _turnManager.SetTarget(current);
                    }

                    _turnManager.EndTargetSelectionPhase();
                    break;
            }
        }
    }
}
