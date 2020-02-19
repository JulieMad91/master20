using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public struct TxData
{
    public int i;
    public float f;
    public double d;
    public bool b;
}

[System.Serializable]
public struct RxData
{
    public float heave;
    public float roll;
    public float pitch;
}

public class UdpServer : MonoBehaviour {
    // UDP data
    public string remoteIp = "127.0.0.1";
    public int remotePort = 25000;
    public string localIp = "127.0.0.1";
    public int localPort = 25001;

    
    public TxData txData;
    public RxData rxData;

    public int nRecv = 0;
    public int nSend = 0;

    public int rxSize = 0;
    public int txSize = 0;
    public byte[] rxBuffer;
    public byte[] txBuffer;

    private IPEndPoint localEP, remoteEP;

    // UDP socket
    UdpClient socket;


    // Use this for initialization
    void Start () {
        // Start async UDP connection
        localEP = new IPEndPoint(IPAddress.Parse(localIp), localPort);
        remoteEP = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);

        socket = new UdpClient(localEP);
        socket.BeginReceive(new AsyncCallback(OnUdpData), socket);

        txBuffer = new byte[17];
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
        rxSize = rxBuffer.Length;

        //// Decode message
        MemoryStream memoryStreamRead = new MemoryStream(rxBuffer);
        BinaryReader binaryReader = new BinaryReader(memoryStreamRead);

        rxData.heave = binaryReader.ReadSingle();
        rxData.roll = binaryReader.ReadSingle();
        rxData.pitch = binaryReader.ReadSingle();


        // Send data to remote soruce
        MemoryStream memoryStreamWrite = new MemoryStream();

        BinaryWriter binaryWriter = new BinaryWriter(memoryStreamWrite);

        binaryWriter.Write(txData.i);
        binaryWriter.Write(txData.f);
        binaryWriter.Write(txData.d);
        binaryWriter.Write(txData.b);

        txBuffer = memoryStreamWrite.ToArray();

        txSize = txBuffer.Length;


        socket.Send(txBuffer, txBuffer.Length, remoteEP);

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

