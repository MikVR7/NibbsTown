using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace NibbsTown
{
    internal class PanelRallyInfo : APanel
    {
        internal static EventIn_DisplayRallyInfo EventIn_DisplayRallyInfo = new EventIn_DisplayRallyInfo();

        [SerializeField] private RectTransform rtElementsParent = null;
        [SerializeField] private Dictionary<Description.DescriptionType, GameObject> prefabsUIElements = new Dictionary<Description.DescriptionType, GameObject>();
        [SerializeField] private RectTransform rtEndElement = null;
        [SerializeField] private Button btnContinue = null;
        [SerializeField] private ScrollRect scrollRect = null;
        [SerializeField] private float elementsSpacing = 0f;
        private List<AUIElement> infoElements = new List<AUIElement>();

        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            EventIn_DisplayRallyInfo.AddListenerSingle(DisplayRallyInfo);
            this.btnContinue.onClick.AddListener(OnBtnContinue);
        }

        private void DisplayRallyInfo(Description[] descriptions) {
            // delete old elements
            this.infoElements.ForEach(i => i.DestroyElement());
            this.infoElements.Clear();

            Debug.Log("DISPLAY RALLY INFO: " + descriptions.Length);

            // create new Elements
            for (int i = 0; i < descriptions.Length; i++) { this.CreateNewInfoElement(i, descriptions[i]); }
            this.RearangeElements();
        }

        private void CreateNewInfoElement(int index, Description rallyDescription)
        {
            Type elementType = AUIElement.UIELEMENT_TYPE[rallyDescription.Type];
            GameObject goUIElement = Instantiate(this.prefabsUIElements[rallyDescription.Type], this.rtElementsParent);
            AUIElement uiElement = (goUIElement.GetComponent(elementType) as AUIElement);
            uiElement.Init(index, rallyDescription.Type, rallyDescription.Data);
            this.infoElements.Add(uiElement);
            uiElement.EventOut_ElementHeightChanged.AddListener(RearangeElements);
        }

        private void RearangeElements()
        {
            float currentPosition = elementsSpacing;
            for(int i = 0; i < this.infoElements.Count; i++)
            {
                this.infoElements[i].SetPosition(-currentPosition);
                currentPosition += (this.infoElements[i].VarOut_ElementHeight() + elementsSpacing);
            }
            this.rtEndElement.anchoredPosition = new Vector2(this.rtEndElement.anchoredPosition.x, -currentPosition);
            scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, currentPosition + this.rtEndElement.sizeDelta.y);
        }

        private void OnBtnContinue()
        {
            RallyTasksHandler.EventIn_FinishedRallyTask.Invoke();
        }
    }
}
