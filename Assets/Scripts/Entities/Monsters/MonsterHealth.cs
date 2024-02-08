using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField]
    private MonsterMain _monsterMain;

    public void TakeDamage(int damage)
    {
        Debug.Log("HP before attack : " + _monsterMain.HpCurrent);
        _monsterMain.HpCurrent -= Mathf.RoundToInt(damage * (100f / (100f + _monsterMain.Def)));
        Debug.Log("HP after attack : " + _monsterMain.HpCurrent);
        if (_monsterMain.HpCurrent <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        // TP to spawn point
        // Desactivate the character
    }

    //public void HealHealth(int heal)
    //{
    //    if (_monsterMain.hpCurrent == _monsterMain.hpMax)
    //    {
    //        return;
    //    }

    //    Debug.Log("HP before heal : " + _monsterMain.hpCurrent);
    //    _monsterMain.hpCurrent += heal;
    //    Debug.Log("HP after heal : " + _monsterMain.hpCurrent);
    //}
}
