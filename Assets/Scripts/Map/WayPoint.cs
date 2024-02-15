using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public int[] casePosition;
    public List<WayPoint> neighbour = new List<WayPoint>();
    public bool obstacle;
    public WayPoint parent;
    public int caseCost;
    public MeshRenderer meshRenderer;
    public Entity entity;
    public SpriteRenderer showPath;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Color alpha = showPath.color;
        alpha.a = 0;
        showPath.color = alpha;
    }

    private void OnMouseOver()
    {
        //Debug.Log(gameObject.name);
    }
}
