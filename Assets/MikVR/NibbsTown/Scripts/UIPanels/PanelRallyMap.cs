//using AndroidServices;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NibbsTown
{
    internal class PanelRallyMap : APanel
    {
        [SerializeField] private Button btnGotoMap = null;
        [SerializeField] private Button btnGotoMypos = null;
        [SerializeField] private Button btnGotoRallySelection = null;
        [SerializeField] private GameObject goBtnGotoRallySelection = null;
        [SerializeField] private TextMeshProUGUI tmpHeader = null;

        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            this.btnGotoMap.onClick.AddListener(OnBtnGotoMap);
            this.btnGotoMypos.onClick.AddListener(OnBtnGotoMypos);
            this.btnGotoRallySelection.onClick.AddListener(OnBtnGotoRallySelection);
            RalliesHandler.EventIn_RallyFinished.AddListenerSingle(RallyFinished);
        }

        private void OnEnable()
        {
            MapsHandler.EventIn_DisplayMap.Invoke(true);
            string suffixDone = RalliesHandler.VarOut_CurrentRally().Done ? " - Abgeschlossen" : string.Empty;
            this.tmpHeader.text = RalliesHandler.VarOut_CurrentRally().Name + suffixDone;
            this.goBtnGotoRallySelection.SetActive(RalliesHandler.VarOut_CurrentRally().Done);
            Debug.Log("RalliesHandler.VarOut_CurrentRally().Done: " + RalliesHandler.VarOut_CurrentRally().Done);
            //RalliesHandler.VarOut_CurrentRally().
        }

        private void OnDisable()
        {
            MapsHandler.EventIn_DisplayMap.Invoke(false);
        }

        //private void OnSliderZoomChanged(float value)
        //{
        //    float zoom = ((1f-value) * (maxMapZoom - minMapZoom)) + minMapZoom;
        //        //float zoom = (value - 0f) * (maxMapZoom - minMapZoom) / (1f - 0f) + minMapZoom;
        //    MapsHandler.EventIn_SetMapZoom.Invoke(zoom);
        //}

        //private void MapOnChangeZoom(float zoom) {
        //    float newValue = 1f-((zoom - minMapZoom) / (maxMapZoom - minMapZoom));
        //    sliderZoom.value = newValue;
        //}

        private void OnBtnGotoMap()
        {
            Rally rally = RalliesHandler.VarOut_CurrentRally();
            MapsHandler.EventIn_SetMapPosition.Invoke(rally.CenterPos);
            MapsHandler.EventIn_SetMapZoom.Invoke(rally.Zoom);
        }

        private void OnBtnGotoMypos()
        {
            Rally rally = RalliesHandler.VarOut_CurrentRally();
            MapsHandler.EventIn_SetMapCurrentGPSPosition.Invoke();
            MapsHandler.EventIn_SetMapZoom.Invoke(rally.Zoom);
        }

        private void OnBtnGotoRallySelection()
        {
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallySelection);
        }

        private void RallyFinished()
        {
            this.goBtnGotoRallySelection.SetActive(true);
        }
    }
}
