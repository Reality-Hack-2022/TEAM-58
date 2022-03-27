using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
// [RequireComponent(typeof(EventTrigger))]
public class InputFieldDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    private InputField myselfInputField;            // myself component - InputField
    private Text inputFieldText;                    // myself component in InputField - Text
    private InputField.LineType inputFieldLineType; // the lineType of InputField component
    private OpenVirtualKeyboard keyboardController; // Virtual Keyboard Controller in the scene

    private void Awake()
    {
        if (myselfInputField == null)
            myselfInputField = GetComponent<InputField>();
        
        if (inputFieldText == null)
            inputFieldText = myselfInputField.textComponent;
        
        inputFieldLineType = myselfInputField.lineType;
        
        if (keyboardController == null)
            keyboardController = GameObject.Find("Virtual Keyboard Controller").GetComponent<OpenVirtualKeyboard>();
        
        // keyboardController.onExitInputField = true;
        
        // myselfInputField.onEndEdit.AddListener(eventData =>
        // {
        //     keyboardController.onExitInputField = true;
        // });
        
        // EventTrigger trigger = GetComponent<EventTrigger>();
        // EventTrigger.Entry entry = new EventTrigger.Entry {eventID = EventTriggerType.Deselect};
        // entry.callback.AddListener((eventData) =>
        // {
        //     keyboardController.onExitInputField = true;
        // });
        // trigger.triggers.Add(entry);
    }

    private void OnEnable()
    {
        if (myselfInputField == null)
            myselfInputField = GetComponent<InputField>();
        
        if (inputFieldText == null)
            inputFieldText = myselfInputField.textComponent;
        
        inputFieldLineType = myselfInputField.lineType;
        
        if (keyboardController == null)
            keyboardController = GameObject.Find("Virtual Keyboard Controller").GetComponent<OpenVirtualKeyboard>();
        
        // keyboardController.onExitInputField = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
#if(UNITY_EDITOR)
        print("InputField OnPointerEnter");
#endif
    }

    public void OnPointerExit(PointerEventData eventData)
    {
#if(UNITY_EDITOR)
        print("InputField OnPointerExit");
#endif
    }

    public void OnPointerDown(PointerEventData eventData)
    {
#if(UNITY_EDITOR)
        print("InputField OnPointerDown");
#endif
    }

    public void OnPointerUp(PointerEventData eventData)
    {
#if(UNITY_EDITOR)
        print("InputField OnPointerUp");
#endif
    }

    /// <summary>
    /// Run the function after the pointer click the inputfield
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        keyboardController.onExitKeyboardArea = false;
        GetInputFieldTarget.SelectInputFieldName = transform.name;
        
        // you can uncomment when testing
#if(UNITY_EDITOR)
         print("SelectInputFieldName = " + transform.name);
#endif

        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(inputFieldText.rectTransform, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out localMousePos);
        print($"localMousePos = ({localMousePos.x},{localMousePos.y})");
        GetInputFieldTarget.Index = GetCharacterIndexFromPosition(localMousePos, inputFieldText, inputFieldLineType);
        
        // you can uncomment when testing
#if(UNITY_EDITOR)
         print("index = " + GetInputFieldTarget.Index);
#endif

        keyboardController.OnOpenVirtualKeyboard();
    }
    
    /// <summary>
    /// Get the character local index position when OnPointerClick at inputField.text
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="text"></param>
    /// <param name="lineType"></param>
    /// <returns></returns>
    private int GetCharacterIndexFromPosition(Vector2 pos, Text text, InputField.LineType lineType)
    {
        TextGenerator gen = text.cachedTextGenerator;

        if (gen.lineCount == 0)
        {
            print("cachedTextGenerator = 0");
            return 0;
        }

        int line = GetUnclampedCharacterLineFromPosition(pos, gen, lineType, text);
        if (line < 0)
            return 0;
        if (line >= gen.lineCount)
            return gen.characterCountVisible;

        int startCharIndex = gen.lines[line].startCharIdx;
        int endCharIndex = GetLineEndPosition(gen, line);

        for (int i = startCharIndex; i < endCharIndex; i++)
        {
            if (i >= gen.characterCountVisible)
                break;

            UICharInfo charInfo = gen.characters[i];
            Vector2 charPos = charInfo.cursorPos / text.pixelsPerUnit;

            float distToCharStart = pos.x - charPos.x;
            float distToCharEnd = charPos.x + (charInfo.charWidth / text.pixelsPerUnit) - pos.x;
            if (distToCharStart < distToCharEnd)
                return i;
        }

        return endCharIndex;
    }
    
    /// <summary>
    /// Part of GetCharacterIndexFromPosition, to run the function by OnPointerClick
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="generator"></param>
    /// <param name="lineType"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    private int GetUnclampedCharacterLineFromPosition(Vector2 pos, TextGenerator generator, InputField.LineType lineType, Text text)
    {
        if (!(lineType == InputField.LineType.MultiLineNewline || lineType == InputField.LineType.MultiLineSubmit))
            return 0;

        // transform y to local scale
        float y = pos.y * text.pixelsPerUnit;
        float lastBottomY = 0.0f;

        for (int i = 0; i < generator.lineCount; ++i)
        {
            float topY = generator.lines[i].topY;
            float bottomY = topY - generator.lines[i].height;

            // pos is somewhere in the leading above this line
            if (y > topY)
            {
                // determine which line we're closer to
                float leading = topY - lastBottomY;
                if (y > topY - 0.5f * leading)
                    return i - 1;
                else
                    return i;
            }

            if (y > bottomY)
                return i;

            lastBottomY = bottomY;
        }

        // Position is after last line.
        return generator.lineCount;
    }
    
    /// <summary>
    /// Part of GetCharacterIndexFromPosition, to run the function by OnPointerClick
    /// </summary>
    /// <param name="gen"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    private static int GetLineEndPosition(TextGenerator gen, int line)
    {
        line = Mathf.Max(line, 0);
        if (line + 1 < gen.lines.Count)
            return gen.lines[line + 1].startCharIdx - 1;
        return gen.characterCountVisible;
    }
}
