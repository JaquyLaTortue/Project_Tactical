using System.Collections.Generic;
using UnityEngine;

public class TankBehaviour : MonsterBehaviour
{
    private void Start()
    {
        _turnManager.OnMonsterTurn += DetectePlayer;
    }

    private void DetectePlayer()
    {
        _monsterMain.MonsterCapacity.HasAttacked = false;
        _monsterMain.MonsterCapacity.HasMoved = false;
        _monsterMain.PaCurrent = _monsterMain.PaMax;

        int distance = 1000;
        List<WayPoint> path = new List<WayPoint>();
        for (int i = 0; i < _entitiesManager.allCharacters.Count; i++)
        {
            path = _monsterMain.MonsterCapacity._mapMain.aStar.GiveThePath(_monsterMain.Position, _entitiesManager.allCharacters[i].Position);
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
            MonsterMain monsterAlly = _entitiesManager.allMonsters[Random.Range(0, _entitiesManager.allMonsters.Count)];
            _monsterMain.MonsterCapacity.Move(monsterAlly.Position);
        }
    }
}
