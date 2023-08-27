using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PicturePuzzle
{
    internal class PuzzlePart : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RawImage rawImage = null;
        [SerializeField] private Image imgOverlay = null;
        private CanvasGroup canvasGroup = null;
        private Vector2 touchOffset = Vector2.zero;
        internal RectTransform myRectTransform { get; private set; } = null;
        private RectTransform parentRectTransform = null;
        private bool isTemplateElement = false;
        private Action<(int, int)> onActionStartDrag = null;
        private Action<(int, int)> onActionRelease = null;
        private (int, int) ijIndices = (0, 0);

        internal void Init(
            (int, int) ijIndices,
            bool isTemplate,
            Action<(int, int)> onActionStartDrag,
            Action<(int, int)> onActionRelease)
        {
            this.ijIndices = ijIndices;
            this.isTemplateElement = isTemplate;
            this.onActionStartDrag = onActionStartDrag;
            this.onActionRelease = onActionRelease;
            this.myRectTransform = this.GetComponent<RectTransform>();
            this.parentRectTransform = this.myRectTransform.parent.GetComponent<RectTransform>();
            this.canvasGroup = this.GetComponent<CanvasGroup>();
        }

        internal void SetTexturePart(Texture2D texture)
        {
            this.rawImage.texture = texture;
        }

        internal void SetOverlayAlpha(float alpha)
        {
            this.imgOverlay.color = new Color(
                this.imgOverlay.color.r,
                this.imgOverlay.color.g,
                this.imgOverlay.color.b,
                alpha);
        }

        internal void SetSizePosition(Vector2 size, Vector2 position)
        {
            this.myRectTransform.sizeDelta = size;
            this.myRectTransform.anchoredPosition = position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(this.isTemplateElement) { return; }
            canvasGroup.blocksRaycasts = false;

            // Get the local point in the parent's rectangle.
            Vector2 localTouchPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out localTouchPosition))
            {
                // The offset is the difference between the current anchored position of the puzzle part and the local touch position.
                touchOffset = myRectTransform.anchoredPosition - localTouchPosition;
                this.onActionStartDrag.Invoke(ijIndices);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (this.isTemplateElement) { return; }
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
            {
                // To maintain the touch offset, add it back to the local point.
                myRectTransform.anchoredPosition = localPoint + touchOffset;
                //this.onActionDrag.Invoke(indexI, indexJ);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (this.isTemplateElement) { return; }
            canvasGroup.blocksRaycasts = true;
            this.onActionRelease.Invoke(ijIndices);
        }

        private List<(int, int)> addedPuzzleParts = new List<(int, int)>();
        internal bool VarOut_IsCorrectPuzzlePart { get; private set; } = false;
        internal void AddPuzzlePart((int, int) ijTouple)
        {
            if(!addedPuzzleParts.Contains(ijTouple)) {
                addedPuzzleParts.Add(ijTouple);
            }
            this.CheckIfPuzzlePartIsCorrect();
        }

        internal void RemovePuzzlePart((int, int) ijTouple)
        {
            if (addedPuzzleParts.Contains(ijTouple))
            {
                addedPuzzleParts.Remove(ijTouple);
            }
            this.CheckIfPuzzlePartIsCorrect();
        }

        private void CheckIfPuzzlePartIsCorrect()
        {
            VarOut_IsCorrectPuzzlePart =
                ((addedPuzzleParts.Count == 1) &&
                (addedPuzzleParts[0].Equals(this.ijIndices)));
        }
    }
}
 