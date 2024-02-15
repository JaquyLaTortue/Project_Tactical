using UnityEngine;

public class ClickedCase : MonoBehaviour
{
    private MapMain _mapMain;

    public void Init(MapMain mp)
    {
        _mapMain = mp;
    }

    [SerializeField]
    private Material _red;

    [SerializeField]
    private Material _blue;

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
            ChangeColorOfWaypointToRed(hit.collider.GetComponent<WayPoint>().showPath);
            _mapMain.Test(hit.collider.GetComponent<WayPoint>());
        }
    }

    public void ChangeColorOfWaypointToRed(SpriteRenderer showPath)
    {
        Color alpha = showPath.color;
        alpha.a = 1;
        showPath.color = alpha;
        showPath.color = Color.red;
    }

    public void ChangeColorOfWaypointToBlue(SpriteRenderer showPath)
    {
        Color alpha = showPath.color;
        alpha.a = 1;
        showPath.color = alpha;
        showPath.color = Color.blue;
    }

    public void ChangeColorOfWaypointToOld(SpriteRenderer showPath)
    {
        Color alpha = showPath.color;
        alpha.a = 0;
        showPath.color = alpha;
    }
}
