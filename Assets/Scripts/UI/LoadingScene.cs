using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private string _nameScene;

    public void LoadScene()
    {
        SceneManager.LoadScene(_nameScene);
    }
}
