﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{

    public event Action OnPlayerTurn;

    public event Action OnMonsterTurn;

    public event Action<CharacterMain> OnCharacterSelected;

    public event Action<MonsterMain> OnEnnemySelected;

    public ManagerMain ManagerMain { get; set; }

    public int Turnindex { get; private set; } = 0;

    public bool PlayerTurn { get; private set; } = true;

    [field: SerializeField]
    public CharacterMain Character { get; private set; }

    public MonsterMain Target { get; private set; }

    public WayPoint Destination { get; private set; }

    [field: SerializeField]
    public PlayerInput InputManager { get; private set; }

    public bool CharacterSelection { get; private set; } = false;

    public bool TargetSelection { get; private set; } = false;

    public bool DestinationSelection { get; private set; } = false;

    public void InitManager(ManagerMain MM)
    {
        MM.turnManager = this;
        ManagerMain = MM;
    }

    public void CharacterSelectionPhase()
    {
        CharacterSelection = true;
    }

    public void EndCharacterSelectionPhase()
    {
        CharacterSelection = false;
    }

    public void TargetSelectionPhase()
    {
        TargetSelection = true;
    }

    public void EndTargetSelectionPhase()
    {
        TargetSelection = false;
    }

    public void DestinationSelectionPhase()
    {
        DestinationSelection = true;
    }

    public void EndDestinationSelectionPhase()
    {
        DestinationSelection = false;
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
                Character.gameObject.layer = 0;
                oldcharacter = Character.name;
                break;
        }

        Character = character.GetComponent<CharacterMain>();
        Character.gameObject.layer = 7;
        ManagerMain.mapMain.wayPointStart = Character.Position;
        Debug.Log($"Character changement: old character : {oldcharacter} and new character : {Character.name}");
        OnCharacterSelected?.Invoke(Character);
        CharacterSelection = false;
        DestinationSelection = true;
    }

    public void SetCharacter1(GameObject character)
    {
        string oldcharacter = Character == null ? "null" : Character.name;
        Character = character.GetComponent<CharacterMain>();
        ManagerMain.mapMain.wayPointStart = Character.Position;
        Debug.Log($"Character changement: old character : {oldcharacter} and new character : {Character.name}");
        OnCharacterSelected?.Invoke(Character);
        CharacterSelection = false;
        TargetSelection = true;
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
                Target.gameObject.layer = 0;
                break;
        }

        Target = target.GetComponent<MonsterMain>();
        Target.gameObject.layer = 6;
        Debug.Log($"Target changement: old Target: {oldtarget} and new character : {target.name}");
        OnEnnemySelected?.Invoke(Target);
        TargetSelection = false;
    }

    public void SetDestination(WayPoint targetPosition)
    {
        Destination = targetPosition;
        Debug.Log($"Target Position changement, new position : {targetPosition.name}");
        DestinationSelection = false;
    }

    public void EndTurn()
    {
        Character = null;
        Target = null;
        Destination = null;

        CharacterSelection = false;
        TargetSelection = false;
        DestinationSelection = false;

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
        CharacterSelection = true;
    }

}
