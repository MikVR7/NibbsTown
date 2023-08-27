using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NibbsTown
{
    internal class FingerScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Vector2 currentSize = new Vector2();
        private RectTransform myRectTransform = null;
        private bool isScaling = false;
        private bool isDragging = false;

        //private Vector2 startPositionTouchDownPercent = Vector2.zero;
        //private Vector2 startPositionTouchDownPixel = Vector2.zero;
        //private float ppiOfImage = 407;
        [SerializeField] private Canvas canvas = null;

        private void Awake()
        {
            this.myRectTransform = this.GetComponent<RectTransform>();
            Sprite sprite = this.GetComponent<Image>().sprite;
            this.currentSize = this.myRectTransform.sizeDelta;
        }

        private void Update()
        {
            //this.UpdateGestureEvents();
            ////if(isScaling)
            ////{
            ////    float distance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            ////}
            //if (isDragging)
            //{
            //    //Debug.Log("DRAGGING: " + Input.GetTouch(0).position + " " + Input.GetTouch(0).rawPosition);
            //    Debug.Log("COUNT TOUCH: " + Input.touchCount);
            //    Vector2 offset = Input.GetTouch(0).position - this.startPositionTouchDownPixel;
                
            //}
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //this.UpdateGestureEvents();
            //this.startPositionTouchDownPercent = new Vector2(Input.GetTouch(0).position.x / Screen.width, Input.GetTouch(0).position.y / Screen.height);
            //this.startPositionTouchDownPixel = Input.GetTouch(0).position;
            ////Debug.Log("DRAGGING: " + Input.GetTouch(0).position + " " + percentWidth + "% " + percentHeight + "%");

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //this.UpdateGestureEvents();
        }

        private void UpdateGestureEvents()
        {
            this.isDragging = Input.touchCount > 0;
            this.isScaling = Input.touchCount > 1;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("ON DRAG!!!!");
            this.myRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("ON BEGIN DRAGGGGGG!");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("ON end DRAGGGGGG!");
        }
    }
}
