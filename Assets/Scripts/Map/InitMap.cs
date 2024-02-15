using System.Collections.Generic;
using UnityEngine;

public class InitMap : MonoBehaviour
{
    [SerializeField]
    private int _length;

    [SerializeField]
    private int _width;

    [SerializeField]
    private List<GameObject> _prefabCase = new List<GameObject>();

    [SerializeField]
    private GameObject _borderPrefab;

    [SerializeField]
    private GameObject _corderPrefab;

    [SerializeField]
    private Transform caseTransformParent;

    public List<WayPoint> InitTheMap(int lengthCase, int widthCase)
    {
        BoxCollider collision = _prefabCase[0].GetComponent<BoxCollider>();
        float prefabWidth = collision.size.z;
        float prefabLength = collision.size.x;
        float totalSizeLength = prefabLength * lengthCase;
        Camera mainCam = Camera.main;
        float pourcentY = mainCam.transform.rotation.eulerAngles.x / 90;
        float pourcentZ = 1 - pourcentY;
        mainCam.transform.position = new Vector3((totalSizeLength / 2) + 1, (lengthCase + widthCase) * pourcentY * 2, (lengthCase + widthCase) * -pourcentZ);
        List<WayPoint> map = new List<WayPoint>();
        for (int i = 0; i < lengthCase; i++)
        {
            for (int j = 0; j < widthCase; j++)
            {
                float rotateY = 0;
                GameObject newGameObject = Instantiate(_prefabCase[Random.Range(0, _prefabCase.Count)]);
                newGameObject.transform.parent = caseTransformParent;
                switch (Random.Range(0, 4))
                {
                    case 0:
                        break;
                    case 1:
                        rotateY = 90;
                        break;
                    case 2:
                        rotateY = 180;
                        break;
                    case 3:
                        rotateY = 270;
                        break;
                }

                newGameObject.transform.Rotate(0, rotateY, 0);
                newGameObject.transform.position += new Vector3((i + 1) * prefabLength, 0, (j + 1) * prefabWidth);
                WayPoint newWaypoint = newGameObject.GetComponent<WayPoint>();
                newWaypoint.casePosition = new int[2] { i, j };
                newWaypoint.name = newWaypoint.casePosition[0] + " " + newWaypoint.casePosition[1];
                map.Add(newWaypoint);
                foreach (WayPoint wayPoint in map)
                {
                    FindNeighbour(newWaypoint, wayPoint);
                }
            }
        }

        InitBorder(lengthCase, widthCase, prefabLength, prefabWidth);
        return map;
    }

    public void Init(MapMain main)
    {
        main.init = this;
    }

    private void FindNeighbour(WayPoint newWaypoint, WayPoint potentialNeighbour)
    {
        if (((potentialNeighbour.casePosition[0] == newWaypoint.casePosition[0] - 1
            ||
            potentialNeighbour.casePosition[0] == newWaypoint.casePosition[0] + 1)
            &&
            potentialNeighbour.casePosition[1] == newWaypoint.casePosition[1])
            ||
            ((potentialNeighbour.casePosition[1] == newWaypoint.casePosition[1] - 1
            ||
            potentialNeighbour.casePosition[1] == newWaypoint.casePosition[1] + 1)
            &&
            potentialNeighbour.casePosition[0] == newWaypoint.casePosition[0]))
        {
            newWaypoint.neighbour.Add(potentialNeighbour);
            potentialNeighbour.neighbour.Add(newWaypoint);
        }
    }

    private void InitBorder(int lengthCase, int widthCase, float prefabLength, float prefabWidth)
    {
        float positionLength = 0;
        float positionWidth = 0;
        float rotateY = 0;
        for (int i = 0; i < 4; i++)
        {
            GameObject newCorner = Instantiate(_corderPrefab);
            switch (i)
            {
                case 0:
                    positionLength = -1;
                    positionWidth = -1;
                    rotateY = 180;
                    break;
                case 1:
                    positionLength = lengthCase;
                    positionWidth = -1;
                    rotateY = 90;
                    break;
                case 2:
                    positionLength = -1;
                    positionWidth = widthCase;
                    rotateY = 270;
                    break;
                case 3:
                    positionLength = lengthCase;
                    positionWidth = widthCase;
                    rotateY = 0;
                    break;
            }

            newCorner.transform.Rotate(0, rotateY, 0);
            newCorner.transform.position += new Vector3((positionLength + 1) * prefabLength, 0, (positionWidth + 1) * prefabWidth);
            newCorner.transform.parent = caseTransformParent;
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < lengthCase; j++)
            {
                GameObject newBorder = Instantiate(_borderPrefab);
                switch (i)
                {
                    case 0:
                        positionWidth = -1;
                        rotateY = 180;
                        break;
                    case 1:
                        positionWidth = widthCase;
                        rotateY = 0;
                        break;
                }

                newBorder.transform.Rotate(0, rotateY, 0);
                newBorder.transform.position += new Vector3((j + 1) * prefabLength, 0, (positionWidth + 1) * prefabWidth);
                newBorder.transform.parent = caseTransformParent;
            }

            for (int j = 0; j < widthCase; j++)
            {
                GameObject newBorder = Instantiate(_borderPrefab);
                switch (i)
                {
                    case 0:
                        positionLength = -1;
                        rotateY = 270;
                        break;
                    case 1:
                        positionLength = lengthCase;
                        rotateY = 90;
                        break;
                }

                newBorder.transform.Rotate(0, rotateY, 0);
                newBorder.transform.position += new Vector3((positionLength + 1) * prefabLength, 0, (j + 1) * prefabWidth);
                newBorder.transform.parent = caseTransformParent;
            }
        }

        /*
        for (int i = 0; i < lengthCase; i++)
        {
            for (int j = 0; j < widthCase; j++)
            {
                
            }
        }*/
    }
}
