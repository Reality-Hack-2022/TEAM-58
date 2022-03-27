using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeaItemObject : MonoBehaviour,IHoverable
{
    public bool IsSelected => throw new System.NotImplementedException();
    public GameObject cubechild;
    public GameObject looker;
    bool isView=false;
    public void OnClick(GazeData data)
    {
        print("you pressed ");
        if(!isView)
        {
            Cursor.lockState = CursorLockMode.None;
            isView = true;
            looker.GetComponent<StarterAssets.FirstPersonController>().filterstate = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            isView = false;
            looker.GetComponent<StarterAssets.FirstPersonController>().filterstate = false;
        }
    }

    public void OnGazeEnter(GazeData data)
    {
        cubechild.GetComponent<Outline>().enabled = true;
        print("you are looking at idea");
    }

    public void OnGazeExit(GazeData data)
    {
        cubechild.GetComponent<Outline>().enabled = false;
        looker = null;

    }

    public void SetLooker(GameObject gameObject)
    {
        looker = gameObject;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
