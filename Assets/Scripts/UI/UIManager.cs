using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<StatPlayer> _list;

    public void InitManager(ManagerMain MM)
    {
        MM.uiManager = this;
    }
}
