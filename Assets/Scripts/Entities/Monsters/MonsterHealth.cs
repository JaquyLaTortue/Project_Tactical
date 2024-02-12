using System;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField]
    private MonsterMain _monsterMain;

    public event Action<int> OnHealthMonsterChange;

    public void TakeDamage(int damage)
    {
        Debug.Log("HP before attack : " + _monsterMain.HpCurrent);
        _monsterMain.HpCurrent -= Mathf.RoundToInt(damage * (100f / (100f + _monsterMain.Def)));
        OnHealthMonsterChange?.Invoke(_monsterMain.HpCurrent);
        Debug.Log("HP after attack : " + _monsterMain.HpCurrent);
        if (_monsterMain.HpCurrent <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        this.transform.position = _monsterMain.Spawnpoint.transform.position;
        this.gameObject.SetActive(false);
    }
}
