using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainObserver : MonoBehaviour
{
    public float checkDistance = 2;
    public float checkRadius = 0.5f;
    public int nChecks = 10;
    public int nHits = 0;
    private Vector3[] origin, direction;

    private RaycastHit[] hits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Contruct raycast hit array
        hits = new RaycastHit[nChecks];
        origin = new Vector3[nChecks];
        direction = new Vector3[nChecks];

        Vector3 v = checkRadius*Vector3.forward;

        // Reset hit count
        nHits = 0;
        for (int i = 0; i < nChecks; i++)
        {
            // Raycast check origin in global space
            origin[i] = transform.position + transform.TransformDirection(
                0.5f * Vector3.down + Quaternion.Euler(0, (float)i / nChecks * 360 , 0) * v
            );

            // Raycast check direction in global space
            direction[i] = transform.TransformDirection(Vector3.down);

            // Cast ray
            if (Physics.Raycast(origin[i], direction[i], out hits[i], checkDistance))
            {
                // Count number of hits
                nHits = nHits + 1;

                // Draw result
                Debug.DrawRay(origin[i], direction[i] * hits[i].distance, Color.red);

            }
        }



    }
}
