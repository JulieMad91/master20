using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float runSpeed = 5f;
    public float speed;
    private float horizontal;
    private float vertical;

    public float h;
    public Vector3 vec;

    public Vector3 terrainSize;

    private Terrain terrain;

    private Rigidbody rb;
    private playerController playerControl;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerControl = GetComponent<playerController>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        h = rb.transform.position.y;
        vec = transform.up;

        // Read parent terrain
        terrain = transform.GetComponentInParent<Terrain>();

        terrainSize = terrain.terrainData.size;

        // Normalized capsule location relative to terrain local coordinate
        float rx = transform.position.x / terrainSize.x;
        float rz = transform.position.z / terrainSize.z;

        // Normal vector from terain directly under capsule
        vec = terrain.terrainData.GetInterpolatedNormal(rx, rz);


        //rb.velocity = new Vector3(horizontal * runSpeed * Time.deltaTime, rb.velocity.y, vertical * runSpeed * Time.deltaTime);
        transform.position = transform.position + new Vector3(horizontal * runSpeed * Time.deltaTime, 0, vertical * runSpeed * Time.deltaTime);
        //speed = rb.velocity.magnitude;
    }
}
