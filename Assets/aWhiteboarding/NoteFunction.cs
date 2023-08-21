using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteFunction : MonoBehaviour, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerExitHandler
{
    private TouchScreenKeyboard keyboard;
    public Text noteWords;
    private bool typing;
    public GameObject controller;
    private float distance;
    private bool hover;
    private Vector3 pos, difference;
    private Vector3 localPos;
    public Transform note;
    private float speed = 0.05f;

    private void Awake()
    {
        {

        }
    }

    void Update()
    {
        Debug.Log(hover);
        if (typing)

        {

            Debug.Log("Wahoo!");
            Debug.Log("Wahoo!");
            Debug.Log("Wahoo!");
            noteWords.text = keyboard.text;
            if (keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done)
            {

                Debug.Log(noteWords.text);
                typing = false;

                Debug.Log("ITS DONE! \n ITS DONE! \n ITS DONE! \n ITS DONE! \n ITS DONE! \n " + keyboard.text);
            }
        }
        else if(hover)
        {
            Debug.Log("Hover \n Hover! \n Hover! \n Hover \n Hover \n " + keyboard.text);
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                Debug.Log("Begin!");
                ChangeNoteText();
            }
        }
    }

    // Start is called before the first frame update
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        distance = eventData.pointerPressRaycast.distance;
        pos = controller.transform.position + controller.transform.forward * distance + difference;
        difference = pos - note.position;
        if (distance < 0.4f)
        {
            distance = 0.4f;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (OVRInput.Get(OVRInput.Button.Two))
        {
            distance = distance + speed;
            Debug.Log("A");
        }
        else if (OVRInput.Get(OVRInput.Button.One))
        {
            distance = distance - speed;
            Debug.Log("B");
            if (distance < 0.4f)
            {
                distance = 0.4f;
            }

        }

        pos = controller.transform.position + controller.transform.forward * distance; //+ difference;
        note.position = pos;
        note.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        hover = true;
        Debug.Log(hover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
        hover = false;
        Debug.Log(hover);
    }

    private void ChangeNoteText()
    {
        keyboard = TouchScreenKeyboard.Open(noteWords.text, TouchScreenKeyboardType.Default);
        typing = true;
    }
}
