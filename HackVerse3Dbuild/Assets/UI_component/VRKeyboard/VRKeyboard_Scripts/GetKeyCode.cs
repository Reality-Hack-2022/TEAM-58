using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GetKeyCode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color32 mNormalColor = Color.white;
    public Color32 mHoverColor = Color.gray;
    public Color32 mDownColor = Color.red;
    
    private OpenVirtualKeyboard keyboardController; // virtual keyboard controller
    private Image buttonImage;                      // key Image
    private string buttonString;                    // key string
    private Text showString;                        // key string (show)

    private InputField inputTarget;                 // the key will input to this input field

    private bool toLowLetterCase;                   // save the statement of keyboard letters
    private readonly CultureInfo cult = new CultureInfo("en-US", false);
    
    private void OnEnable()
    {
        if (keyboardController == null)
            keyboardController = GameObject.Find("Virtual Keyboard Controller").GetComponent<OpenVirtualKeyboard>();
        
        if(buttonImage == null)
            buttonImage = GetComponent<Image>();
        
        if(buttonString == null)
            buttonString = transform.name;

        if (showString == null)
            showString = transform.Find("Text").GetComponent<Text>();
    }

    private void Update()
    {
        if (toLowLetterCase == LetterCaseDetection.Lowercase)
            return;
        
        toLowLetterCase = LetterCaseDetection.Lowercase;
        
        // determine the button string is english or not
        if (Regex.IsMatch(buttonString, "^[a-zA-Z0-9]*$") && 
            !(string.Equals(buttonString, "delete") || string.Equals(buttonString, "clear") || 
              string.Equals(buttonString, "backward") || string.Equals(buttonString, "forward") ||
              string.Equals(buttonString, "Letter case") || string.Equals(buttonString, "To0") ||
              string.Equals(buttonString, "ToLast")))
        {
            buttonString = toLowLetterCase ? buttonString.ToLower(cult) : buttonString.ToUpper(cult);
            showString.text = buttonString;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        keyboardController.onExitKeyboardArea = false;
        buttonImage.color = mHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        keyboardController.onExitKeyboardArea = false;
        buttonImage.color = mNormalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.color = mDownColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        keyboardController.onExitKeyboardArea = false;
        buttonImage.color = mHoverColor;
        
        string target = GetInputFieldTarget.SelectInputFieldName;
        int index = GetInputFieldTarget.Index;
        
        if(inputTarget == null)
            inputTarget = GameObject.Find(GetInputFieldTarget.SelectInputFieldName).GetComponent<InputField>();
        
        if (inputTarget.gameObject.name != target)
        {
#if(UNITY_EDITOR)
            print("change target");
#endif
            inputTarget = GameObject.Find(target).GetComponent<InputField>();
        }
        
#if(UNITY_EDITOR)
        print("You now click = " + buttonString);
        print("Your input target = " + inputTarget.gameObject.name);
        print("Your input index in target = " + index);
#endif
        
        string targetText = inputTarget.text;
        if (!(string.Equals(buttonString, "delete") || string.Equals(buttonString, "clear") || 
            string.Equals(buttonString, "backward") || string.Equals(buttonString, "forward") ||
            string.Equals(buttonString, "Letter case") || string.Equals(buttonString, "To0") ||
            string.Equals(buttonString, "ToLast")))
        {
            inputTarget.text = targetText.Insert(index, buttonString);
#if(UNITY_EDITOR)
            print("inputTarget.text = " + inputTarget.text);
#endif
            GetInputFieldTarget.Index++;
        }
        else
        {
            switch (buttonString)
            {
                case "delete":
                    if (GetInputFieldTarget.Index > 0)
                    {
                        GetInputFieldTarget.Index--;
                        inputTarget.text = targetText.Remove(GetInputFieldTarget.Index, 1);
                    }
#if(UNITY_EDITOR)
                    print("inputTarget.text.Length = " + inputTarget.text.Length);
                    print("GetInputFieldTarget.Index = " + GetInputFieldTarget.Index);
#endif
                    break;
                case "clear":
                    if (inputTarget.text.Length > 0)
                    {
                        inputTarget.text = targetText.Remove(0);
                        GetInputFieldTarget.Index = 0;
                    }
#if(UNITY_EDITOR)
                    print("inputTarget.text.Length = " + inputTarget.text.Length);
                    print("GetInputFieldTarget.Index = " + GetInputFieldTarget.Index);
#endif
                    break;
                case "backward":
                    if(GetInputFieldTarget.Index > 0)
                        GetInputFieldTarget.Index--;
#if(UNITY_EDITOR)
                    print("inputTarget.text.Length = " + inputTarget.text.Length);
                    print("GetInputFieldTarget.Index = " + GetInputFieldTarget.Index);
#endif
                    break;
                case "forward":
                    if (GetInputFieldTarget.Index < inputTarget.text.Length)
                        GetInputFieldTarget.Index++;
#if(UNITY_EDITOR)
                    print("inputTarget.text.Length = " + inputTarget.text.Length);
                    print("GetInputFieldTarget.Index = " + GetInputFieldTarget.Index);
#endif
                    break;
                case "Letter case":
                    LetterCaseDetection.Lowercase = !LetterCaseDetection.Lowercase;
#if(UNITY_EDITOR)
                    print("LetterCaseDetection.Lowercase = " + LetterCaseDetection.Lowercase);
#endif
                    break;
                case "ToLast":
                    GetInputFieldTarget.Index = inputTarget.text.Length;
#if(UNITY_EDITOR)
                    print("inputTarget.text.Length = " + inputTarget.text.Length);
                    print("GetInputFieldTarget.Index = " + GetInputFieldTarget.Index);
#endif
                    break;
                case "To0":
                    GetInputFieldTarget.Index = 0;
#if(UNITY_EDITOR)
                    print("inputTarget.text.Length = " + inputTarget.text.Length);
                    print("GetInputFieldTarget.Index = " + GetInputFieldTarget.Index);
#endif
                    break;
            }
        }
    }
}
