//using Sirenix.Utilities;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//namespace NibbsTown
//{
//    internal class PanelStartScreen : APanel
//    {
//        private enum BtnType
//        {
//            Info = 0,
//            Map = 1,
//            StartRally = 2,
//            ContinueRally = 3,
//            EndRally = 4,
//            PauseRally = 5,
//            ResumeRally = 6,
//        }

//        [SerializeField] private Button btnInfo = null;
//        [SerializeField] private Button btnMap = null;
//        [SerializeField] private Button btnStartRally = null;
//        [SerializeField] private Button btnContinueRally = null;
//        [SerializeField] private Button btnEndRally = null;
//        [SerializeField] private Button btnPauseRally = null;
//        [SerializeField] private Button btnResumeRally = null;

//        private Dictionary<BtnType, Button> buttons = new Dictionary<BtnType, Button>();

//        internal override void Init(PanelsHandler.PanelType panelType)
//        {
//            base.Init(panelType);

//            this.buttons.Clear();
//            this.buttons.Add(BtnType.Info, btnInfo);
//            this.buttons.Add(BtnType.Map, btnMap);
//            this.buttons.Add(BtnType.StartRally, btnStartRally);
//            this.buttons.Add(BtnType.ContinueRally, btnContinueRally);
//            this.buttons.Add(BtnType.EndRally, btnEndRally);
//            this.buttons.Add(BtnType.PauseRally, btnPauseRally);

//            this.btnInfo.onClick.AddListener(OnBtnInfo);
//            this.btnMap.onClick.AddListener(OnBtnMap);
//            this.btnStartRally.onClick.AddListener(OnBtnStartRally);
//            this.btnContinueRally.onClick.AddListener(OnBtnContinueRally);
//            this.btnEndRally.onClick.AddListener(OnBtnEndRally);
//            this.btnPauseRally.onClick.AddListener(OnBtnPauseRally);
//            this.btnResumeRally.onClick.AddListener(OnBtnResumeRally);

//            StationsHandler.EventOut_StationsLoadingDone.AddListenerSingle(OnStationsLoadingDone);
//        }

//        internal void DestroyObject()
//        {
//            this.buttons.Keys.ForEach(i => this.buttons[i].onClick.RemoveAllListeners());
//            StationsHandler.EventOut_StationsLoadingDone.RemoveListener(OnStationsLoadingDone);
//        }

//        private void OnEnable()
//        {
//            UpdateButtons();
//        }

//        private void OnStationsLoadingDone()
//        {
//            UpdateButtons();
//        }

//        private void UpdateButtons()
//        {
//            Rally currentRally = RalliesHandler.VarOut_CurrentRally();
//            string currentRallyId = RalliesHandler.VarOut_GetIDByRally(currentRally);
//            bool isRallyRunning = currentRally.Name.Equals(RalliesHandler.NO_RALLY);
//            bool rallyStartedPreviously = (currentRallyId != RalliesHandler.NO_RALLY) &&
//                PlayerPrefsHandler.VarOut_RallyStartedPreviously(currentRallyId);

//            this.btnInfo.gameObject.SetActive(RalliesHandler.VarOut_GetStations().Count > 0);
//            this.btnMap.gameObject.SetActive(RalliesHandler.VarOut_GetStations().Count > 0);
//            this.btnContinueRally.gameObject.SetActive(isRallyRunning);
//            this.btnEndRally.gameObject.SetActive(isRallyRunning);
//            this.btnPauseRally.gameObject.SetActive(isRallyRunning);
//            this.btnResumeRally.gameObject.SetActive(!isRallyRunning && rallyStartedPreviously && (RalliesHandler.VarOut_GetStations().Count > 0));
//            this.btnStartRally.gameObject.SetActive(!isRallyRunning && (RalliesHandler.VarOut_GetStations().Count > 0));
//        }

//        private void OnBtnInfo()
//        {
//            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallyInfo);
//        }

//        private void OnBtnMap()
//        {
//            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallyMap);
//        }

//        private void OnBtnStartRally()
//        {
//            ////PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.Rally);
//            //this.btnContinueRally.gameObject.SetActive(true);
//            //this.btnEndRally.gameObject.SetActive(true);
//            //this.btnPauseRally.gameObject.SetActive(true);
//        }

//        private void OnBtnContinueRally()
//        {
            
//        }

//        private void OnBtnPauseRally()
//        {

//        }

//        private void OnBtnResumeRally()
//        {

//        }

//        private void OnBtnEndRally()
//        {

//        }
//    }
//}
