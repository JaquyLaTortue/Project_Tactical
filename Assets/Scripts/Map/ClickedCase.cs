using System.Collections;
using UnityEngine;

public class ClickedCase : MonoBehaviour
{
    private MapMain _mapMain;

    public void Init(MapMain mp)
    {
        _mapMain = mp;
        StartCoroutine(DelayToStopError());
    }

    [SerializeField]
    private Material _red;

    [SerializeField]
    private Material _old;
    /// <summary>
    /// Cette fonction ne sert que pour les tests de début de projet, on est sensé avoir un input général qui n'est n'inclus directement MapMain.
    /// </summary>
    public void OnClickOnCase()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000))
        {
            ChangeColorOfWaypointToRed(hit.collider.GetComponent<MeshRenderer>());
            _mapMain.Test(hit.collider.GetComponent<WayPoint>());
            //Debug.Log(_hit.collider.gameObject.name);
        }
    }

    public void ChangeColorOfWaypointToRed(MeshRenderer waypointToColor)
    {
        waypointToColor.material = _red;
    }

    public void ChangeColorOfWaypointToOld(MeshRenderer waypointToColor)
    {
        waypointToColor.material = _old;
    }

    IEnumerator DelayToStopError()
    {
        yield return new WaitForSeconds(0.1f);
        ChangeColorOfWaypointToRed(_mapMain.wayPointStart.GetComponent<MeshRenderer>());
    }
}
