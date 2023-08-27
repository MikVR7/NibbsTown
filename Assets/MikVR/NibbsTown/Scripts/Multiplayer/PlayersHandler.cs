using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static NibbsTown.MapObjectStation;

namespace NibbsTown
{
    internal class PlayersHandler
    {
        private Player playerSelf = new Player();
        private List<Station> nextStations = new List<Station>();

        internal void Init() {
            PanelLogin.EventOut_UsernameChanged.AddListenerSingle(UsernameChanged);
            RallyTasksHandler.EventOut_FinishedRallyTasks.AddListenerSingle(FinishedRallyTasks);
        }

        private void CreateMapObjectCharacterSelf()
        {
            Debug.Log("Map obj created: character self");
            MapObjectsHandler.EventIn_AddMapObject.Invoke(GPSPosition.Zero, AMapObject.MapObjectType.Character, this.playerSelf);
            GPSHandler.EventOut_OnNewGPSCoordinates.AddListenerSingle(OnNewGPSCoordinates);
        }

        private void UsernameChanged(string username)
        {
            playerSelf.Name = username;
            playerSelf.Key = username; // TODO: chage that to individual key!
            this.CreateMapObjectCharacterSelf();
            StationsHandler.EventOut_StationIndexUpdated.AddListenerSingle(StationIDUpdated);
        }

        private void StationIDUpdated(List<Station> stations)
        {
            Debug.Log("STATION ID UPDATE: " + stations.Count);
            this.nextStations = stations;
        }

        private void OnNewGPSCoordinates(GPSPosition gpsPosition)
        {
            //Debug.Log("NEW GPS COORDINATE SET: " + gpsPosition.Latitude + " " + gpsPosition.Longitude);
            if (this.playerSelf.Key.Length == 0) { return; }
            MapObjectsHandler.EventIn_SetMapObjectPosition.Invoke(gpsPosition, AMapObject.MapObjectType.Character, this.playerSelf.Key);
            this.nextStations.ForEach(i => i.Distance = MapsHandler.VarOut_GetDistanceBetweenPoints(gpsPosition, i.Pos));
            Station closestStation = GetClosestStation();
            if(closestStation == null) { return; }
            if(closestStation.Distance <= 20)
            {
                Debug.Log("New station reached: " + closestStation.Index + " " + closestStation.Distance);
                GPSHandler.EventOut_OnNewGPSCoordinates.RemoveListener(OnNewGPSCoordinates);
                RallyTasksHandler.EventOut_TasksLoadingDone.AddListenerSingle(TasksLoadingDone);
                StationsHandler.EventIn_StationReached.Invoke(closestStation);
            }
        }

        private Station GetClosestStation()
        {
            if (nextStations.Count == 0) return null;
            return nextStations
                .Where(station => station.State != StationState.Done)
                .OrderBy(station => station.Distance)
                .FirstOrDefault();
        }

        private void TasksLoadingDone()
        {
            Debug.Log("Tasks loading done");
            RallyTasksHandler.EventOut_TasksLoadingDone.RemoveListener(TasksLoadingDone);
            RallyTasksHandler.EventIn_StartRallyTasks.Invoke();
        }

        private void FinishedRallyTasks()
        {
            Debug.Log("Finished rally task");
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallyMap);
            StationsHandler.EventIn_SetCurrentStationState.Invoke(MapObjectStation.StationState.Done);
            
            //PanelRallyInfo.EventIn_DisplayRallyInfo.Invoke()
            GPSHandler.EventOut_OnNewGPSCoordinates.AddListenerSingle(OnNewGPSCoordinates);
        }
    }
}
