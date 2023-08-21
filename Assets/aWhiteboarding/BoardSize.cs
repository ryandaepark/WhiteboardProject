using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardSize : MonoBehaviour, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerExitHandler
{
    //allows you to resize an item in a 2d scale- build for boards specifiacally

    //FOR CONTROLLER: name it something explicit, populate as Gameobject.find  B)
    public GameObject controller, note, gregary;  //note: the item to resize, gregary: the little grey bar under the board that gets stretched out in one dimension  
    private float distance, xScale, yScale;
    private Vector3 pos, localPos;
    private Transform dot;


    private void Awake()
    {
        {
            controller = GameObject.Find("RightHandAnchor");
            //Debug.Log(controller);
            dot = GetComponent<Transform>();
            xScale = note.transform.localScale.x / dot.transform.localPosition.x;
            yScale = note.transform.localScale.y / dot.transform.localPosition.y;
        }
    }
    // Start is called before the first frame update
    public void OnBeginDrag(PointerEventData eventData)
    {

        //find initial distance of controller
        Debug.Log("Begin Drag");
        distance = eventData.pointerPressRaycast.distance;
    }

    public void OnDrag(PointerEventData eventData)
    {

        Resize();
    }



    public void Resize()
    {
        //find pointer X and Y and convert to scale
        pos = controller.transform.position + controller.transform.forward * distance;
        localPos = dot.parent.transform.InverseTransformPoint(pos);
        note.transform.localScale = new Vector3(localPos.x * xScale, localPos.y * yScale, note.transform.localScale.z);
        note.transform.localPosition = new Vector3(localPos.x / 2, localPos.y / 2, note.transform.localPosition.z);

        //resize note, move note, menu, text box, and anchors
        dot.localPosition = new Vector3(localPos.x, localPos.y, dot.localPosition.z);

        gregary.transform.localPosition = new Vector3(localPos.x / 2, gregary.transform.localPosition.y, gregary.transform.localPosition.y);
        gregary.transform.localScale = new Vector3(localPos.x * xScale, gregary.transform.localScale.y, gregary.transform.localScale.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        Debug.Log("end");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}