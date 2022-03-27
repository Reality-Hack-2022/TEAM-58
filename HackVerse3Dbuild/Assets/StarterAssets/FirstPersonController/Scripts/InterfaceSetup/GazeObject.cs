using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[System.Serializable]
public struct GazeData
{
    public Material HighlightMaterial;
    //other info we might want to be configurable

    //this data set by the cursor at time of hover
    [System.NonSerialized]
    public Component GazeScript;
    [System.NonSerialized]
    public Camera ViewCamera;


}
public enum GazeStates
{

    Default,
    Clickable,
    Loading,
    Movable,
    Info,
    Smile

}


public class GazeObject : MonoBehaviour
{
    [Header("Gaze Settings")]
     public GameObject reticleEnlargedRef;
    public GameObject reticleDefaultRef;
    public GameObject reticleArrowsRef;
    public GameObject reticleLoadingRef;
    public GameObject reticleInfoRef;
    public GameObject reticleSmileRef;

    public static GameObject reticleEnlarged;
    public static GameObject reticleDefault;
    public static GameObject reticleArrows;
    public static GameObject reticleLoading;
    public static GameObject reticleInfo;
    public static GameObject reticleSmile;

    static GameObject currentReticle;
    public float gazeAnimationDurationRef = 1f;
    public static float gazeAnimationDuration;

    Vector3 currentscale;
    static Tween currenttween;
    private static GazeObject _instance;

    public static GazeObject Instance { get { return _instance; } }
    void MakeInstance()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void AssignSprites()
    {
        reticleDefault = reticleDefaultRef;
        reticleEnlarged = reticleEnlargedRef;
        reticleLoading = reticleLoadingRef;
        reticleArrows = reticleArrowsRef;
        reticleInfo = reticleInfoRef;
        reticleSmile = reticleSmileRef;
    }
    private void Awake()
    {
        gazeAnimationDuration = gazeAnimationDurationRef;
        MakeInstance();
        AssignSprites();
        reticleEnlarged.SetActive(false);
        reticleDefault.SetActive(true);
        //     reticleEnlarged.transform.DOScale(0.1f, gazeAnimationSpeed);
     }

    
    public static void SetGazeState(GazeStates currentstate)
    {
        
        reticleDefault.transform.DOScale(2.5f,0f).OnComplete(() => reticleDefault.SetActive(false));
 
        GazeStates choice = currentstate;
        switch (choice)
        {
            
            case GazeStates.Clickable:
                GazeClickable();
                break;
            case GazeStates.Loading:
                
                GazeLoading();
                break;
            case GazeStates.Movable:
                GazeMovable();
                break;
            case GazeStates.Info:
                GazeInfo();
                break;
            case GazeStates.Smile:
                GazeSmile();
                break;
            default:
                 break;
        }

    }
    public static void GazeLoading()
    {
        reticleLoading.SetActive(true);
        reticleInfo.SetActive(false);
        reticleLoading.transform.DOLocalRotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360).SetLoops(6).SetRelative(true).SetEase(Ease.Linear).OnComplete(DebugClose);

     }
    public static void GazeSmile()
    {
        reticleSmile.SetActive(true);
        currentReticle = reticleSmile;
    }    
    static void DebugClose()
    {
        reticleLoading.SetActive(false);
        reticleDefault.SetActive(true);
        currentReticle = reticleLoading;
    }
    public static void GazeClickable()
    {
        reticleEnlarged.SetActive(true);
        currentReticle = reticleEnlarged;
    }
    public static void GazeInfo()
    {
        reticleInfo.SetActive(true);
        currentReticle = reticleInfo;
    }
    public static void GazeMovable()
    {
        reticleEnlarged.SetActive(true);
        reticleArrows.SetActive(true);
      

      currenttween=  reticleArrows.transform.DOScale(1.3f, 0.3f).SetLoops(-1, (LoopType.Yoyo));


        currentReticle = reticleArrows;
    }
    public static void GazeDefault()
    {
        if (currentReticle)
        {
            currentReticle.SetActive(false);
        }
        
        
            currenttween.Rewind();
         
        currentReticle = null;
        reticleEnlarged.SetActive(false);
        reticleDefault.SetActive(true);
        reticleDefault.transform.DOScale(1f, gazeAnimationDuration);

    }

   
}
