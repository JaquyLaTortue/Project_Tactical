using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<StatPlayer> _list;
    public List<StatMonster> _monsterList;
    [SerializeField]
    private LoadingScene sceneLoader;

    public void InitManager(ManagerMain MM)
    {
        MM.uiManager = this;
    }

    public void Win()
    {
        sceneLoader.LoadScene("VictoryScene");
    }

    public void Lose()
    {
        sceneLoader.LoadScene("DefeatScene");
    }
}
