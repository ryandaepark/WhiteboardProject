using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    public float baseSpeed = 10.0f;
    public float shiftSpeed = 20.0f;
    public Vector2 cameraSensitivity = new Vector2(0.1f, 0.1f);

    private Vector3 lastMouse;

    private void Start()
    {
        lastMouse = Input.mousePosition;
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //}

        //if (Input.GetMouseButtonUp(1))
        //{
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //}

        if (Input.GetMouseButton(1))
        {
            lastMouse = Input.mousePosition - lastMouse;
            lastMouse = new Vector3(-lastMouse.y, lastMouse.x, 0.0f) * cameraSensitivity;
            lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0.0f);
            transform.eulerAngles = lastMouse;
        }

        lastMouse = Input.mousePosition;

        Vector3 v = new Vector3();
        if (Input.GetKey(KeyCode.W)) v += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.S)) v += new Vector3(0, 0, -1);
        if (Input.GetKey(KeyCode.A)) v += new Vector3(-1, 0, 0);
        if (Input.GetKey(KeyCode.D)) v += new Vector3(1, 0, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= shiftSpeed;
        }
        else
        {
            v *= baseSpeed;
        }

        v *= Time.deltaTime;

        transform.Translate(v);
    }
}
