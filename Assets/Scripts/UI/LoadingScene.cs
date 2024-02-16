using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private EntitiesManager entitiesManager;

    public void LoadScene(string nameScene)
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            entitiesManager.ManualSceneChangement = true;
        }

        SceneManager.LoadScene(nameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
