using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class InteractableButton : MonoBehaviour, IHoverable
{

    [Header("Button Settings")]
    public Image imageComponent;
    public Color defaultColor;
    public Color hoverColor;
    public Color pressedColor;
    public float distance=0.2f;
    [Range(0, 1)]
    public float expandButtonRate = 0.3f;

    public UnityEvent unityEvent = new UnityEvent();

    public bool IsSelected => throw new System.NotImplementedException();

    void Awake()
    {
        defaultColor = imageComponent.GetComponent<Image>().color;
    }
    IEnumerator ButtonClickTransition()
    {
        imageComponent.color = pressedColor;
        imageComponent.transform.DOLocalMoveY(-0.03f, 0.1f);
       // GazeObject.GazeDefault();

        yield return new WaitForSeconds(0.3f);
        imageComponent.transform.DOLocalMoveY(0f, 0.1f);
        imageComponent.color = hoverColor;

    //    GazeObject.SetGazeState(GazeStates.Clickable);


    }
    public void OnClick(GazeData data)
    {
        unityEvent.Invoke();
        StartCoroutine(ButtonClickTransition());

    }

    public void OnGazeEnter(GazeData data)
    {
        imageComponent.DOColor(hoverColor, expandButtonRate);
        imageComponent.transform.DOLocalMoveZ(distance, expandButtonRate);
     //   GazeObject.SetGazeState(GazeStates.Clickable);

    }

    public void OnGazeExit(GazeData data)
    {
        imageComponent.DOColor(defaultColor, expandButtonRate);
        imageComponent.transform.DOLocalMoveZ(0.0f, expandButtonRate);
//        GazeObject.GazeDefault();



    }

    public void SetLooker(GameObject gameObject)
    {
     }
}