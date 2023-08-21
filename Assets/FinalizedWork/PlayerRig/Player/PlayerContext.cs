using System;
using System.Collections;
using System.Collections.Generic;
using BeyondTheKnown;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class PlayerContext : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 MovementVector;
    public Vector2 MouseLook;
    //public float MouseScroll;
    private MovementComponent m_MovementComponent;
    private LookAroundComponent m_LookAroundComponent;
    private Vector3 pos;
    public bool Teleporting;
    public GameObject Canvas;
    void Start()
    {
        m_MovementComponent = GetComponent<MovementComponent>();
        m_LookAroundComponent = GetComponent<LookAroundComponent>();

    }

    private void Update()
    {
        
        if(MouseLook != Vector2.zero) 
            m_LookAroundComponent.LookAround(MouseLook);
        if(!Teleporting)
            Move(MovementVector);
    }
    

    // Update is called once per frame
    public void Move(Vector2 vec)
    {
        //Debug.Log("Move Executed");
        if (vec != Vector2.zero)
        {
            pos = (transform.right * vec.x) + (transform.forward * vec.y); 
        }else pos = Vector3.zero;
        m_MovementComponent.Move(pos);
    }

    public void CanJump(bool value)
    {
        m_MovementComponent.IsJumpPressed = value;
    }
}
