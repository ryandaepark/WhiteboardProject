using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class FontSizeEdit : MonoBehaviour, IPointerDownHandler
{
    public bool increase;
    public Text words;
    public int fontSize;



    public void OnPointerDown(PointerEventData eventData)
    {

        //edits font size of terget text when button is pressed

        if(increase)
        {
            fontSize = words.fontSize;
            fontSize += 10;
            words.fontSize = fontSize;
        }
        else
        {
            fontSize = words.fontSize;
            fontSize -= 10;
            words.fontSize = fontSize;
        }

    }
}
