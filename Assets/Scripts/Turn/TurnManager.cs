using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    public event Action OnPlayerTurn;

    public event Action OnMonsterTurn;

    public event Action<CharacterMain> OnCharacterSelected;

    public event Action<MonsterMain> OnEnnemySelected;

    public int Turnindex { get; private set; } = 0;

    public bool PlayerTurn { get; private set; } = true;

    public CharacterMain Character { get; private set; }

    public MonsterMain Target { get; private set; }

    public WayPoint TargetPosition { get; private set; }

    [field: SerializeField]
    public PlayerInput InputManager { get; private set; }


    public void SetCharacter(GameObject character)
    {
        string oldcharacter = Character == null ? "null" : Character.name;
        Character = character.GetComponent<CharacterMain>();
        Debug.Log($"Character changement: old character : {oldcharacter} and new character : {character.name}");
        OnCharacterSelected?.Invoke(Character);
    }

    public void SetTarget(GameObject target)
    {
        string oldtarget = Target == null ? "null" : Target.name;
        Target = target.GetComponent<MonsterMain>();
        Debug.Log($"Target changement: old Target: {oldtarget} and new character : {target.name}");
        OnEnnemySelected?.Invoke(Target);
    }

    public void SetTargetPosition(WayPoint targetPosition)
    {
        TargetPosition = targetPosition;
        Debug.Log($"Target Position changement, new position : {targetPosition.name}");
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
                OnPlayerTurn?.Invoke();
                break;
            case 1:
                Debug.Log("Monster's Turn");
                PlayerTurn = false;
                OnMonsterTurn?.Invoke();
                break;
        }
    }
}
