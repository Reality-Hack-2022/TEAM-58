using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    PhotonView PV;
    public GameObject playerFollow;
    public GameObject holder;
    public GameObject cam;
    public GameObject root;
    public StarterAssets.FirstPersonController FPSscript;
    public AvatarManager avatarmanager;
    public bool IsSelected => throw new System.NotImplementedException();

    // Start is called before the first frame update
    void Start()
    {
        PV = photonView;
        if (!PV.IsMine)
        {
            this.GetComponentInChildren<Camera>().enabled = false;
            this.GetComponentInChildren<AudioListener>().enabled = false;
            Destroy(this.GetComponentInChildren<StarterAssets.FirstPersonController>());
            //parent.GetComponentInChildren<StarterAssets.FirstPersonController>().enabled = false;
            Destroy(playerFollow.gameObject);
        }
    }
    private void Awake()
    {

        
        

    }
    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
    }

  
}
