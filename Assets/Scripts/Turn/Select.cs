using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    [SerializeField]
    private TurnManager _turnManager;
    [SerializeField]
    private LayerMask mapMask;
    [SerializeField]
    private LayerMask entityMask;
    private Ray _ray;
    private RaycastHit _hit;


    public void OnInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started && _turnManager.PlayerTurn)
        {
            if (_turnManager.CharacterSelectionToMove || _turnManager.CharacterSelectionToAttack)
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

    public void SelectCharacter()
    {
        Debug.Log("Selecting Player");
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, 1000, entityMask))
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
                    if (_turnManager.Character.gameObject != current)
                    {
                        Debug.Log("Changing character");
                        _turnManager.SetCharacter(current);
                    }

                    break;
            }
        }
    }

    public void SelectTarget()
    {
        Debug.Log("Selecting Target");
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, 1000, entityMask))
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

        if (Physics.Raycast(_ray, out _hit, 1000, mapMask))
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
                        if (_turnManager.Destination.gameObject != current)
                        {
                            Debug.Log("Changing destination");
                            _turnManager.SetDestination(current.GetComponent<WayPoint>());
                        }

                        _turnManager.EndDestinationSelectionPhase();
                        break;
                }
            }
        }
    }

    public void SelectAlly()
    {
        Debug.Log("Selecting Ally");
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, 1000, mapMask))
        {
            GameObject current = _hit.transform.gameObject;
            if (current.CompareTag("Character"))
            {
                switch (_turnManager.Ally)
                {
                    case null:
                        Debug.Log("Setting ally");
                        _turnManager.SetAlly(current);
                        break;
                    case not null:
                        if (_turnManager.Ally.gameObject != current)
                        {
                            Debug.Log("Changing ally");
                            _turnManager.SetAlly(current);
                        }

                        _turnManager.EndAllySelectionPhase();
                        break;
                }
            }
        }
    }
}
