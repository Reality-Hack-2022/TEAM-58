using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class NetworkPlayer : MonoBehaviourPun
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    private Transform xrRig;
    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;
    private XROrigin rig;

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
        if(photonView.IsMine)
        {
            localPlayerInstance = this.gameObject;
            MapPosition(localPlayerInstance.transform, xrRig);
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);
        }
        
    }

    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
