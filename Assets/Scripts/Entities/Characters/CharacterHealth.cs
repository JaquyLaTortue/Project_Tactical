using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public CharacterMain CharacterMain;

    public void TakeDamage(int damage)
    {
        Debug.Log("HP before attack : " + CharacterMain.hpCurrent);
        CharacterMain.hpCurrent -= damage;
        Debug.Log("HP after attack : " + CharacterMain.hpCurrent);
        if (CharacterMain.hpCurrent <= 0)
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
        if (CharacterMain.hpCurrent == CharacterMain.hpMax)
        {
            return;
        }

        Debug.Log("HP before heal : " + CharacterMain.hpCurrent);
        CharacterMain.hpCurrent += heal;
        Debug.Log("HP after heal : " + CharacterMain.hpCurrent);
    }
}
