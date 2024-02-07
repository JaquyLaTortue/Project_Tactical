using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public MonsterMain _monsterMain;

    public void TakeDamage(int damage)
    {
        Debug.Log("HP before attack : " + _monsterMain.hpCurrent);
        _monsterMain.hpCurrent -= damage;
        Debug.Log("HP after attack : " + _monsterMain.hpCurrent);
        if (_monsterMain.hpCurrent <= 0)
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
