using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class StatPlayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _atk;
    [SerializeField]
    private TextMeshProUGUI _def;
    [SerializeField]
    private TextMeshProUGUI _pa;

    public CharacterMain _characterMain;

    Gauge _gauge;

    public void InitUI(CharacterMain characterMain)
    {
        _characterMain = characterMain;
        _characterMain.CharacterHealth.OnHealthChanged += OnHealthChanged;
        _characterMain.CharacterCapacity.OnPAChanged += OnPAChanged;
        SetStat();
    }

    private void OnHealthChanged(int hp)
    {
        _gauge.ChangeGauge();
    }

    private void OnPAChanged(int pa)
    {
        _pa.text = "" + pa;
    }

    private void SetStat()
    {
        _atk.text = "" + _characterMain.Atk;
        _def.text = "" + _characterMain.Def;
        _pa.text = "" + _characterMain.PaCurrent;
    }
}
