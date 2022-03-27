using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using TMPro;

public class NetworkPlayer : MonoBehaviourPun
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    [SerializeField] private TextMeshPro playerNameText;

    private Transform xrRig;
    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;
    private XROrigin rig;
    public Transform torso; 

    private GameObject localPlayerInstance = null;

    private void Awake()
    {
        rig = FindObjectOfType<XROrigin>();
    }

    void Start()
    {
        if(rig != null)
        {
            xrRig = rig.transform;
            headRig = rig.transform.Find("Camera Offset/Main Camera");
            leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
            rightHandRig = rig.transform.Find("Camera Offset/RightHand Controller");
        }
    }

    void Update()
    {
        playerNameText.text = this.photonView.Owner.NickName;
        if (photonView.IsMine)
        {
            leftHand.GetChild(0).gameObject.SetActive(false);
            rightHand.GetChild(0).gameObject.SetActive(false);
            localPlayerInstance = this.gameObject;
            MapPosition(localPlayerInstance.transform, xrRig);
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);
            torso.transform.position = new Vector3(headRig.transform.position.x, headRig.transform.position.y - 0.27f,
            headRig.transform.position.z);
        }
        
    }

    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
