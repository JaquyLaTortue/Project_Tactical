using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class used to manage the turns phases & variables.
/// </summary>
public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _turnText;

    public event Action OnPlayerTurn;

    public event Action OnMonsterTurn;

    public event Action<CharacterMain> OnCharacterSelected;

    public event Action<MonsterMain> OnEnnemySelected;

    public ManagerMain ManagerMain { get; set; }

    public int Turnindex { get; private set; } = 0;

    public bool PlayerTurn { get; private set; } = true;

    public CharacterMain Character { get; set; }

    public MonsterMain Target { get; set; }

    public CharacterMain Ally { get; private set; }

    public WayPoint Destination { get; private set; }

    [field: SerializeField]
    public PlayerInput InputManager { get; private set; }

    // bool to start the character selection in order to move then
    public bool CharacterSelectionToMove { get; private set; } = false;

    // bool to start the character selection in order to attack then
    public bool CharacterSelectionToAttack { get; private set; } = false;

    // bool to start the character selection in order to use the special then
    public bool CharacterSelection { get; private set; } = false;

    // bool to start the target selection in order to attack or use the special
    public bool TargetSelection { get; private set; } = false;

    // bool to start the destination selection in order to move
    public bool DestinationSelection { get; private set; } = false;

    // bool to start the ally selection for the heal special
    public bool AllySelection { get; private set; } = false;

    public void InitManager(ManagerMain mM)
    {
        mM.turnManager = this;
        ManagerMain = mM;
    }

    // Start the character selection in order to move then & end all the other phases
    public void CharacterSelectionToMovePhase()
    {
        ResetVariables();
        CharacterSelectionToMove = true;
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();
        SetUIText("Select a character");
    }

    // Start the character selection in order to attack then & end all the other phases
    public void CharacterSelectionToAttackPhase()
    {
        ResetVariables();
        CharacterSelectionToAttack = true;
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();
        SetUIText("Select a character");
    }

    // Start the character selection in order to use the special then & end all the other phases
    public void CharacterSpecialSelectionPhase()
    {
        ResetVariables();
        CharacterSelection = true;
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();
        SetUIText("Select a character");
    }

    // End the character selection phase & reset the variables and the UI text
    public void EndCharacterSelectionPhase()
    {
        CharacterSelectionToAttack = false;
        CharacterSelectionToMove = false;
        CharacterSelection = false;
        SetUIText(string.Empty);
    }

    // Start the target selection in order to attack or use the special then & end all the other phases
    public void TargetSelectionPhase()
    {
        TargetSelection = true;
        EndCharacterSelectionPhase();
        EndDestinationSelectionPhase();
        EndAllySelectionPhase();
        _turnText.text = "Select a target";
    }

    // End the target selection phase & reset the variables and the UI text
    public void EndTargetSelectionPhase()
    {
        TargetSelection = false;
        SetUIText(string.Empty);
    }

    // Start the destination selection in order to move then & end all the other phases
    public void DestinationSelectionPhase()
    {
        DestinationSelection = true;
        EndCharacterSelectionPhase();
        EndTargetSelectionPhase();
        EndAllySelectionPhase();
        _turnText.text = "Select a destination";
    }

    // End the destination selection phase & reset the variables and the UI text
    public void EndDestinationSelectionPhase()
    {
        DestinationSelection = false;
        SetUIText(string.Empty);
    }

    // Start the ally selection for the heal special then & end all the other phases
    public void AllySelectionPhase()
    {
        AllySelection = true;
        EndCharacterSelectionPhase();
        EndTargetSelectionPhase();
        EndDestinationSelectionPhase();
        _turnText.text = "Select an ally";
    }

    // End the ally selection phase & reset the variables and the UI text
    public void EndAllySelectionPhase()
    {
        AllySelection = false;
        SetUIText(string.Empty);
    }

    /// <summary>
    /// Set the character selected and call the character selection event & then start the destination selection or the target selection depending on wich bool is true.
    /// </summary>
    /// <param name="character">Will be used as the selected character.</param>
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

    /// <summary>
    /// Set the character selected and call the target selection event.
    /// </summary>
    /// <param name="target">Will be used as the selected Target.</param>
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

        OnEnnemySelected?.Invoke(Target);
        EndTargetSelectionPhase();
    }

    /// <summary>
    /// Set the ally selected.
    /// </summary>
    /// <param name="ally">Will be used as the selected ally</param>
    public void SetAlly(GameObject ally)
    {
        Ally = ally.GetComponent<CharacterMain>();
        EndAllySelectionPhase();
        SetUIText("You can Cast the Special");
    }

    /// <summary>
    /// Set the destination selected.
    /// </summary>
    /// <param name="targetPosition">The given waypoint will be used as the selected destination.</param>
    public void SetDestination(WayPoint targetPosition)
    {
        Destination = targetPosition;
        DestinationSelection = false;
    }

    /// <summary>
    /// Switch on the phase that is needed to cast the special.
    /// </summary>
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

    /// <summary>
    /// Reset all the variables, the UI text & take off the outline of the character and the target.
    /// </summary>
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
    /// <summary>
    /// Used to set the UI text.
    /// </summary>
    /// <param name="desiredText">The given string will be used to fill the UI text.</param>
    public void SetUIText(string desiredText)
    {
        _turnText.text = desiredText;
    }

    /// <summary>
    /// End the turn and reset all the variables and the UI text.
    /// </summary>
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

    /// <summary>
    /// Determine wich turn is next (Player or Monster).
    /// </summary>
    private void DetermineTurn()
    {
        switch (Turnindex % 2)
        {
            case 0:
                PlayerTurn = true;
                OnPlayerTurn?.Invoke();
                break;
            case 1:
                PlayerTurn = false;
                OnMonsterTurn?.Invoke();
                break;
        }
    }

    private void Start()
    {
        // Set the first turn to the player's turn
        OnPlayerTurn?.Invoke();
    }
}
