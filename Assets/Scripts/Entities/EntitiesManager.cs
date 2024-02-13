using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    private ManagerMain managerMain;

    public List<MonsterMain> allMonsters = new List<MonsterMain>();
    public List<CharacterMain> allCharacters = new List<CharacterMain>();

    [SerializeField]
    List<GameObject> charactersPrefabs = new List<GameObject>();

    [SerializeField]
    List<GameObject> monstersPrefabs = new List<GameObject>();

    public void InitManager(ManagerMain MM)
    {
        MM.entitiesManager = this;
        managerMain = MM;
    }

    public void InitEntities()
    {
        CreateEntities();
    }

    private void CreateEntities()
    {
        // Chara part.
        foreach (GameObject obj in charactersPrefabs)
        {
            GameObject newObj = Instantiate(obj);
            CharacterMain newCharacter = newObj.GetComponent<CharacterMain>();
            newCharacter.InitCharacter(managerMain.mapMain);
            allCharacters.Add(newCharacter);
        }

        List<WayPoint> wayPointsToSpawn = new List<WayPoint>();
        wayPointsToSpawn = SearchWaypointToSpawn(true);

        foreach (CharacterMain chara in allCharacters)
        {
            int waypointRandom = Random.Range(0, wayPointsToSpawn.Count);
            chara.CharacterCapacity.ChangeWaypoint(wayPointsToSpawn[waypointRandom]);
            wayPointsToSpawn[waypointRandom].obstacle = true;
            wayPointsToSpawn.Remove(wayPointsToSpawn[waypointRandom]);
        }

        // Monster part.
        foreach (GameObject obj in monstersPrefabs)
        {
            GameObject newObj = Instantiate(obj);
            MonsterMain newMonster = newObj.GetComponent<MonsterMain>();
            newMonster.InitMonster(managerMain.mapMain);
            MonsterBehaviour monsterBehaviour = newObj.GetComponent<MonsterBehaviour>();
            monsterBehaviour.InitBehaviour(managerMain);
            allMonsters.Add(newMonster);
        }

        wayPointsToSpawn = SearchWaypointToSpawn(false);

        foreach (MonsterMain monster in allMonsters)
        {
            int waypointRandom = Random.Range(0, wayPointsToSpawn.Count);
            monster.MonsterCapacity.ChangeWaypoint(wayPointsToSpawn[waypointRandom]);
            wayPointsToSpawn[waypointRandom].obstacle = true;
            wayPointsToSpawn.Remove(wayPointsToSpawn[waypointRandom]);
        }
    }

    private List<WayPoint> SearchWaypointToSpawn(bool isCharacter)
    {
        List<WayPoint> wayPointsToSpawn = new List<WayPoint>();
        foreach (WayPoint wayPoint in managerMain.mapMain.config.allWayPoints)
        {
            Debug.Log(wayPoint.casePosition[0]);
            if (isCharacter)
            {
                if (wayPoint.casePosition[0] <= 1)
                {
                    wayPointsToSpawn.Add(wayPoint);
                }
            }
            else
            {
                if (wayPoint.casePosition[0] >= managerMain.mapMain.config.LengthCase - 2)
                {
                    wayPointsToSpawn.Add(wayPoint);
                }
            }
        }

        return wayPointsToSpawn;
    }
}
