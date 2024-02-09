using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    [SerializeField] private TurnManager _turnManager;
    private Ray _ray;

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


    public void SelectCharacter()
    {
        Debug.Log("Selecting Player");
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit))
        {
            GameObject current = hit.transform.gameObject;
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

                    break;
            }
        }
    }

    public void SelectTarget()
    {
        Debug.Log("Selecting Target");
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit))
        {
            GameObject current = hit.transform.gameObject;
            if (_turnManager.Target != current && current.CompareTag("Ennemy"))
            {
                _turnManager.SetTarget(current);
            }
        }
    }

    public void SelectDestination()
    {
        Debug.Log("Selecting Destination on map");
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit))
        {
            GameObject current = hit.transform.gameObject;
            if (current.CompareTag("Map"))
            {
                _turnManager.SetDestination(current.GetComponent<WayPoint>());
            }
        }
    }
}
