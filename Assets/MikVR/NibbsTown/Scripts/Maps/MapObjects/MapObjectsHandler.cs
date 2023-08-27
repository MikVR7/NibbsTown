using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using static NibbsTown.AMapObject;
using static NibbsTown.MapObjectStation;

namespace NibbsTown
{
    internal class MapObjectsHandler : SerializedMonoBehaviour
    {
        internal static EventIn_RemoveAllMapObjectsStation EventIn_RemoveAllMapObjectsStation = new EventIn_RemoveAllMapObjectsStation();
        internal static EventIn_AddMapObject EventIn_AddMapObject = new EventIn_AddMapObject();
        internal static EventIn_SetMapObjectPosition EventIn_SetMapObjectPosition = new EventIn_SetMapObjectPosition();
        internal static EventIn_SetMapObjectStationState EventIn_SetMapObjectStationState = new EventIn_SetMapObjectStationState();

        [SerializeField] private OffscreenIndicatorsHandler offscreenIndicatorsHandler = null;
        [SerializeField] private Dictionary<AMapObject.MapObjectType, GameObject> prefabsMapObjects = new Dictionary<AMapObject.MapObjectType, GameObject>();
        private Dictionary<MapObjectType, Dictionary<string, AMapObject>> mapObjects = new Dictionary<MapObjectType, Dictionary<string, AMapObject>>();
        private OnlineMapsMarker3DManager onlineMapsMarker3DManager = null;

        internal void Init(OnlineMapsMarker3DManager onlineMapsMarker3DManager)
        {
            this.mapObjects.Clear();
            this.mapObjects.Add(MapObjectType.Character, new Dictionary<string, AMapObject>());
            this.mapObjects.Add(MapObjectType.Station, new Dictionary<string, AMapObject>());
            EventIn_RemoveAllMapObjectsStation.AddListenerSingle(RemoveAllMapObjectsStation);
            EventIn_AddMapObject.AddListenerSingle(AddMapObject);
            EventIn_SetMapObjectPosition.AddListenerSingle(SetMapObjectPosition);
            EventIn_SetMapObjectStationState.AddListenerSingle(SetMapObjectStationState);
            this.onlineMapsMarker3DManager = onlineMapsMarker3DManager;
            this.offscreenIndicatorsHandler.Init();
        }

        private void RemoveAllMapObjectsStation()
        {
            this.mapObjects[MapObjectType.Station].Values.ForEach(i =>
            {
                i.DestroyInstance();
                this.onlineMapsMarker3DManager.Remove(i.VarOut_OnlineMapsMarker3D);
            });
            this.mapObjects[MapObjectType.Station].Clear();
            offscreenIndicatorsHandler.EventIn_DeleteAllOffscreenIndicators.Invoke();
            Debug.Log("All map objects station were removed."); 
        }

        private void AddMapObject(GPSPosition gpsPosition, MapObjectType mapObjectType, object objectData)
        {
            bool wasActive = this.gameObject.activeSelf;
            try
            {
                this.gameObject.SetActive(true);
                GameObject prefab = this.prefabsMapObjects[mapObjectType];
                OnlineMapsMarker3D omm3d = this.onlineMapsMarker3DManager.Create(gpsPosition.Longitude, gpsPosition.Latitude, prefab);
                AMapObject mapObject = null;
                string key = string.Empty;
                if (mapObjectType == MapObjectType.Station)
                {
                    Debug.Log("Map obj added: " + mapObjectType);
                    mapObject = omm3d.transform.GetComponent<MapObjectStation>();
                    mapObject.Init(omm3d);
                    (mapObject as MapObjectStation).EventIn_SetStationData.Invoke(objectData as Station);
                    key = (objectData as Station).Key;
                }
                else if(mapObjectType == MapObjectType.Character)
                {
                    mapObject = omm3d.transform.GetComponent<MapObjectCharacter>();
                    mapObject.Init(omm3d);
                    (mapObject as MapObjectCharacter).SetCharacterData((objectData as Player).Name.ToString());
                    key = (objectData as Player).Key;
                }
                this.mapObjects[mapObjectType].Add(key, mapObject);
                this.offscreenIndicatorsHandler.EventIn_CreateOffscreenIndicator.Invoke(mapObject);
            }
            catch(Exception ex)
            {
                Debug.LogError("ERROR: " + ex.Message);
                Debug.LogError("ERROR: " + ex.StackTrace);
            }
            if(!wasActive) { this.gameObject.SetActive(false); }
        }

        

        private void SetMapObjectPosition(GPSPosition gpsPosition, MapObjectType objectType, string key)
        {
            //// Check if the map object exists
            AMapObject mapObject = null;
            if(this.mapObjects.ContainsKey(objectType) && this.mapObjects[objectType].ContainsKey(key))
            {
                mapObject = this.mapObjects[objectType][key];
            }

            if (mapObject == null)
            {
                Debug.LogWarning($"Map object {objectType} with key {key} not found!");
                return;
            }

            // Get the OnlineMapsMarker3D component from the map object
            OnlineMapsMarker3D marker = mapObject.VarOut_OnlineMapsMarker3D;
            if (marker == null)
            {
                Debug.LogWarning($"OnlineMapsMarker3D not found in MapObject with key {key}.");
                return;
            }

            // Set the new position
            marker.SetPosition(gpsPosition.Longitude, gpsPosition.Latitude);
        }

        private void SetMapObjectStationState(string key, StationState state)
        {
            if(this.mapObjects[MapObjectType.Station].ContainsKey(key)) {
                (this.mapObjects[MapObjectType.Station][key] as MapObjectStation).EventIn_SetStationState.Invoke(state);
            }
        }
    }
} 
