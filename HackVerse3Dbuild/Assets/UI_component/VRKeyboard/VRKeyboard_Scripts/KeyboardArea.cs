using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    // the waiting time of the closing keyboard
    // triggered by the control ray which is exited the keyboard area
    public int backWaitingTime = 3;
    
    private OpenVirtualKeyboard keyboardController; // Virtual Keyboard Controller
    private Coroutine closeKeyboardCoroutine;       // close keyboard coroutine

    private void Awake()
    {
        if (keyboardController == null)
            keyboardController = GameObject.Find("Virtual Keyboard Controller").GetComponent<OpenVirtualKeyboard>();

        keyboardController.onExitKeyboardArea = true;
    }

    private void OnEnable()
    {
        if (keyboardController == null)
            keyboardController = GameObject.Find("Virtual Keyboard Controller").GetComponent<OpenVirtualKeyboard>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(closeKeyboardCoroutine != null)
            StopCoroutine(closeKeyboardCoroutine);
#if(UNITY_EDITOR)
        print("Keyboard Area Enter");
#endif
        keyboardController.onExitKeyboardArea = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
#if(UNITY_EDITOR)
        print("Keyboard Area Exit");
#endif
        closeKeyboardCoroutine = StartCoroutine(RecordExitTime());
    }

    private IEnumerator RecordExitTime()
    {
        yield return new WaitForSeconds(backWaitingTime);
        keyboardController.onExitKeyboardArea = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
#if(UNITY_EDITOR)
        print("Keyboard Area OnPointerDown");
#endif
    }

    public void OnPointerUp(PointerEventData eventData)
    {
#if(UNITY_EDITOR)
        print("Keyboard Area OnPointerUp");
#endif
    }

    public void OnPointerClick(PointerEventData eventData)
    {
#if(UNITY_EDITOR)
        print("Keyboard Area OnPointerClick");
#endif
    }
}
