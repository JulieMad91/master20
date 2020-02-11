using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net.Sockets;


public class UdpComm : MonoBehaviour
{

    public byte[] txBuffer;
    public int nSend;

    private UdpClient udp;



    // Start is called before the first frame update
    void Start()
    {
        udp = new UdpClient(5678);
        
        

        txBuffer = new byte[4];
    }

    // Update is called once per frame
    void Update()
    {
        
        udp.Send(txBuffer, txBuffer.Length);

        nSend = nSend + 1;

    }
}
