using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCustomisation : MonoBehaviour
{
    public Slider R;
    public Slider G;
    public Slider B;
    public Slider hat;
    public GameObject[] hats;
    float hatindex;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetPlayer(float hatindex)
    {
        for (int i = 0; i < hats.Length; i++)
        {
            if (i == hatindex)
            {
                hats[i].SetActive(true);
            }
            else
            {
                hats[i].SetActive(false);
            }
        }
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }
}
