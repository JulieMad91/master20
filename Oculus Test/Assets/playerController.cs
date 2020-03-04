using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float runSpeed = 10f;
    public float overallSpeed;
    //private float horizontal;
    //private float vertical;
    public float gravity = 0f;
    private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
       controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= runSpeed;
            //Debug.Log(moveDirection);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        overallSpeed = controller.velocity.magnitude;


        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");

        //speed = rb.velocity.magnitude;

        //rb.velocity = new Vector3(horizontal * runSpeed, rb.velocity.y, vertical * runSpeed);

    }

    private void Update()
    {
        if (Input.anyKey)
        {
            Debug.Log(Input.inputString);
        }
    }
}
