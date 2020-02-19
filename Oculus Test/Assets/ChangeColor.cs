using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public UdpServer udp;

    public int n, k;
    // Start is called before the first frame update
    void Start()
    {
        n = 0;
        k = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (udp.nRecv != n)
        {
            n = udp.nRecv;

            switch (k)
            {
                case 0:
                    GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    k = 1;

                    break;

                case 1:
                    GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    k = 2;

                    break;

                case 2:
                    GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    k = 0;
                
                    break;

                default:
                    break;
            }
            
        }

    }
}
