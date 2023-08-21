using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeyondTheKnown
{
    public class LookAroundComponent : MonoBehaviour
    {
        public enum InvertMouse
        {
            Yes = -1,No = 1
        }
        public Camera m_Camera;
        public float mouseSensitivity;
        public InvertMouse m_InvertMouse = InvertMouse.Yes;
        public float CameraClamp;
        private float xRotation;

        public void LookAround(Vector2 vec)
        {
            vec *= mouseSensitivity * Time.deltaTime;
            CameraRotation(vec.x);
            PlayerRotation(vec.y);

        }

        private void CameraRotation(float vecX)
        {
           
            xRotation -=  vecX;
            xRotation = Mathf.Clamp(xRotation, -CameraClamp,CameraClamp);
            var transformEulerAngles = m_Camera.transform.eulerAngles;
            transformEulerAngles.x = xRotation;
            m_Camera.transform.eulerAngles = transformEulerAngles;
        }

        private void PlayerRotation(float vecY)
        {
            transform.Rotate(Vector3.up,vecY);
        }
    }
}
