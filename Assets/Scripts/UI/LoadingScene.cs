using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private string _nameScene;

    public void LoadScenne()
    {
        SceneManager.LoadScene(_nameScene);
    }
}
