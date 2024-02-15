using UnityEngine;

public class LineTeleportation : MonoBehaviour
{
    LineRenderer _line;
    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        Vector3[] positions = new Vector3[3];
        positions[0] = new Vector3(-2.0f, -2.0f, 0.0f);
        positions[1] = new Vector3(0.0f, 2.0f, 0.0f);
        positions[2] = new Vector3(2.0f, -2.0f, 0.0f);
        _line.positionCount = positions.Length;
        _line.SetPositions(positions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
