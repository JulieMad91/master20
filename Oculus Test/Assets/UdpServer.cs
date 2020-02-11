using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine;

public class UdpServer : MonoBehaviour {


    public int nRecv = 0;
    public int nSend = 0;

    public int rxSize = 0;
    public int txSize = 0;
    public byte[] rxBuffer;
    public byte[] txBuffer;

    private IPEndPoint remoteEP;

    // UDP socket
    UdpClient socket;


    // Use this for initialization
    void Start () {
        // Start async UDP connection

        socket = new UdpClient(5500);
        socket.BeginReceive(new AsyncCallback(OnUdpData), socket);

    }


    void Update()
    {
        


    }

    void OnUdpData(IAsyncResult result)
    {
        // this is what had been passed into BeginReceive as the second parameter:
        UdpClient socket = result.AsyncState as UdpClient;

        // Recieve data from remote source
        rxBuffer = socket.EndReceive(result, ref remoteEP);


        // Echo data back to remote
        socket.Send(rxBuffer, rxBuffer.Length, remoteEP);

        nSend = nSend + 1;

        // Schedule the next receive operation once reading is done:
        socket.BeginReceive(new AsyncCallback(OnUdpData), socket);

        // Increment counter
        nRecv = nRecv + 1;
    }

    void OnApplicationQuit()
    {
        // Close UDP connection
        socket.Close();
    }
}

