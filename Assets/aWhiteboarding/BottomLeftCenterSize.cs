using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BottomLeftCenterSize : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    //resizes an object with the origin in the bottom left.
    public GameObject background, controller, note, menu, bottomAnchor, topAnchor, leftAnchor, rightAnchor, textBox;    //all parts that need to react differently to scaling
    private RectTransform textBoxTransform;
    private float distance, xScale, yScale;
    private Vector3 pos, localPos, textBoxScale;
    private Transform dot;

    private void Awake()
    {
        {
            controller = GameObject.Find("RightHandAnchor");
            //Debug.Log(controller);
            dot = GetComponent<Transform>();
            textBoxScale = textBox.transform.localScale;
            textBoxTransform = textBox.GetComponent<RectTransform>();
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
        menu.transform.localPosition = new Vector3(localPos.x / 2, menu.transform.localPosition.y, menu.transform.localPosition.z);

        textBoxTransform.sizeDelta = new Vector2(localPos.x / textBoxScale.x, localPos.y / textBoxScale.y);
        textBox.transform.localPosition = new Vector3(localPos.x / 2, localPos.y / 2, textBox.transform.localPosition.z);

        bottomAnchor.transform.localPosition = new Vector3(localPos.x / 2, 0.0f, bottomAnchor.transform.localPosition.z);
        topAnchor.transform.localPosition = new Vector3(localPos.x / 2, localPos.y, topAnchor.transform.localPosition.z);
        leftAnchor.transform.localPosition = new Vector3(0.0f , localPos.y / 2, leftAnchor.transform.localPosition.z);
        rightAnchor.transform.localPosition = new Vector3(localPos.x, localPos.y / 2, rightAnchor.transform.localPosition.z);
    }
}

