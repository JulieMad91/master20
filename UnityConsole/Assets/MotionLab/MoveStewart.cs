using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStewart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Connect to parent UdpServer
        UdpServer udp = this.gameObject.GetComponentInParent<UdpServer>();

        // Transform based on udp data
        this.transform.position = new Vector3(0, udp.rxData.heave, 0);

        this.transform.eulerAngles = new Vector3(
            -udp.rxData.roll/Mathf.PI*180,
            0,
            -udp.rxData.pitch/Mathf.PI*180
        );
    }
}
