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

    public bool CharacterSelection { get; private set; } = false;

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
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();
        SetUIText("Select a character");
    }

    public void CharacterSelectionToAttackPhase()
    {
        ResetVariables();
        CharacterSelectionToAttack = true;
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();
        SetUIText("Select a character");
    }

    public void CharacterSpecialSelectionPhase()
    {
        ResetVariables();
        CharacterSelection = true;
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();
        SetUIText("Select a character");
    }

    public void EndCharacterSelectionPhase()
    {
        CharacterSelectionToAttack = false;
        CharacterSelectionToMove = false;
        CharacterSelection = false;
        SetUIText(string.Empty);
    }

    public void TargetSelectionPhase()
    {
        TargetSelection = true;
        EndCharacterSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();
        _turnText.text = "Select a target";
    }

    public void EndTargetSelectionPhase()
    {
        TargetSelection = false;
        SetUIText(string.Empty);
    }

    public void DestinationSelectionPhase()
    {
        DestinationSelection = true;
        EndCharacterSelectionPhase();
        EndTargetSelectionPhase();
        EndAllySelectionPhase();
        _turnText.text = "Select a destination";
    }

    public void EndDestinationSelectionPhase()
    {
        DestinationSelection = false;
        SetUIText(string.Empty);
    }

    public void AllySelectionPhase()
    {
        AllySelection = true;
        EndCharacterSelectionPhase();
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
        _turnText.text = "Select an ally";
    }

    public void EndAllySelectionPhase()
    {
        AllySelection = false;
        SetUIText(string.Empty);
    }

    public void SetCharacter(GameObject character)
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
        if (CharacterSelectionToMove)
        {
            EndCharacterSelectionPhase();
            DestinationSelectionPhase();
        }
        else if (CharacterSelectionToAttack)
        {
            EndCharacterSelectionPhase();
            TargetSelectionPhase();
        }
        else
        {
            return;
        }
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
        SetUIText("You can Cast the Special");
    }

    public void SetDestination(WayPoint targetPosition)
    {
        Destination = targetPosition;
        Debug.Log($"Target Position changement, new position : {targetPosition.name}");
        DestinationSelection = false;
    }

    public void SetUpSpecial()
    {
        switch (Character.CharacterCapacity.Capacity.name)
        {
            case "Heal":
                AllySelectionPhase();
                break;
            case "Shield":
                SetUIText("You can Cast the Special");
                break;
            case "Ultimate Attack":
                TargetSelectionPhase();
                break;
            default:
                break;
        }
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

    public void SetUIText(string desiredText)
    {
        Debug.Log($"Setting UI Text: {desiredText}");
        _turnText.text = desiredText;
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
