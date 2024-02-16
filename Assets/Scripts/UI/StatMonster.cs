using UnityEngine;

public class StatMonster : MonoBehaviour
{
    public MonsterMain monsterMain;

    [SerializeField]
    private Gauge _gauge;

    public void InitUI(MonsterMain _monsterMain)
    {
        monsterMain = _monsterMain;
        monsterMain.MonsterHealth.OnHealthMonsterChange += OnHealthChanged;
        _gauge = GetComponentInParent<Gauge>();
       // _gauge._characterMain = _characterMain;
    }

    private void OnHealthChanged(int hp)
    {
        _gauge.ChangeGauge(monsterMain.HpMax, monsterMain.HpCurrent);
    }
}
