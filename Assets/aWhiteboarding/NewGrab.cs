using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewGrab : MonoBehaviour, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    //Script that allows movement of item. Item will move in a 2d plane relative to its parent if whiteboarded is true and 3d if not
    //in 3d movement, the a and b buttons move it in and out

    private GameObject controller;
    private float distance;
    private Vector3 pos, difference;
    private Vector3 localPos;
    public Transform note, originalParent; //note: the item to move, OriginalParent: the parent to set it to when entering 3d movement
    private float speed = 0.05f;    //the speed of moving an item forward or back in 3d mode
    public bool WhiteBoarded;
    private ReadBoardBehind targetBoard;

    private void Awake()
    {
        {
            controller = GameObject.Find("RightHandAnchor");
            targetBoard = controller.GetComponent<ReadBoardBehind>();
            originalParent = note.transform.parent;
        }
    }

    // Start is called before the first frame update
    public void OnBeginDrag(PointerEventData eventData)
    {

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
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            WhiteBoarded = !WhiteBoarded;
            if (WhiteBoarded) {
                if (targetBoard.board != null)
                {
                    note.SetParent(targetBoard.board.transform.parent);
                    Debug.Log("We got here");
                } 
                else
                {
                    WhiteBoarded = !WhiteBoarded;
                }
            } 
            else
            {
                note.SetParent(originalParent);
            }
            
        }
        
        if (WhiteBoarded)
        {
            Move2d();
        }
        else
        {
            Move3d();
        }
        
    }
    
    public void Move3d()
    {
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            distance = distance + speed;

        }
        else if (OVRInput.Get(OVRInput.Button.One))
        {
            distance = distance - speed;

            if (distance < 0.4f)
            {
                distance = 0.4f;
            }

        }

        pos = controller.transform.position + controller.transform.forward * distance; //+ difference;
        note.position = pos;
        note.rotation = controller.transform.rotation;
    }

    public void Move2d()
    {
        pos = controller.transform.position + controller.transform.forward * distance;
        localPos = note.parent.transform.InverseTransformPoint(pos);
        note.transform.localPosition = new Vector3(localPos.x, localPos.y, -10.0f); //
        //note.position = new Vector3(pos.x, pos.y, note.parent.transform.position.z);
        //note.transform.localRotation.Set(0.0f, 0.0f, 0.0f, 1);
        note.rotation = note.parent.rotation;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }
}
