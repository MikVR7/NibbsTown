using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace NibbsTown
{
    internal class RalliesHandler
    {
        private static RalliesHandler Instance = null;
        internal static EventIn_LoadRalliesList EventIn_LoadRalliesList = new EventIn_LoadRalliesList();
        internal static EventIn_SetCurrentRally EventIn_SetCurrentRally = new EventIn_SetCurrentRally();
        internal static EventIn_RallyFinished EventIn_RallyFinished = new EventIn_RallyFinished();
        internal static EventOut_RalliesLoadingDone EventOut_RalliesLoadingDone = new EventOut_RalliesLoadingDone();
        internal static readonly string NO_RALLY = "no_rally_running_243546";
        
        internal static Dictionary<string, Rally> VarOut_GetRallies() { return Instance.rallies; }
        
        internal static Rally VarOut_GetRallyByKey(string rallyKey) { return Instance.rallies[rallyKey]; }
        internal static string VarOut_GetIDByRally(Rally rally) {
            foreach (string key in Instance.rallies.Keys)
            {
                if (Instance.rallies[key].Equals(rally))
                {
                    return key;
                }
            }
            return RalliesHandler.NO_RALLY;
        }

        private Dictionary<string, Rally> rallies { get; set; } = new Dictionary<string, Rally>();
        internal static string VarOut_CurrentRallyKey { get; private set; } = string.Empty;
        private RallyCreator rallyCreator = new RallyCreator();
        private StationsHandler stationsHandler = new StationsHandler();

        internal static Rally VarOut_CurrentRally()
        {
            if (string.IsNullOrEmpty(VarOut_CurrentRallyKey)) { return new Rally() { Name = NO_RALLY }; }
            return Instance.rallies[VarOut_CurrentRallyKey];
        }

        internal void Init()
        {
            Instance = this;
            this.rallyCreator.Init();
            this.stationsHandler.Init();
            EventIn_LoadRalliesList.AddListenerSingle(LoadRalliesList);
            EventIn_SetCurrentRally.AddListenerSingle(SetCurrentRally);
            EventIn_RallyFinished.AddListenerSingle(RallyFinished);
        }

        private void LoadRalliesList()
        {
            DatabaseRallies.EventInOut_LoadDBRalliesAll.Invoke(OnLoadingRalliesDone);
        }

        private void OnLoadingRalliesDone(Dictionary<string, Rally> response)
        {
            Debug.Log("Loading rallies done.");
            this.rallies = response;
            foreach(string key in this.rallies.Keys)
            {
                this.rallies[key].Key = key;
            }
            EventOut_RalliesLoadingDone.Invoke();
        }
        
        private void SetCurrentRally(string rallyKey)
        {
            VarOut_CurrentRallyKey = rallyKey;
            VarOut_CurrentRally().Done = false;
            this.stationsHandler.EventIn_LoadStations.Invoke();
        }

        private void RallyFinished()
        {
            Debug.Log("Rally finished: " + VarOut_CurrentRally().Name);
            VarOut_CurrentRally().Done = true;
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallyInfo);
            PanelRallyInfo.EventIn_DisplayRallyInfo.Invoke(VarOut_CurrentRally().Endscreen);
            PlayerPrefsHandler.EventIn_DeleteFinishedRallyStations.Invoke(VarOut_CurrentRally().Key);
        }
    }
}
