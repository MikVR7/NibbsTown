//using Sirenix.OdinInspector;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//namespace NibbsTown
//{
//    internal class ScrollviewContent : MonoBehaviour
//    {
//        private RectTransform myRectTransform = null;
//        private float contentWidth = 0f;
//        private float contentHeight = 0f;
//        [SerializeField] private float imgBorderPercent = 0.5f;
//        [SerializeField] private float spacing = 10f;
//        [SerializeField] private float borderTop = 0f;
//        [SerializeField] private float borderBottom = 0f;
//        [SerializeField] private float borderLeft = 0f;
//        [SerializeField] private float borderRight = 0f;
//        private List<RectTransform> rtChildren = new List<RectTransform>();

//        internal void Init()
//        {
//            if (this.myRectTransform == null)
//            {
//                this.myRectTransform = this.GetComponent<RectTransform>();
//            }
//            this.rtChildren = this.myRectTransform.GetComponentsInChildren<RectTransform>().Skip(1).ToList();
//            this.OnUpdateScrollviewContent();
//        }

//        private void OnRectTransformDimensionsChange()
//        {
//            if (this.gameObject.activeInHierarchy)
//            {
//                Init();
//            }
//        }

//        [Button("Update")]
//        private void OnBtnUpdate()
//        {
//            this.Init();
//        }

//        private void OnUpdateScrollviewContent()
//        {
//            this.contentWidth = this.myRectTransform.rect.width;
//            this.contentHeight = this.borderTop;

//            // get all active child elements
//            for (int i = 0; i < rtChildren.Count; i++)
//            {
//                if (rtChildren[i].gameObject.activeSelf)
//                {
//                    this.FitElement(rtChildren[i], ((rtChildren.Count - 1) == i));
//                }
//            }
//            this.myRectTransform.sizeDelta = new Vector2(this.myRectTransform.sizeDelta.x, this.contentHeight);
//        }

//        private float height = 0f;
//        private void FitElement(RectTransform rtChild, bool isLastElement)
//        {
//            RawImage rawImageChild = rtChild.GetComponent<RawImage>();
//            rtChild.anchorMin = new Vector2(0, 1);
//            rtChild.anchorMax = new Vector2(0, 1);
//            rtChild.pivot = new Vector2(0, 1);
//            this.height = 0f;
//            if ((rawImageChild != null) && (rawImageChild.texture != null))
//            {
//                float heightMultiplier = (float)rawImageChild.texture.width / (float)rawImageChild.texture.height;
//                height = Mathf.Abs(this.contentWidth / heightMultiplier) * this.imgBorderPercent;
//                rtChild.anchoredPosition = new Vector2((this.contentWidth * (1f - this.imgBorderPercent)) * 0.5f, -this.contentHeight);
//                rtChild.sizeDelta = new Vector2(this.contentWidth * this.imgBorderPercent, height);
//                this.AddBordersLeftRight(rtChild);
//            }
//            else
//            {
//                /*StartCoroutine*/
//                SetTextMeshPro(rtChild);
//            }
//            this.contentHeight += height;
//            if (isLastElement)
//            {
//                this.contentHeight += this.borderBottom;
//            }
//            else
//            {
//                this.contentHeight += spacing;
//            }
//        }

//        private void AddBordersLeftRight(RectTransform rtChild)
//        {
//            rtChild.anchoredPosition = new Vector2(rtChild.anchoredPosition.x + this.borderLeft, rtChild.anchoredPosition.y);
//            rtChild.sizeDelta = new Vector2(rtChild.sizeDelta.x - (this.borderLeft + this.borderRight), rtChild.sizeDelta.y);
//        }

//        private void SetTextMeshPro(RectTransform rtChild)
//        {
//            //yield return new WaitForEndOfFrame();

//            TextMeshProUGUI tmpChild = rtChild.GetComponent<TextMeshProUGUI>();
//            if (tmpChild != null)
//            {
//                rtChild.sizeDelta = new Vector2(this.contentWidth, rtChild.sizeDelta.y);
//                //yield return new WaitForEndOfFrame();
//                height = tmpChild.GetPreferredValues().y;// tmpChild.text, float.PositiveInfinity, float.PositiveInfinity).y;
//                //float padding = 0;
//                //float minHeight = 0;
//                ////var width = Mathf.Max(preferredValues.x + padding * 2, minWidth);
//                //height = Mathf.Max(preferredValues.y + padding * 2, minHeight);
//                //Vector2 bgSize = new Vector2(width, height);


//                Debug.Log("HEIGHT: " + height + " " + tmpChild.preferredHeight + " " + tmpChild.flexibleHeight + " " + tmpChild.maxHeight + " " + tmpChild.renderedHeight);
//                rtChild.anchoredPosition = new Vector2(0f, -this.contentHeight);
//                rtChild.sizeDelta = new Vector2(rtChild.sizeDelta.x, height);
//                this.AddBordersLeftRight(rtChild);
//            }
//        }
//    }
//}
