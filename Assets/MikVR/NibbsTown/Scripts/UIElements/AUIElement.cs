using System;
using System.Collections.Generic;
using UnityEngine;

namespace NibbsTown
{
    internal abstract class AUIElement : MonoBehaviour
    {
        internal EventOut_ElementHeightChanged EventOut_ElementHeightChanged = new EventOut_ElementHeightChanged();

        internal static readonly Dictionary<Description.DescriptionType, Type> UIELEMENT_TYPE =
            new Dictionary<Description.DescriptionType, Type>()
            {
                { Description.DescriptionType.Image, typeof(UIElementInfoPicture) },
                { Description.DescriptionType.Text, typeof(UIElementInfoText) },
            };

        internal Description.DescriptionType ElementType { get; set; }
        private float elementHeight = 0f;
        protected void SetElementHeight(float elementHeight)
        {
            this.elementHeight = elementHeight;
            EventOut_ElementHeightChanged.Invoke();
        }
        public float VarOut_ElementHeight()
        {
            return this.elementHeight;
        }

        protected string descriptionData = string.Empty;
        protected int elementIndex = 0;
        protected RectTransform myRectTransform = null;

        internal virtual void Init(int elementIndex, Description.DescriptionType descriptionType, string descriptionData)
        {
            this.myRectTransform = this.GetComponent<RectTransform>();
            this.descriptionData = descriptionData;
            this.ElementType = descriptionType;
            this.elementIndex = elementIndex;
            this.gameObject.name = ElementType.ToString().ToLower() + "_" + elementIndex;
        }

        internal virtual void DestroyElement()
        {
            Destroy(this.gameObject);
        }

        internal void SetPosition(float yPosition)
        {
            this.myRectTransform.anchoredPosition = new Vector2(this.myRectTransform.anchoredPosition.x, yPosition); 
        }
    }
}
