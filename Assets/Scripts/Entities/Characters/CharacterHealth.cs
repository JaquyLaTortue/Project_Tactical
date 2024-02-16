using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    private CharacterMain _characterMain;

    public event Action<int> OnHealthChanged;

    /// <summary>
    /// Prend des dégats et les applique à la vie du personnage.
    /// </summary>
    /// <param name="damage">Quantité de santé perdue.</param>
    public void TakeDamage(int damage)
    {
        Debug.Log("HP before attack : " + _characterMain.HpCurrent);
        _characterMain.HpCurrent -= Mathf.RoundToInt(damage * (100f / (100f + _characterMain.Def)));
        OnHealthChanged?.Invoke(_characterMain.HpCurrent);
        Debug.Log("HP after attack : " + _characterMain.HpCurrent);
        if (_characterMain.HpCurrent <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Tue le personnage.
    /// </summary>
    public void Die()
    {
        Debug.Log("Die");
        Destroy(gameObject);
    }

    /// <summary>
    /// Soigne le personnage.
    /// </summary>
    /// <param name="heal">Quantité de santé soignée.</param>
    public void HealHealth(int heal)
    {
        if (_characterMain.HpCurrent == _characterMain.HpMax)
        {
            Debug.Log("HP is already full");
            return;
        }

        Debug.Log("HP before heal : " + _characterMain.HpCurrent);
        _characterMain.HpCurrent += heal;
        OnHealthChanged?.Invoke(_characterMain.HpCurrent);
        Debug.Log("HP after heal : " + _characterMain.HpCurrent);
    }
}
