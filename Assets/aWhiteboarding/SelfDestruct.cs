using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelfDestruct : MonoBehaviour, IPointerDownHandler 
{
    public GameObject item;
    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        Destroy(item);
    }
}
