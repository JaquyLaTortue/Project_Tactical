using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatPlayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _pv;
    [SerializeField]
    private TextMeshProUGUI _atk;
    [SerializeField]
    private TextMeshProUGUI _def;
    [SerializeField]
    private TextMeshProUGUI _pa;

    CharacterMain _characterMain;

    // Update is called once per frame
    void Update()
    {
        _pv.text = "" + _characterMain.HpCurrent;
        _atk.text = "" + _characterMain.Atk;
        _def.text = "" + _characterMain.Def;
        _pa.text = "" + _characterMain.PaCurrent;
    }
}
