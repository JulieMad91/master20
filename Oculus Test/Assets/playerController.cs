using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float runSpeed = 5f;
    public float speed;
    private float horizontal;
    private float vertical;
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

        speed = rb.velocity.magnitude;

        rb.velocity = new Vector3(horizontal * runSpeed, rb.velocity.y, vertical * runSpeed);
    }
}
