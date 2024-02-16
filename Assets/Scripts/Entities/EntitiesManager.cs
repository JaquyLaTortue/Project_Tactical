using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    private ManagerMain managerMain;

    public bool ManualSceneChangement = false;

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

    public void RemoveEntity(Entity entity)
    {
        if (ManualSceneChangement)
        {
            return;
        }
        if (entity is CharacterMain)
        {
            allCharacters.Remove((CharacterMain)entity);
            if (allCharacters.Count == 0)
            {
                managerMain.uiManager.Lose();
            }
        }
        else if (entity is MonsterMain)
        {
            allMonsters.Remove((MonsterMain)entity);
            if (allMonsters.Count == 0)
            {
                managerMain.uiManager.Win();
            }
        }
    }

    private void CreateEntities()
    {
        // Chara part.
        for (int i = 0; i < charactersPrefabs.Count; i++)
        {
            GameObject newObj = Instantiate(charactersPrefabs[i]);
            CharacterMain newCharacter = newObj.GetComponent<CharacterMain>();
            newCharacter.InitCharacter(managerMain, managerMain.uiManager._list[i]);
            allCharacters.Add(newCharacter);
        }

        List<WayPoint> wayPointsToSpawn = new List<WayPoint>();
        wayPointsToSpawn = SearchWaypointToSpawn(true);

        foreach (CharacterMain chara in allCharacters)
        {
            int waypointRandom = Random.Range(0, wayPointsToSpawn.Count);
            float spawnHighness = wayPointsToSpawn[waypointRandom].GetComponent<BoxCollider>().size.y / 2;
            chara.CharacterCapacity.ChangeWaypoint(wayPointsToSpawn[waypointRandom]);
            chara.transform.position = new Vector3(chara.transform.position.x, chara.transform.position.y + spawnHighness, chara.transform.position.z);
            wayPointsToSpawn[waypointRandom].obstacle = true;
            wayPointsToSpawn[waypointRandom].entity = chara;
            wayPointsToSpawn.Remove(wayPointsToSpawn[waypointRandom]);
        }

        // Monster part.
        for (int i = 0; i < monstersPrefabs.Count; i++)
        {
            GameObject newObj = Instantiate(monstersPrefabs[i]);
            MonsterMain newMonster = newObj.GetComponent<MonsterMain>();
            newMonster.InitMonster(managerMain, managerMain.uiManager._monsterList[i]);
            MonsterBehaviour monsterBehaviour = newObj.GetComponent<MonsterBehaviour>();
            monsterBehaviour.InitBehaviour(managerMain);
            allMonsters.Add(newMonster);
        }

        wayPointsToSpawn = SearchWaypointToSpawn(false);

        foreach (MonsterMain monster in allMonsters)
        {
            int waypointRandom = Random.Range(0, wayPointsToSpawn.Count);
            float spawnHighness = wayPointsToSpawn[waypointRandom].GetComponent<BoxCollider>().size.y / 2;
            monster.MonsterCapacity.ChangeWaypoint(wayPointsToSpawn[waypointRandom]);
            monster.transform.position = new Vector3(monster.transform.position.x, monster.transform.position.y + spawnHighness, monster.transform.position.z);
            wayPointsToSpawn[waypointRandom].obstacle = true;
            wayPointsToSpawn[waypointRandom].entity = monster;
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
