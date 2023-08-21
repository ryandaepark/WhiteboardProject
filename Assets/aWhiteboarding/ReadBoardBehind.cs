using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReadBoardBehind : MonoBehaviour
{
    public GameObject board;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;
        int layerMask = 1 << 9;
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            board = hit.collider.gameObject;
        }
        else 
        {
            board = null;
        }
    }
}