using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NibbsTown
{
    // MAP STYLES: https://www.lockedownseo.com/adding-custom-styles-to-google-maps/
    // MAP STYLES: https://snazzymaps.com/
    // https://snazzymaps.com/style/287720/modest

    internal class MapObjectStation : AMapObject
    {
        internal enum StationState
        {
            Idle = 0,
            Active = 1,
            Reached = 2,
            Done = 3,
        }
        internal EventIn_SetStationData EventIn_SetStationData = new EventIn_SetStationData();
        internal EventIn_SetCurrentStationState EventIn_SetStationState = new EventIn_SetCurrentStationState();

        [SerializeField] private TextMeshPro tmpHeader = null;
        [SerializeField] private GameObject goHeader = null;
        [SerializeField] private GameObject sonarIndicator = null;
        [SerializeField] private Material matSonarIndicator = null;
        [SerializeField] private SpriteRenderer spriteTheme = null;
        [SerializeField] private SpriteRenderer spriteContent = null;
        [SerializeField] private SpriteRenderer spriteCheckmark = null;
        
        [SerializeField] private Dictionary<StationState, Color> cTheme = new Dictionary<StationState, Color>();
        [SerializeField] private Dictionary<StationState, Color> cContent = new Dictionary<StationState, Color>();
        //[SerializeField] internal Station VarOut_StationData = null;
        private string stationKey = string.Empty;
        private StationState stationState = StationState.Idle;

        internal override void Init(OnlineMapsMarker3D marker)
        {
            this.objectType = MapObjectType.Station;
            base.Init(marker);
            this.EventIn_SetStationState.AddListenerSingle(SetStationState);
            this.EventIn_SetStationData.AddListenerSingle(SetStationData);
            this.matSonarIndicator.renderQueue = 2000;
        }

        internal override void DestroyInstance()
        {
            base.DestroyInstance();
            this.EventIn_SetStationState.RemoveListener(SetStationState);
            this.EventIn_SetStationData.RemoveListener(SetStationData);
        }

        internal void SetStationData(Station stationData)
        {
            //this.VarOut_StationData = stationData;
            this.stationKey = stationData.Key;
            this.tmpHeader.text = stationData.Name;
            //SetStationState(stationData.State); 
        }

        private void OnEnable()
        {
            if (!string.IsNullOrEmpty(this.stationKey))
            {
                SetStationState(this.stationState);
            }
        }

        private void SetStationState(StationState state)
        {
            Debug.Log("SET STATION STATE: " + state + " " + this.stationKey);

            this.stationState = state;
            //StationsHandler.EventIn_SetCurrentStationState.Invoke(state);
            this.goHeader.SetActive(state == StationState.Active);
            this.sonarIndicator.SetActive(state == StationState.Active);
            this.spriteContent.color = this.cContent[state];
            this.spriteTheme.color = this.cTheme[state];
            this.spriteCheckmark.enabled = state == StationState.Done;
        }
    }
}
