using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Whiteboard : MonoBehaviour
{
    public GameObject whiteboard;

    public void Destroy()
    {
        Destroy(whiteboard);
    }
}
