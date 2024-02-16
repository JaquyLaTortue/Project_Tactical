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

    private bool _isPaused = false;

    /// <summary>
    /// Pause the game display the pause menu & Switch the action map to avoid the player to do something unwanted.
    /// </summary>
    /// <param name="ctx">the callbackcontext needed.</param>
    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (!ctx.started)
        {
            return;
        }

        _isPaused = !_isPaused;
        _pauseMenu.SetActive(_isPaused);
        _managerMain.turnManager.EndCharacterSelectionPhase();
        _managerMain.turnManager.EndTargetSelectionPhase();
        _managerMain.turnManager.EndDestinationSelectionPhase();
        _managerMain.turnManager.EndAllySelectionPhase();
        switch (_isPaused)
        {
            case true:
                _playerInput.SwitchCurrentActionMap("Pause");
                break;
            case false:
                _playerInput.SwitchCurrentActionMap("Game");
                break;
        }
    }

    /// <summary>
    /// Resume the game and switch the action map to the game one.
    /// </summary>
    public void Resume()
    {
        _isPaused = false;
        _pauseMenu.SetActive(false);
        _managerMain.turnManager.InputManager.SwitchCurrentActionMap("Game");
    }

    /// <summary>
    /// Quit the game.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
