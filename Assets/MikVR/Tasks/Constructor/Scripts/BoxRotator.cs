using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constructor
{
    public class BoxRotator : MonoBehaviour
    {
        internal static bool VarOut_IsRotating { get; private set; } = false;

        [SerializeField] private Transform tBox = null;
        [SerializeField] private Camera cBoxCamera = null;

        private float startAngle;

        internal void Init()
        {

        }

        private void Update()
        {
            if(DoorKnob.VarOut_CountPullingKnobs.Count > 0) { return; }

            // For touch input on mobile devices
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        VarOut_IsRotating = true;
                        startAngle = AngleBetweenTwoPoints(this.tBox.position, this.cBoxCamera.ScreenToWorldPoint(touch.position));
                        break;

                    case TouchPhase.Moved:
                        if (VarOut_IsRotating)
                        {
                            RotateObjectUsingInput(this.cBoxCamera.ScreenToWorldPoint(touch.position));
                        }
                        break;

                    case TouchPhase.Ended:
                        VarOut_IsRotating = false;
                        break;
                }
            }
            // For mouse input in the Unity editor or a PC build
            else if (Input.GetMouseButtonDown(0))
            {
                VarOut_IsRotating = true;
                startAngle = AngleBetweenTwoPoints(this.tBox.position, this.cBoxCamera.ScreenToWorldPoint(Input.mousePosition));
            }
            else if (Input.GetMouseButton(0) && VarOut_IsRotating)
            {
                RotateObjectUsingInput(this.cBoxCamera.ScreenToWorldPoint(Input.mousePosition));
            }
            else if (Input.GetMouseButtonUp(0))
            {
                VarOut_IsRotating = false;
            }
        }

        private void RotateObjectUsingInput(Vector3 inputPosition)
        {
            float currentAngle = AngleBetweenTwoPoints(this.tBox.position, inputPosition);
            float difference = currentAngle - startAngle;
            this.tBox.Rotate(0, 0, difference);
            startAngle = currentAngle;
        }

        private float AngleBetweenTwoPoints(Vector3 position1, Vector3 position2)
        {
            Vector3 fromLine = position2 - position1;
            Vector3 toLine = Vector3.up;

            float angle = Vector3.Angle(fromLine, toLine);

            // Check for clockwise rotation
            if (position2.x > position1.x)
            {
                angle = 360f - angle;
            }

            return angle;
        }
    }
}