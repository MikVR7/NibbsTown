using System.Collections.Generic;
using UnityEngine;

namespace Constructor
{
    internal class DoorKnob : MonoBehaviour
    {
        internal static List<Vector3> VarOut_CountPullingKnobs { get; private set; } = new List<Vector3>();

        private Vector3 initialTouchPosition;
        
        [SerializeField] private Transform tDoorKnob = null;
        [SerializeField] private Transform tKnob = null;
        private Vector3 initialKnobPosition = Vector3.zero;
        [SerializeField] private bool isTouched = false;
        private float offsetX = 0f;
        private float startXPos = 0f;
        private Camera cameraMain = null;

        internal void Init(Camera cameraMain)
        {
            this.cameraMain = cameraMain;
            this.offsetX = tKnob.localPosition.x;
            this.initialKnobPosition = tKnob.localPosition; // Use local position here
        }

        private void Update()
        {
            Vector3 inputPos = Vector3.zero;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                inputPos = cameraMain.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began && IsTouchingThisObject(inputPos))
                {
                    initialTouchPosition = inputPos;
                    VarOut_CountPullingKnobs.Add(this.initialKnobPosition);
                    startXPos = this.tDoorKnob.localPosition.x;
                }

                if (touch.phase == TouchPhase.Moved && isTouched)
                {
                    MoveChildObject(inputPos);
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (IsTouchingThisObject(inputPos))
                {
                    initialTouchPosition = inputPos;
                    VarOut_CountPullingKnobs.Add(this.initialKnobPosition);
                    startXPos = this.tDoorKnob.localPosition.x;
                }
            }
            else if (Input.GetMouseButton(0) && isTouched)
            {
                inputPos = cameraMain.ScreenToWorldPoint(Input.mousePosition);
                MoveChildObject(inputPos);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                this.isTouched = false;
                VarOut_CountPullingKnobs.Remove(this.initialKnobPosition);
            }
        }

        private bool IsTouchingThisObject(Vector3 inputPosition)
        {
            RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero);
            this.isTouched = hit.collider != null && hit.collider.transform == tKnob;
            return isTouched;
        }

        private void MoveChildObject(Vector3 inputPosition)
        {
            Vector3 globalDragDirection = inputPosition - initialTouchPosition;
            float dragAmount = Vector3.Dot(globalDragDirection, tKnob.right);
            Vector3 newPosition = new Vector3(initialKnobPosition.x + dragAmount, initialKnobPosition.y, initialKnobPosition.z);
            newPosition.x = Mathf.Clamp(newPosition.x - offsetX + startXPos, -1f, 0f);
            tDoorKnob.localPosition = newPosition;
        }
    }
}