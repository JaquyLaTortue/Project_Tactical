using System.Collections.Generic;

public class RangeBehaviour : MonsterBehaviour
{
    private void Start()
    {
       _turnManager.OnMonsterTurn += DetectePlayer;
    }

    private void DetectePlayer()
    {
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
        if (_targetPlayer.Position.casePosition[0] == _monsterMain.Position.casePosition[0] + _monsterMain.Range)
        {
            if (_targetPlayer.Position.casePosition[1] == _monsterMain.Position.casePosition[1] + _monsterMain.Range)
            {
                _monsterMain.MonsterCapacity.Attack(_targetPlayer);
            }
        }
        else
        {
            _monsterMain.MonsterCapacity.Move(_targetPlayer.Position);
        }
    }
}
