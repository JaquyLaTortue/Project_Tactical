using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    private CharacterMain _characterMain;

    public void TakeDamage(int damage)
    {
        Debug.Log("HP before attack : " + _characterMain.hpCurrent);
        _characterMain.hpCurrent -= damage * (100 / (100 + _characterMain.def));
        Debug.Log("HP after attack : " + _characterMain.hpCurrent);
        if (_characterMain.hpCurrent <= 0)
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

    public void HealHealth(int heal)
    {
        if (_characterMain.hpCurrent == _characterMain.hpMax)
        {
            Debug.Log("HP is already full");
            return;
        }

        Debug.Log("HP before heal : " + _characterMain.hpCurrent);
        _characterMain.hpCurrent += heal;
        Debug.Log("HP after heal : " + _characterMain.hpCurrent);
    }
}
