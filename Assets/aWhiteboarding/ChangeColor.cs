using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeColor : MonoBehaviour, IPointerDownHandler
{
    private int n, len;
    public Material[] colors;
    public GameObject target;
    private Renderer rend;
    public Material M0, M1, M2, M3;

    // Start is called before the first frame update
    void Awake()
    {
        rend = target.GetComponent<Renderer>();
        n = 0;
        colors = new Material[] { M0, M1, M2, M3 };
        len = colors.Length - 1;
    }
    // Update is called once per frame
    public void OnPointerDown(PointerEventData eventData)
    {


        Debug.Log(n + "   ");// + colors[n]);
        rend.material = colors[n];
        if(n >= len)
        {
            n = 0;
        }
        else
        {
            n += 1;
        }

    }


}
