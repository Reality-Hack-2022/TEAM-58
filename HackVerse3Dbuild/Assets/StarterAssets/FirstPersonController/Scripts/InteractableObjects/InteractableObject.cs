using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InteractableObject : MonoBehaviourPun, IHoverable
{
    public Rigidbody object_rb;
    public Outline outline_component;
    public bool IsSelected => throw new System.NotImplementedException();
    public GameObject looker;
    // Start is called before the first frame update
    void Start()
    {
        InitialiseRigidbody();
        InitaliseOutline();
        isAccessible = true;
        ToggleOutline(false);
    }
    public bool isAccessible;
    void InitaliseOutline()
    {
        if (this.GetComponent<Outline>())
        {
            outline_component = this.GetComponent<Outline>();
        }
        else
        {
            Debug.Log("Creating Outline for item");
            outline_component = this.gameObject.AddComponent<Outline>();
        }
    }
    void InitialiseRigidbody()
    {
        if (this.GetComponent<Rigidbody>())
        {
            object_rb = this.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.Log("Creating Rigidbody for item");
            object_rb = this.gameObject.AddComponent<Rigidbody>();
        }
    }

    public void PickObject(Transform holderReference)
    {
        object_rb.isKinematic = true;
        this.gameObject.transform.parent = holderReference;
        this.gameObject.transform.position = holderReference.gameObject.transform.position;
        this.gameObject.transform.rotation = holderReference.gameObject.transform.rotation;
        print("Object:" + this.gameObject.name + " Picked Up");
    }

    public void ToggleOutline(bool state)
    {
        outline_component.enabled = state;
    }

    public void DropObject()
    {
        object_rb.isKinematic = false;
         this.gameObject.transform.parent = null;
        print("Object:" + this.gameObject.name + " Dropped");
    }
    // Update is called once per frame



    void Update()
    {

    }
    public void OnGazeEnter(GazeData data)
    {

        ToggleOutline(true);

        //    GazeObject.SetGazeState(GazeStates.Clickable);
    }

    public void OnGazeExit(GazeData data)
    {

        ToggleOutline(false);
        //   GazeObject.GazeDefault();
     //   DropObject();
      //  isPickedUp = false;


    }

    public bool isPickedUp;
    public Transform holder;
    int viewId;
    public void OnClick(GazeData data)
    {

        viewId = looker.GetComponent<PhotonView>().ViewID;
        if (!isPickedUp)
        {
            
            //    GazeObject.SetGazeState(GazeStates.Movable);

            PickObject(looker.GetComponent<PlayerController>().holder.transform);
            isPickedUp = true;
             this.photonView.RPC("objectAllign", RpcTarget.All, isPickedUp, viewId);

        }
        else
        {
            DropObject();
            isPickedUp = false;
             this.photonView.RPC("objectAllign", RpcTarget.All, isPickedUp, viewId);

            // this.photonView.RPC("objectAllign", RpcTarget.All, isPickedUp, viewId);


        }

    }


    [PunRPC]
    void objectAllign(bool flag, int viewId)
    {

       // Debug.LogError("this is " + viewId);
        if (flag)
        {
            isPickedUp = true;
            object_rb.isKinematic = true;
             if (!PhotonView.Find(viewId).IsMine)
            {
                this.gameObject.GetComponent<Collider>().enabled = false;
            }
            this.gameObject.transform.parent = PhotonView.Find(viewId).GetComponent<PlayerController>().holder.transform;
            this.gameObject.transform.position = PhotonView.Find(viewId).GetComponent<PlayerController>().holder.transform.position;
            this.gameObject.transform.rotation = PhotonView.Find(viewId).GetComponent<PlayerController>().holder.transform.rotation;
        }

        else
        {
            isPickedUp = false;
            object_rb.isKinematic = false;
            if (!PhotonView.Find(viewId).IsMine)
            {
                this.gameObject.GetComponent<Collider>().enabled = true;
            }

            this.gameObject.transform.parent = null;
        }
 
    }

    public void SetLooker(GameObject lookerobject)
    {
        looker = lookerobject;
    }





}
