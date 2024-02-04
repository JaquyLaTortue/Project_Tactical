using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public int[] casePosition;
    public List<WayPoint> neighbour = new List<WayPoint>();
    public bool obstacle;
    public WayPoint parent;
    public int caseCost;
}
