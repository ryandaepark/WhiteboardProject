using UnityEngine;

namespace BeyondTheKnown
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementComponent : MonoBehaviour
    {
        public bool IsJumpPressed;
        private bool IsJumping;
        private const float FallingMultiplier = 2f;
        private float FallingValue;
        private CharacterController m_CharacterController;
        [SerializeField] private float m_Speed;
        [SerializeField] public float m_maxJumpHeight;
        [SerializeField] public float m_maxJumpTime;
        private float m_gravity;
        private float m_initialJumpVelocity;
        private const float m_groundedGravity = -0.5f;
        private Vector3 m_Movement;

        private void Awake()
        {
            m_CharacterController = GetComponent<CharacterController>();
            InitialJumpVariables();
        }

        //gravity and jump velocity for constant Jump trajectory  
        void InitialJumpVariables()
        {
            float timeToApex = m_maxJumpTime * 0.5f;
            m_gravity = (-2 * m_maxJumpHeight) / Mathf.Pow(timeToApex, 2);
            m_initialJumpVelocity = (2 * m_maxJumpHeight) / timeToApex;
        }
        
        public void Move(Vector3 MovementVector)
        {
            m_Movement.x = MovementVector.x * m_Speed;
            m_Movement.z = MovementVector.z * m_Speed;
            //Debug.Log("Movement vector: " + m_Movement);
            m_CharacterController.Move(m_Movement * Time.deltaTime);
            ApplyGravity();
            HandleJump();
        }

        public void ApplyGravity()
        {
            FallingValue = (m_Movement.y <= 0 || !IsJumpPressed) ? FallingMultiplier : 1; 
            if (m_CharacterController.isGrounded)
            {
                m_Movement.y = m_groundedGravity;
            }
            else
            {
                // Avg vertical Velocity is (OLD vel + (OLD vel + gravity))/2;
                float AverageVerticalVelocity = (m_Movement.y + (m_Movement.y + (m_gravity * FallingValue * Time.deltaTime))) * .5f;
                m_Movement.y = AverageVerticalVelocity;
            }
        }

        public void HandleJump()
        {
            if (!IsJumping && m_CharacterController.isGrounded && IsJumpPressed)
            {
                IsJumping = true;
                m_Movement.y = m_initialJumpVelocity * 0.5f;
            }else if (IsJumping && m_CharacterController.isGrounded)
            {
                IsJumping = false;
            }
        }
        
    }
}
