using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public GameObject[] points;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].transform.position);
            points[i].GetComponent<MeshRenderer>().enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].transform.position);
        }
    }

    public void DisableCollision()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].GetComponent<Collider>().enabled = false;
        }
    }
}
