using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WordSearch
{
    internal class WordLetter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private TextMeshProUGUI tmpLetter = null;
        private (int, int) xyIndices = (0, 0);
        private Coroutine coroutine = null;

        private bool isDragging = false;

        internal void Init((int, int) xyIndices, string letter)
        {
            this.tmpLetter.text = letter;
            this.xyIndices = xyIndices;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (this.coroutine == null)
            {
                isDragging = true;
                ObjectTouched(eventData.pointerCurrentRaycast.gameObject);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //isDragging = false;
            //WordsMatrix.EventIn_FinishedSelectingWord.Invoke(WordsMatrix.VarOut_TouchedIndices);

            //WordsMatrix.VarOut_TouchedIndices.Clear();
            //WordsMatrix.VarOut_CurrentFingerMovement = FingerMovement.None;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isDragging)
            {
                ObjectTouched(eventData.pointerCurrentRaycast.gameObject);
            }
        }

        private void ObjectTouched(GameObject touchedObject)
        {
            //if (touchedObject != null && (touchedObject.name != WordsMatrix.VarOut_LastTouchedObject))
            //{
            //    WordLetter touchedWordLetter = touchedObject.GetComponent<WordLetter>();
            //    if (touchedWordLetter != null)
            //    {
            //        if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.None)
            //        {
            //            WordsMatrix.VarOut_CurrentFingerMovement = FingerMovement.Undecided;
            //            WordsMatrix.VarOut_TouchedIndices.Add(touchedWordLetter.indices);
            //            touchedWordLetter.imgBG.color = colorTouched;
            //        }
            //        else if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.Undecided)
            //        {
            //            //touchedWordLetter = FixCrossError(touchedWordLetter);
            //            WordsMatrix.VarOut_CurrentFingerMovement = GetDirection(WordsMatrix.VarOut_TouchedIndices[0], touchedWordLetter.indices);
            //            WordsMatrix.VarOut_TouchedIndices.Add(touchedWordLetter.indices);
            //            touchedWordLetter.imgBG.color = colorTouched;
            //        }
            //        else if ((WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.Horizontal) || (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.HorizontalReverse))
            //        {
            //            touchedWordLetter = this.ColorNextTouchedObject(new KeyValuePair<int, int>(touchedWordLetter.indices.Key, WordsMatrix.VarOut_TouchedIndices[0].Value)/*, touchedWordLetter*/); ;
            //        }
            //        else if ((WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.Vertical) || (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.VerticalReverse))
            //        {
            //            touchedWordLetter = this.ColorNextTouchedObject(new KeyValuePair<int, int>(WordsMatrix.VarOut_TouchedIndices[0].Key, touchedWordLetter.indices.Value)/*, touchedWordLetter*/);
            //        }
            //        else if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.CrossFalling)
            //        {
            //            touchedWordLetter = this.ColorNextTouchedObjectCrossFalling(touchedWordLetter.indices);
            //        }
            //        else if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.CrossFallingReverse)
            //        {
            //            touchedWordLetter = this.ColorNextTouchedObjectCrossFallingReverse(touchedWordLetter.indices);
            //        }
            //        else if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.CrossRising)
            //        {
            //            touchedWordLetter = this.ColorNextTouchedObjectCrossRising(touchedWordLetter.indices);
            //        }
            //        else if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.CrossRisingReverse)
            //        {
            //            touchedWordLetter = this.ColorNextTouchedObjectCrossRisingReverse(touchedWordLetter.indices);
            //        }
            //        WordsMatrix.VarOut_LastTouchedObject = touchedWordLetter.gameObject.name;
            //        Debug.Log("NEW DIRECTION: " + WordsMatrix.VarOut_CurrentFingerMovement);
            //    }
            //}
        }
    }
}
