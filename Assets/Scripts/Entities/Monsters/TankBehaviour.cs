using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TankBehaviour : MonsterBehaviour
{
    /// <summary>
    /// Est appelé avec l'event dans le turnManager pour faire les actions du monstre.
    /// </summary>
    private void Start()
    {
        _turnManager.OnMonsterTurn += DetectePlayer;
    }

    /// <summary>
    /// Permet de faire les actions du monstre en lui indiquant de se déplacer ou d'attaquer suivant la distance avec le joueur qui lui est le plus proche.
    /// Si il se déplace il se déplacera vers un autre monstre si il n'est pas à portée du joueur.
    /// </summary>
    private void DetectePlayer()
    {
        _monsterMain.MonsterCapacity.HasAttacked = false;
        _monsterMain.MonsterCapacity.HasMoved = false;
        _monsterMain.MonsterCapacity._hasSpecial = false;
        _monsterMain.PaCurrent = _monsterMain.PaMax;

        int distance = 1000;
        List<WayPoint> path = new List<WayPoint>();
        for (int i = 0; i < _entitiesManager.allCharacters.Count; i++)
        {
            path = _monsterMain.MonsterCapacity._mapMain.UseAStar(_monsterMain.Position, _entitiesManager.allCharacters[i].Position);
            if (distance < path.Count)
            {
                distance = path.Count;
                _targetPlayer = _entitiesManager.allCharacters[i];
            }
        }

        if (path.Count <= _monsterMain.Range)
        {
            _monsterMain.MonsterCapacity.Attack(_targetPlayer);
        }
        else
        {
            List<MonsterMain> otherMonsters = _entitiesManager.allMonsters.Except(new List<MonsterMain>() { _monsterMain }).ToList();
            if (otherMonsters.Count > 0)
            {
                MonsterMain monsterAlly = otherMonsters[Random.Range(0, otherMonsters.Count)];
                _monsterMain.MonsterCapacity.Move(monsterAlly.Position.neighbour[0]);
            }
        }
    }
}
