using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics.LinearAlgebra.Factorization;


public class TerrainObserver : MonoBehaviour
{
    public float h;
    public float rotX;
    public float rotZ;

    public Vector3 nVector;
    public float checkDistance = 2;
    public float checkRadius = 0.5f;
    public int nChecks = 20;
    public int nHits = 0;
 

    private Vector3[] origin, direction;

    private RaycastHit[] hits;

    private Matrix<float> A, N, V;



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

        // Terrain heigth
        h = (transform.position + transform.TransformDirection(Vector3.down * 1.08f)).y;

        Vector3 v = checkRadius*Vector3.forward;

        // Find plane with Raycast
        http://thehiddensignal.com/unity-angle-of-sloped-ground-under-player/

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

        // Estimate plane based on 3 or more hitpoints
        if (nHits >= 3)
        {
            A = Matrix<float>.Build.Dense(nHits, 4);

            // Fill data
            for (int i = 0; i < nHits; i++)
            {
                // Calibration of the Norwegian motion laboratory using conformal geometric algebra, Olav Heng med ml.
                // https://dl.acm.org/doi/abs/10.1145/3095140.3097285?casa_token=a4BDviqGwywAAAAA:Xa7wPapL0sY-fAxfMJvYaKG7JTrUP5AlYQvHikOBizZa2iLkgZfVGdlpZNAlrXSJPqbIaIzEeByi
                // https://github.com/sondre1988/matlab-functions/blob/master/src/PlaneFitCGA.m

                // Hit point in global space
                Vector3 hitPoint = origin[i] + direction[i] * hits[i].distance;

                // Stack hit points
                A[i, 0] = hitPoint.x;
                A[i, 1] = hitPoint.y;
                A[i, 2] = hitPoint.z;
                A[i, 3] = -1;

                // Solve using Eigenvalue decomposition
                N = A.Transpose() * A;
                Evd<float> evd = N.Evd();

                V = evd.EigenVectors;

                // Get normal vector
                Vector<float> sol = V.Column(0);

                // Fix signs
                if (sol[1] < 0)
                {
                    sol = -sol;
    
                }

                nVector.x = sol[0];
                nVector.y = sol[1];
                nVector.z = sol[2];

                nVector.Normalize();

                // Calculate angles of plane relative to world
                rotX = Mathf.Rad2Deg * Mathf.Atan2(nVector.z, nVector.y);
                rotZ = Mathf.Rad2Deg * Mathf.Atan2(nVector.x, nVector.y);


            }
        }



    }
}
