using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private static PlayerInputAction m_PlayerInputAction;

    public PlayerContext PlayerContext;

    private void Awake()
    {
        m_PlayerInputAction = new PlayerInputAction();
        PlayerContext = GetComponent<PlayerContext>();
        m_PlayerInputAction.PlayerControls.Movement.performed += OnMovement;
        m_PlayerInputAction.PlayerControls.MouseX.performed += OnMouseX;
        m_PlayerInputAction.PlayerControls.MouseY.performed += OnMouseY;
        //m_PlayerInputAction.PlayerControls.MouseScrollY.performed += OnMouseScrollY;
        m_PlayerInputAction.PlayerControls.Fire.performed += OnFire;
        m_PlayerInputAction.PlayerControls.Jump.started += _ => PlayerContext.CanJump(true);
        m_PlayerInputAction.PlayerControls.Jump.canceled += _ => PlayerContext.CanJump(false);
        m_PlayerInputAction.PlayerControls.Exit.performed += OnToggleMenu;
        m_PlayerInputAction.UI.Exit.performed += OnTogglePlayer;


    }

    private void Start()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }
    private void OnMouseY(InputAction.CallbackContext obj)
    {
        float value = obj.ReadValue<float>();
        PlayerContext.MouseLook.x = value;
        //Debug.Log($"Mouse value <b>Y:</b> <color=red>{value}</color>");

    }

   /* private void OnMouseScrollY(InputAction.CallbackContext obj)
    {
        float value = obj.ReadValue<float>();
        PlayerContext.MouseScroll = value;
    }*/


    private void OnEnable()
    {
        m_PlayerInputAction.Enable();
    }

    private void OnDisable()
    {
        m_PlayerInputAction.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        PlayerContext.MovementVector = value;
        //Debug.Log($"Movement executed and value <b>X:</b> <color=red>{value.x}</color> , <b>Y:</b> <color=green>{value.y}</color>");
    }


    public void OnFire(InputAction.CallbackContext context)
    {

    }

    public void OnMouseX(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        PlayerContext.MouseLook.y = value;
        //Debug.Log($"Mouse value <b>X:</b> <color=red>{value}</color>");
    }

    public void OnToggleMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Toggle Menu");
        // Cursor.visible = true;
        m_PlayerInputAction.PlayerControls.Disable();
        m_PlayerInputAction.UI.Enable();
        PlayerContext.Canvas.SetActive(true);
        // FindObjectOfType<MainCanvas>().gameObject.SetActive(true);
    }

    public void OnTogglePlayer(InputAction.CallbackContext context)
    {
        Debug.Log("Toggle Player");

        PlayerContext.Canvas.SetActive(false);
        // Cursor.visible = false;

        m_PlayerInputAction.UI.Disable();
        m_PlayerInputAction.PlayerControls.Enable();

    }

}
