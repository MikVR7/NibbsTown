using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace NibbsTown
{
    internal class PanelsHandler : SerializedMonoBehaviour
    {
        internal enum PanelType
        {
            None = 0,
            Loading = 1,
            Login = 2,
            RallySelection = 3,
            //StartScreen = 4,
            RallyInfo = 5,
            RallyMap = 6,
            //Rally = 7,
        }

        internal static EventIn_OnBtnPanelBack EventIn_OnBtnPanelBack = new EventIn_OnBtnPanelBack();
        internal static EventIn_SetPanel EventIn_SetPanel = new EventIn_SetPanel();
        internal static EventOut_OnPanelChanged EventOut_OnPanelChanged = new EventOut_OnPanelChanged();

        [SerializeField] private Dictionary<PanelType, APanel> panels = new Dictionary<PanelType, APanel>();

        private List<PanelType> panelQueue = new List<PanelType>();
        private PanelType currentPanel = PanelType.None;

        internal void Init()
        {
            this.panels.Keys.ForEach(i => this.panels[i].Init(i));
            EventIn_OnBtnPanelBack.AddListenerSingle(OnBtnPanelBack);
            EventIn_SetPanel.AddListenerSingle(SetPanel);
            SetPanel(PanelType.Loading);
        }

        private void OnBtnPanelBack()
        {
            Debug.Log("THIS PANEL QUEUE COUNT: " + panelQueue.Count);
            if (this.panelQueue.Count >= 2)
            {
                PanelType previousPanelType = this.panelQueue[this.panelQueue.Count - 2];
                this.panelQueue.RemoveAt(this.panelQueue.Count - 1);
                this.panelQueue.RemoveAt(this.panelQueue.Count - 1);
                SetPanel(previousPanelType);
            }
        }

        private void SetPanel(PanelType nextPanel)
        {
            Debug.Log("SET PANEL: " + nextPanel);
            this.currentPanel = nextPanel;
            
            if (!currentPanel.Equals(PanelType.Loading))
            {
                AddPanelToPanelQueue(nextPanel);
            }
            this.panels.Keys.ForEach(i => this.panels[i].SetActive(nextPanel.Equals(i)));
            bool isActiveBtnBack = (this.panelQueue.Count > 1) /*&& (nextPanel != PanelType.Rally)*/;
            EventOut_OnPanelChanged.Invoke(this.currentPanel);
        }

        private void AddPanelToPanelQueue(PanelType nextPanel)
        {
            this.panelQueue.Add(nextPanel);
        }
    }
}
