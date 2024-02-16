using TMPro;
using UnityEngine;

public class StatPlayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _atk;
    [SerializeField]
    private TextMeshProUGUI _def;
    [SerializeField]
    private TextMeshProUGUI _pa;

    public CharacterMain _characterMain;

    private Gauge _gauge;

    public void InitUI(CharacterMain characterMain)
    {
        _characterMain = characterMain;
        _characterMain.CharacterHealth.OnHealthChanged += OnHealthChanged;
        _characterMain.CharacterCapacity.OnPAChanged += OnPAChanged;
        _gauge = GetComponentInParent<Gauge>();
        _gauge._characterMain = _characterMain;
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
