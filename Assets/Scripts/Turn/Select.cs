using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    [SerializeField] private TurnManager _turnManager;
    private Ray _ray;

    public void SelectPlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.started && _turnManager.PlayerTurn)
        {
            Debug.Log("Selecting Player");
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray, out hit))
            {
                GameObject current = hit.transform.gameObject;
                Debug.Log("Hit: " + current.name);

                if (_turnManager.Character != current && current.CompareTag("Character"))
                {
                    _turnManager.SetCharacter(current);
                }

                _turnManager.InputManager.SwitchCurrentActionMap("EmptyActionMap");
            }
        }
        else if (ctx.started && !_turnManager.PlayerTurn)
        {
            Debug.Log("Not Player's Turn");
            _turnManager.InputManager.SwitchCurrentActionMap("EmptyActionMap");
        }
    }

    public void SelectTarget(InputAction.CallbackContext ctx)
    {
        if (ctx.started && _turnManager.PlayerTurn)
        {
            Debug.Log("Selecting Target");
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray, out hit))
            {
                GameObject current = hit.transform.gameObject;
                Debug.Log($"Hit: {current.name}");

                if (_turnManager.Target != current && current.CompareTag("Ennemy"))
                {
                    _turnManager.SetTarget(current);
                }

                _turnManager.InputManager.SwitchCurrentActionMap("EmptyActionMap");
            }
        }
        else if (ctx.started && !_turnManager.PlayerTurn)
        {
            Debug.Log("Not Player's Turn");
            _turnManager.InputManager.SwitchCurrentActionMap("EmptyActionMap");
        }
    }

    public void SelectDestination(InputAction.CallbackContext ctx)
    {
        if (ctx.started && _turnManager.PlayerTurn)
        {
            Debug.Log("Selecting Destination on map");
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray, out hit))
            {
                GameObject current = hit.transform.gameObject;
                Debug.Log($"Hit: {current.name}");

                if (current.CompareTag("Map"))
                {
                    _turnManager.SetTargetPosition(current.GetComponent<WayPoint>());
                }

                _turnManager.InputManager.SwitchCurrentActionMap("EmptyActionMap");
            }
        }
        else if (ctx.started && !_turnManager.PlayerTurn)
        {
            Debug.Log("Not Player's Turn");
            _turnManager.InputManager.SwitchCurrentActionMap("EmptyActionMap");
        }
    }
}
