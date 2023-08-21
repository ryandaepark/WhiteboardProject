using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextEditor : MonoBehaviour
{
    public TouchScreenKeyboard overlayKeyboard;
    private bool typing;
    public Text words; //the target text to edit
    public string mid;

    private void Awake()
    {
        typing = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (typing)
        {
            mid = overlayKeyboard.text;
            words.text = mid;
            if (overlayKeyboard != null && overlayKeyboard.status == TouchScreenKeyboard.Status.Done)
            {
                typing = false;
            }
        }
    }

    private void changeNoteText()
    {
        typing = true;
        overlayKeyboard = TouchScreenKeyboard.Open(words.text, TouchScreenKeyboardType.Default);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        changeNoteText();
    }

}
