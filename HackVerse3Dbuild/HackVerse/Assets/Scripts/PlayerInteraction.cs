using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Portal")
        {
            this.gameObject.transform.position += new Vector3(0f, 0.1f, 0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.transform.position = new Vector3(965.659973f, 2015.35303f, 5014.49023f);
    }
}
