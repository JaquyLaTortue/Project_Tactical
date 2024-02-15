using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _turnText;

    public event Action OnPlayerTurn;

    public event Action OnMonsterTurn;

    public event Action<CharacterMain> OnCharacterSelected;

    public event Action<MonsterMain> OnEnnemySelected;

    public ManagerMain ManagerMain { get; set; }

    public int Turnindex { get; private set; } = 0;

    public bool PlayerTurn { get; private set; } = true;

    [field: SerializeField]
    public CharacterMain Character { get; set; }

    public MonsterMain Target { get; set; }

    public CharacterMain Ally { get; private set; }

    public WayPoint Destination { get; private set; }

    [field: SerializeField]
    public PlayerInput InputManager { get; private set; }

    public bool CharacterSelectionToMove { get; private set; } = false;

    public bool CharacterSelectionToAttack { get; private set; } = false;

    public bool TargetSelection { get; private set; } = false;

    public bool DestinationSelection { get; private set; } = false;

    public bool AllySelection { get; private set; } = false;

    public void InitManager(ManagerMain mM)
    {
        mM.turnManager = this;
        ManagerMain = mM;
    }

    public void CharacterSelectionToMovePhase()
    {
        ResetVariables();
        CharacterSelectionToMove = true;
        _turnText.text = "Select a character";
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();
    }

    public void CharacterSelectionToAttackPhase()
    {
        ResetVariables();
        CharacterSelectionToAttack = true;
        _turnText.text = "Select a character";
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();
    }

    public void EndCharacterSelectionPhase()
    {
        CharacterSelectionToAttack = false;
        CharacterSelectionToMove = false;
    }

    public void TargetSelectionPhase()
    {
        TargetSelection = true;
        _turnText.text = "Select a target";
        EndCharacterSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();
    }

    public void EndTargetSelectionPhase()
    {
        TargetSelection = false;
    }

    public void DestinationSelectionPhase()
    {
        DestinationSelection = true;
        _turnText.text = "Select a destination";
        EndCharacterSelectionPhase();
        EndTargetSelectionPhase();
        EndAllySelectionPhase();
    }

    public void EndDestinationSelectionPhase()
    {
        DestinationSelection = false;
    }

    public void AllySelectionPhase()
    {
        AllySelection = true;
        _turnText.text = "Select an ally";
        EndCharacterSelectionPhase();
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
    }

    public void EndAllySelectionPhase()
    {
        AllySelection = false;
    }

    public void SetCharacterToMove(GameObject character)
    {
        string oldcharacter;
        switch (Character)
        {
            case null:
                oldcharacter = null;
                break;
            case not null:
                foreach (Transform child in Character.transform)
                {
                    child.gameObject.layer = 0;
                }

                oldcharacter = Character.name;
                break;
        }

        Character = character.GetComponent<CharacterMain>();
        foreach (Transform child in character.transform)
        {
            child.gameObject.layer = 7;
        }

        ManagerMain.mapMain.wayPointStart = Character.Position;
        Debug.Log($"Character changement: old character : {oldcharacter} and new character : {Character.name}");
        OnCharacterSelected?.Invoke(Character);
        EndCharacterSelectionPhase();
        DestinationSelectionPhase();
    }

    public void SetCharacterToAttack(GameObject character)
    {
        string oldcharacter;
        switch (Character)
        {
            case null:
                oldcharacter = null;
                break;
            case not null:
                foreach (Transform child in Character.transform)
                {
                    child.gameObject.layer = 0;
                }

                oldcharacter = Character.name;
                break;
        }

        Character = character.GetComponent<CharacterMain>();
        foreach (Transform child in character.transform)
        {
            child.gameObject.layer = 7;
        }

        ManagerMain.mapMain.wayPointStart = Character.Position;
        Debug.Log($"Character changement: old character : {oldcharacter} and new character : {Character.name}");
        OnCharacterSelected?.Invoke(Character);
        EndCharacterSelectionPhase();
        TargetSelectionPhase();
    }

    public void SetTarget(GameObject target)
    {
        string oldtarget;
        switch (Target)
        {
            case null:
                oldtarget = null;
                break;
            case not null:
                oldtarget = Target.name;
                foreach (Transform child in Target.transform)
                {
                    child.gameObject.layer = 0;
                }

                break;
        }

        Target = target.GetComponent<MonsterMain>();
        foreach (Transform child in target.transform)
        {
            child.gameObject.layer = 6;
        }

        Debug.Log($"Target changement: old Target: {oldtarget} and new character : {target.name}");
        OnEnnemySelected?.Invoke(Target);
        EndTargetSelectionPhase();
    }

    public void SetAlly(GameObject ally)
    {
        Ally = ally.GetComponent<CharacterMain>();
        EndAllySelectionPhase();
    }

    public void SetDestination(WayPoint targetPosition)
    {
        Destination = targetPosition;
        Debug.Log($"Target Position changement, new position : {targetPosition.name}");
        DestinationSelection = false;
    }

    public void ResetVariables()
    {
        if (Character != null)
        {
            foreach (Transform child in Character.transform)
            {
                child.gameObject.layer = 0;
            }
        }

        if (Target != null)
        {
            foreach (Transform child in Target.transform)
            {
                child.gameObject.layer = 0;
            }
        }

        Character = null;
        Target = null;
        Destination = null;
        Ally = null;

        _turnText.text = string.Empty;
    }

    public void EndTurn()
    {
        ResetVariables();

        EndCharacterSelectionPhase();
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();

        Turnindex++;
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

    private void Start()
    {
        OnPlayerTurn?.Invoke();
    }

}
