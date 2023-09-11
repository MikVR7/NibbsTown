using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WordSearch
{
    internal class WordLetter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        internal enum LetterState
        {
            Idle = 0,
            Touched = 1,
            Correct = 2,
            Wrong = 3
        }

        [SerializeField] private TextMeshProUGUI tmpLetter = null;
        [SerializeField] private Image imgBG = null;
        [SerializeField] private Color colorIdle = Color.white;
        [SerializeField] private Color colorTouched = Color.white;
        [SerializeField] private Color colorCorrect = Color.white;
        [SerializeField] private Color colorWrong = Color.white;
        private (int, int) xyIndices = (0, 0);
        //private Coroutine coroutine = null;
        private Action<(int, int)> actionOnClick = null;
        private Action<(int, int)> actionOnDrag = null;
        private Action<(int, int)> actionOnRelease = null;
        

        //private bool isDragging = false;

        internal void Init((int, int) xyIndices, string letter, Action<(int, int)> actionOnClick, Action<(int, int)> actionOnDrag, Action<(int, int)> actionOnRelease)
        {
            this.tmpLetter.text = letter;
            this.xyIndices = xyIndices;
            this.actionOnClick = actionOnClick;
            this.actionOnDrag = actionOnDrag;
            this.actionOnRelease = actionOnRelease;
        }

        internal void Destroy()
        {
            //if (coroutine != null)
            //{
            //    StopCoroutine(coroutine);
            //    coroutine = null;
            //}
            Destroy(this.gameObject);
        }

        internal void SetLetterState(LetterState state)
        {
            if(state == LetterState.Idle)
            {
                this.imgBG.color = this.colorIdle;
            }
            else if (state == LetterState.Touched)
            {
                this.imgBG.color = this.colorTouched;
            }
            else if (state == LetterState.Correct)
            {
                this.imgBG.color = this.colorCorrect;
            }
            else if (state == LetterState.Wrong)
            {
                this.imgBG.color = this.colorWrong;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("EVENT DATA: " + eventData.button.ToString() + " " + eventData.pointerId);
            if (eventData.pointerId == 0)
            {
                this.actionOnClick.Invoke(this.xyIndices);
            }
            //if (this.coroutine == null)
            //{
            //    isDragging = true;
            //    ObjectTouched(eventData.pointerCurrentRaycast.gameObject);
            //}
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.pointerId == 0)
            {
                this.actionOnRelease.Invoke(this.xyIndices);
            }
            //isDragging = false;
            //WordsMatrix.EventIn_FinishedSelectingWord.Invoke(WordsMatrix.VarOut_TouchedIndices);

            //WordsMatrix.VarOut_TouchedIndices.Clear();
            //WordsMatrix.VarOut_CurrentFingerMovement = FingerMovement.None;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.pointerId == 0)
            {
                this.actionOnDrag.Invoke(this.xyIndices);
            }
            //if (isDragging)
            //{
            //    ObjectTouched(eventData.pointerCurrentRaycast.gameObject);
            //}
        }

        //private void ObjectTouched(GameObject touchedObject)
        //{
        //    //if (touchedObject != null && (touchedObject.name != WordsMatrix.VarOut_LastTouchedObject))
        //    //{
        //    //    WordLetter touchedWordLetter = touchedObject.GetComponent<WordLetter>();
        //    //    if (touchedWordLetter != null)
        //    //    {
        //    //        if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.None)
        //    //        {
        //    //            WordsMatrix.VarOut_CurrentFingerMovement = FingerMovement.Undecided;
        //    //            WordsMatrix.VarOut_TouchedIndices.Add(touchedWordLetter.indices);
        //    //            touchedWordLetter.imgBG.color = colorTouched;
        //    //        }
        //    //        else if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.Undecided)
        //    //        {
        //    //            //touchedWordLetter = FixCrossError(touchedWordLetter);
        //    //            WordsMatrix.VarOut_CurrentFingerMovement = GetDirection(WordsMatrix.VarOut_TouchedIndices[0], touchedWordLetter.indices);
        //    //            WordsMatrix.VarOut_TouchedIndices.Add(touchedWordLetter.indices);
        //    //            touchedWordLetter.imgBG.color = colorTouched;
        //    //        }
        //    //        else if ((WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.Horizontal) || (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.HorizontalReverse))
        //    //        {
        //    //            touchedWordLetter = this.ColorNextTouchedObject(new KeyValuePair<int, int>(touchedWordLetter.indices.Key, WordsMatrix.VarOut_TouchedIndices[0].Value)/*, touchedWordLetter*/); ;
        //    //        }
        //    //        else if ((WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.Vertical) || (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.VerticalReverse))
        //    //        {
        //    //            touchedWordLetter = this.ColorNextTouchedObject(new KeyValuePair<int, int>(WordsMatrix.VarOut_TouchedIndices[0].Key, touchedWordLetter.indices.Value)/*, touchedWordLetter*/);
        //    //        }
        //    //        else if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.CrossFalling)
        //    //        {
        //    //            touchedWordLetter = this.ColorNextTouchedObjectCrossFalling(touchedWordLetter.indices);
        //    //        }
        //    //        else if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.CrossFallingReverse)
        //    //        {
        //    //            touchedWordLetter = this.ColorNextTouchedObjectCrossFallingReverse(touchedWordLetter.indices);
        //    //        }
        //    //        else if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.CrossRising)
        //    //        {
        //    //            touchedWordLetter = this.ColorNextTouchedObjectCrossRising(touchedWordLetter.indices);
        //    //        }
        //    //        else if (WordsMatrix.VarOut_CurrentFingerMovement == FingerMovement.CrossRisingReverse)
        //    //        {
        //    //            touchedWordLetter = this.ColorNextTouchedObjectCrossRisingReverse(touchedWordLetter.indices);
        //    //        }
        //    //        WordsMatrix.VarOut_LastTouchedObject = touchedWordLetter.gameObject.name;
        //    //        Debug.Log("NEW DIRECTION: " + WordsMatrix.VarOut_CurrentFingerMovement);
        //    //    }
        //    //}
        //}
    }
}
