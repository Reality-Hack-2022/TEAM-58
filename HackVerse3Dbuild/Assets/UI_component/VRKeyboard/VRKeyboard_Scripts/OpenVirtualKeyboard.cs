using UnityEngine;

public class OpenVirtualKeyboard : MonoBehaviour
{
    // public GameObject mainCanvas;                // gameObject - main canvas in your scene
    // private RectTransform canvasRectTransform;   // the RectTransform component on the gameObject - main canvas
    private GameObject virtualKeyboard;         // {virtual keyboard} prefabs in your scene
    private RectTransform keyboardBackground;   // the gameObject background in {virtual keyboard} prefabs in your scene

    [HideInInspector]
    public bool onExitKeyboardArea;

    private void Awake()
    {
        // if (mainCanvas != null)
        // {
        //     canvasRectTransform = mainCanvas.GetComponent<RectTransform>();
        // }

        virtualKeyboard = GameObject.Find("Virtual Keyboard").gameObject;
        if (virtualKeyboard == null)
            Debug.LogError("Pls drag the {Virtual Keyboard} prefabs in your scene");
        else
        {
#if (UNITY_EDITOR)
            print("Get the {virtual keyboard} prefabs");
#endif
            keyboardBackground = virtualKeyboard.transform.Find("Background").gameObject.GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (onExitKeyboardArea)
        {
            OnCloseVirtualKeyboard();
        }
        else
        {
            OnOpenVirtualKeyboard();
        }
    }

    public void OnOpenVirtualKeyboard()
    {
        if(virtualKeyboard.activeSelf)
            return;
        
#if(UNITY_EDITOR)
        print("OnOpenVirtualKeyboard");
#endif
        
        SetupKeyboardSize();

        virtualKeyboard.SetActive(true);
    }

    public void OnCloseVirtualKeyboard()
    {
        if(!virtualKeyboard.activeSelf)
            return;
        
#if(UNITY_EDITOR)
        print("OnCloseVirtualKeyboard");
#endif
        
        virtualKeyboard.SetActive(false);
    }

    private void SetupKeyboardSize()
    {
        // you can uncomment when testing
// #if(UNITY_EDITOR)
//         print("SetupKeyboardSize");
// #endif

        // Vector2 interfaceSize = canvasRectTransform.sizeDelta;
        // float keyboardWidth = interfaceSize.y * .6f;
        // float keyboardHeight = interfaceSize.y * .6f;
        
        float keyboardWidth = 485;
        float keyboardHeight = 485;
        
        // you can uncomment when testing
#if(UNITY_EDITOR)
         print($"interface size is {keyboardWidth} x {keyboardHeight}");
#endif
        keyboardBackground.sizeDelta = new Vector2(keyboardWidth, keyboardHeight);
    }
}
