using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
	PhotonView PV;

	GameObject controller;
	public Vector3 color;
	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}

	void Start()
	{
 
		if (PV.IsMine)
		{
			CreateController();
		}
	}


	 

	void CreateController()
	{
		Transform spawnpoint=this.gameObject.transform;
		controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero+(Random.Range(-2,2)*Vector3.forward), spawnpoint.rotation);
		 
	}

}