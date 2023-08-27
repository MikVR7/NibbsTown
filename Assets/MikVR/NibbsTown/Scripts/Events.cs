using CodeEvents;
using System;
using System.Collections.Generic;
using UnityEngine;
using static NibbsTown.AMapObject;
using static NibbsTown.MapObjectStation;
using static NibbsTown.RallyCreator;

namespace NibbsTown
{
    // TownRallyHandler.cs
    internal class EventOut_OnUpdate : EventSystem { }
    internal class EventOut_OnUpdateAt2 : EventSystem { }
    internal class EventOut_OnUpdateAt50 : EventSystem { }

    // TaskBarHandler.cs
    internal class EventIn_SetActiveBtnBack : EventSystem <bool> { }
    internal class EventIn_SetTaskBarUsername : EventSystem<string> { }
    internal class EventIn_SetTaskBarRallyname : EventSystem<string> { }
    internal class EventOut_OnBtnDebug : EventSystem { }

    // PanelsHandlerBody.cs
    internal class EventIn_OnBtnPanelBack : EventSystem { }
    internal class EventIn_SetPanel : EventSystem<PanelsHandler.PanelType> { }
    internal class EventOut_OnPanelChanged : EventSystem<PanelsHandler.PanelType> { }

    // RalliesHandler.cs
    //internal class EventIn_SetCurrentRally : EventSystem<int> { }
    //internal class EventOut_RallyChanged : EventSystem<Rally> { }
    internal class EventIn_LoadRalliesList : EventSystem { }
    internal class EventIn_SetCurrentRally : EventSystem<string> { }
    internal class EventIn_RallyFinished : EventSystem { }
    internal class EventOut_RalliesLoadingDone : EventSystem { }
    internal class EventOut_StationsLoadingDone : EventSystem { }
    internal class EventOut_TasksLoadingDone : EventSystem { }
    internal class EventOut_StationIndexUpdated : EventSystem<List<Station>> { }
    //internal class EventIn_CurrentTaskFinished : EventSystem { }
    //internal class EventOut_RallyStationTaskChanged : EventSystem { }
    internal class EventIn_LoadStations : EventSystem { }
    //internal class EventIn_IncrementStationIndex : EventSystem { }
    internal class EventIn_LoadTasks : EventSystem<Station> { }
    internal class EventIn_StartRallyTasks : EventSystem { }
    internal class EventIn_FinishedRallyTask : EventSystem { }
    internal class EventOut_FinishedRallyTasks : EventSystem { }

    // RallyCreator.cs
    internal class EventIn_AddNewRallyToServer : EventSystem<NewRallyType> { }

    // OverlayHandler.cs
    internal class EventIn_DisplayOverlay : EventSystem<OverlaysHandler.OverlayType, string, Action, Action> { }
    internal class EventIn_HideOverlay : EventSystem { }

    // OverlayConfirmation.cs
    internal class EventIn_DisplayOverlaySpecific : EventSystem<string, Action, Action> { }

    // MapsHandler.cs
    internal class EventIn_SetMapPosition : EventSystem<GPSPosition> { }
    internal class EventIn_SetMapCurrentGPSPosition : EventSystem { }
    internal class EventIn_DisplayMap : EventSystem<bool> { }
    internal class EventIn_SetMapZoom : EventSystem<float> { }
    internal class EventOut_MapOnChangeZoom : EventSystem<float> { }
    internal class EventOut_OnMapClick : EventSystem<GPSPosition> { }
    internal class EventOut_OnMapChangedPosition : EventSystem { }

    // MapObjectsHandler.cs
    internal class EventIn_RemoveAllMapObjectsStation : EventSystem { }
    internal class EventIn_AddMapObject : EventSystem<GPSPosition, MapObjectType, object> { }
    internal class EventIn_SetMapObjectPosition : EventSystem<GPSPosition, MapObjectType, string> { }
    internal class EventIn_SetMapObjectStationState : EventSystem<string, StationState> { }

    // DatabaseHandler.cs
    internal class EventIn_SaveRallyWhole : EventSystem<string, Rally, Dictionary<string, Station>, Dictionary<string, RallyTask>> { }
    internal class EventIn_TestDB : EventSystem { }
    internal class EventInOut_LoadDBRalliesAll : EventSystem<Action<Dictionary<string, Rally>>> { }
    internal class EventInOut_LoadDBStations : EventSystem<string, Action<Dictionary<string, Station>>> { }
    internal class EventInOut_LoadDBTasks : EventSystem<string, string, Action<Dictionary<string, RallyTask>>> { }
    internal class EventIn_UploadImage : EventSystem<string, Texture2D> { }
    internal class EventInOut_LoadImage : EventSystem<string, Action<Texture2D>> { }
    internal class EventInOut_LoadAllPlayers : EventSystem<Action<Dictionary<string, Group>>> { }
    internal class EventIn_AddPlayer : EventSystem<Player> { }
    internal class EventIn_RemovePlayer : EventSystem<string> { }

    // DatabaseRallies.cs
    internal class EventIn_DeleteAllRalliesFromDB : EventSystem { }
    internal class EventIn_AddRallyToDB : EventSystem<Rally> { }

    // Settings.cs
    //internal class EventOut_SettingsChangedStr :EventSystem<string> { }
    //internal class EventOut_SettingsChangedInt : EventSystem<int> { }
    //internal class EventOut_SettingsChangedBool : EventSystem<bool> { }
    internal class EventOut_ValueChangedEvent : EventSystem { }

    // PanelLogin.cs
    internal class EventOut_UsernameChanged : EventSystem<string> { }

    // PanelRallyInfo.cs
    internal class EventIn_DisplayRallyInfo : EventSystem<Description[]> { }

    // GPSHandler.cs
    //internal class EventIn_StartGPSConnection : EventSystem { }
    //internal class EventIn_StopGPSConnection : EventSystem { }
    internal class EventOut_OnNewGPSCoordinates : EventSystem<GPSPosition> { }

    // OffscreenIndicatorsHandler.cs
    internal class EventIn_CreateOffscreenIndicator : EventSystem<AMapObject> { }
    internal class EventIn_DeleteOffscreenIndicator : EventSystem<string> { }
    internal class EventIn_DeleteAllOffscreenIndicators : EventSystem { }

    // MapObjectStation.cs
    internal class EventIn_SetStationData : EventSystem<Station> { }

    // StationsHandler.cs
    internal class EventIn_StationReached : EventSystem<Station> { }
    internal class EventIn_SetCurrentStationState : EventSystem<StationState> { }

    // AUIElement.cs
    internal class EventOut_ElementHeightChanged : EventSystem { }

    // PlayerPrefsHandler.cs
    internal class EventIn_SetFinishedRallyStation : EventSystem<string, string> { }
    internal class EventIn_DeleteFinishedRallyStations : EventSystem<string> { }

    // TaskScenesHandler.cs
    internal class EventIn_StartTaskScene : EventSystem<RallyTask> { }
    internal class EventOut_FinishedTaskScene : EventSystem { }
}
