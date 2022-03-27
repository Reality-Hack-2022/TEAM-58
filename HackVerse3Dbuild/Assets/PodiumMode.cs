using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PodiumMode : MonoBehaviourPun
{
    public Transform largepos;
    bool isEnlarged;
    public GameObject respawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    public GameObject A;
    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)

        {  
              A = other.gameObject;
            A.GetComponent<Collider>().enabled = false;
            A.transform.position = new Vector3(-377, -280, -140);

            A.transform.eulerAngles = new Vector3(0, 63, 0);
            A.transform.localScale = new Vector3(236, 236, 236);
            A.SetActive(true);
            A.GetComponent<Collider>().enabled = true;

            isEnlarged = true;
        }
    }

     
}
