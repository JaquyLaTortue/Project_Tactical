using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private ManagerMain _managerMain;
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private PlayerInput _playerInput;

    private bool isPaused = false;

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (!ctx.started)
        {
            return;
        }

        isPaused = !isPaused;
        _pauseMenu.SetActive(isPaused);
        _managerMain.turnManager.EndCharacterSelectionPhase();
        _managerMain.turnManager.EndTargetSelectionPhase();
        _managerMain.turnManager.EndDestinationSelectionPhase();
        _managerMain.turnManager.EndAllySelectionPhase();
        switch (isPaused)
        {
            case true:
                _playerInput.SwitchCurrentActionMap("Pause");
                break;
            case false:
                _playerInput.SwitchCurrentActionMap("Game");
                break;
        }
    }

    public void Resume()
    {
        isPaused = false;
        _pauseMenu.SetActive(false);
        _managerMain.turnManager.InputManager.SwitchCurrentActionMap("Game");
    }
}
