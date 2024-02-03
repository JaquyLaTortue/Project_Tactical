using UnityEngine;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{

    public int Turnindex { get; private set; } = 0;

    public bool PlayerTurn { get; private set; } = true;

    public GameObject Character { get; private set; }

    public GameObject Target { get; private set; }

    [field: SerializeField]
    public PlayerInput InputManager { get; private set; }

    public void SetCharacter(GameObject character)
    {
        string oldcharacter = Character == null ? "null" : Character.name;
        Character = character;
        Debug.Log($"Character changement: old character : {oldcharacter} and new character : {character.name}");
    }

    public void SetTarget(GameObject target)
    {
        string oldtarget = Target == null ? "null" : Target.name;
        Target = target;
        Debug.Log($"Target changement: old Target: {oldtarget} and new character : {target.name}");
    }

    public void EndTurn()
    {
        Turnindex++;
        InputManager.SwitchCurrentActionMap("EmptyActionMap");
        DetermineTurn();
    }

    private void DetermineTurn()
    {
        switch (Turnindex % 2)
        {
            case 0:
                Debug.Log("Player's Turn");
                PlayerTurn = true;
                break;
            case 1:
                Debug.Log("Monster's Turn");
                PlayerTurn = false;
                break;
        }
    }
}
