using System.Collections.Generic;
using UnityEngine;

namespace NibbsTown
{
    internal class StationsHandler
    {
        internal EventIn_LoadStations EventIn_LoadStations = new EventIn_LoadStations();
        internal static EventIn_StationReached EventIn_StationReached = new EventIn_StationReached();
        internal static EventIn_SetCurrentStationState EventIn_SetCurrentStationState = new EventIn_SetCurrentStationState();
        internal static EventOut_StationsLoadingDone EventOut_StationsLoadingDone = new EventOut_StationsLoadingDone();
        internal static EventOut_StationIndexUpdated EventOut_StationIndexUpdated = new EventOut_StationIndexUpdated();
        internal static Dictionary<string, Station> VarOut_Stations { get; private set; } = new Dictionary<string, Station> { };
        internal static List<Station> VarOut_CurrentStations { get; set; } = new List<Station>();
        private int currentStationIndex = -1;
        private RallyTasksHandler rallyTasksHandler = new RallyTasksHandler();
        private Station reachedStation = null;
        
        internal void Init() {
            this.rallyTasksHandler.Init();
            EventIn_StationReached.AddListenerSingle(StationReached);
            EventIn_SetCurrentStationState.AddListenerSingle(SetCurrentStationState);
            EventIn_LoadStations.AddListenerSingle(LoadStations);
        }

        internal void LoadStations()
        {
            OverlaysHandler.EventIn_DisplayOverlay.Invoke(OverlaysHandler.OverlayType.Waiting, "Loading rally ...", null, null);
            VarOut_Stations.Clear();
            VarOut_CurrentStations.Clear();
            MapObjectsHandler.EventIn_RemoveAllMapObjectsStation.Invoke();
            DatabaseRallies.EventInOut_LoadDBStations.Invoke(RalliesHandler.VarOut_CurrentRallyKey, OnLoadingStationsDone);
        }

        private void OnLoadingStationsDone(Dictionary<string, Station> response)
        {
            Debug.Log("Loading station done " + response.Count);
            VarOut_Stations = response;
            Rally rally = RalliesHandler.VarOut_CurrentRally();
            MapsHandler.EventIn_SetMapPosition.Invoke(rally.CenterPos);
            MapsHandler.EventIn_SetMapZoom.Invoke(rally.Zoom);
            List<string> finishedStations = PlayerPrefsHandler.VarOut_GetRallyStationKeys(rally.Key);
            foreach (string key in VarOut_Stations.Keys)
            {
                VarOut_Stations[key].Key = key;
                MapObjectStation.StationState state = finishedStations.Contains(key) ?
                    MapObjectStation.StationState.Done : MapObjectStation.StationState.Idle;
                VarOut_Stations[key].State = state;
                MapObjectsHandler.EventIn_AddMapObject.Invoke(VarOut_Stations[key].Pos, AMapObject.MapObjectType.Station, VarOut_Stations[key]);
                MapObjectsHandler.EventIn_SetMapObjectStationState.Invoke(key, state);
            }
            currentStationIndex = -1;

            EventOut_StationsLoadingDone.Invoke();
            OverlaysHandler.EventIn_HideOverlay.Invoke();
        }

        private void SetCurrentStationState(MapObjectStation.StationState stationState)
        {
            if (!RalliesHandler.VarOut_CurrentRally().Done)
            {
                if (this.reachedStation != null)
                {
                    this.reachedStation.State = stationState;
                    MapObjectsHandler.EventIn_SetMapObjectStationState.Invoke(this.reachedStation.Key, stationState);
                }
                if (stationState == MapObjectStation.StationState.Done)
                {
                    if (this.reachedStation != null)
                    {
                        PlayerPrefsHandler.EventIn_SetFinishedRallyStation.Invoke(RalliesHandler.VarOut_CurrentRallyKey, reachedStation.Key);
                    }
                    this.reachedStation = null;
                    IncrementStationIndex();
                }
            }
        }

        private void StationReached(Station station)
        {
            this.reachedStation = station;
            this.reachedStation.State = MapObjectStation.StationState.Reached;
            RallyTasksHandler.EventIn_LoadTasks.Invoke(station);
        }

        private void IncrementStationIndex()
        {
            bool areLeftWithSameIndex = this.AreLeftWithSameIndex();
            if (!areLeftWithSameIndex)
            {
                currentStationIndex = this.GetNextLowestIndex();

                if (currentStationIndex < 0) {
                    RalliesHandler.EventIn_RallyFinished.Invoke();
                    return;
                }

                foreach (string key in VarOut_Stations.Keys)
                {
                    if ((VarOut_Stations[key].Index == currentStationIndex) &&
                        (VarOut_Stations[key].State != MapObjectStation.StationState.Done))
                    {
                        VarOut_Stations[key].State = MapObjectStation.StationState.Active;
                        MapObjectsHandler.EventIn_SetMapObjectStationState.Invoke(key, VarOut_Stations[key].State);
                    }
                }
            }

            List<Station> stations = new List<Station>();
            foreach (string key in VarOut_Stations.Keys)
            {
                if (VarOut_Stations[key].Index.Equals(currentStationIndex) &&
                    (VarOut_Stations[key].State != MapObjectStation.StationState.Done))
                {
                    stations.Add(VarOut_Stations[key]);
                }
            }
            EventOut_StationIndexUpdated.Invoke(stations);
            Debug.Log("Current station index: " + this.currentStationIndex);
        }

        private bool AreLeftWithSameIndex()
        {
            foreach (string key in VarOut_Stations.Keys)
            {
                if ((VarOut_Stations[key].Index == currentStationIndex) &&
                    (VarOut_Stations[key].State != MapObjectStation.StationState.Done))
                {
                    return true;
                }
            }
            return false;
        }

        private int GetNextLowestIndex()
        {
            // Get the current station ID
            int currentIndex = currentStationIndex;

            // Initialize the next ID to -1 (which will be the value if there's no higher ID)
            int nextIndex = -1;

            // Iterate over each station in the dictionary
            foreach (KeyValuePair<string, Station> entry in VarOut_Stations)
            {
                if (entry.Value.State == MapObjectStation.StationState.Done) { continue; }

                // Get the station ID
                int stationIndex = entry.Value.Index;

                // Check if the station ID is higher than the current ID
                if (stationIndex > currentIndex)
                {
                    // If this is the first higher ID we've found, or if it's lower than the current next ID,
                    // update the next ID
                    if (nextIndex == -1 || stationIndex < nextIndex)
                    {
                        nextIndex = stationIndex;
                    }
                }
            }
            return nextIndex;
        }
    }
}
