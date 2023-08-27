using Firebase.Firestore;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NibbsTown
{
    [System.Serializable]
    [FirestoreData]
    public struct GPSPosition
    {
        [FirestoreProperty()][ShowInInspector][JsonProperty("lo")] public double Longitude { get; set; }
        [FirestoreProperty()][ShowInInspector][JsonProperty("la")] public double Latitude { get; set; }
        public static GPSPosition Zero => new GPSPosition(0f, 0f);
        public GPSPosition(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }

    internal class MapsHandler : MonoBehaviour
    {
        private static MapsHandler instance = null;

        internal static EventIn_SetMapPosition EventIn_SetMapPosition = new EventIn_SetMapPosition();
        internal static EventIn_SetMapCurrentGPSPosition EventIn_SetMapCurrentGPSPosition = new EventIn_SetMapCurrentGPSPosition();
        internal static EventIn_DisplayMap EventIn_DisplayMap = new EventIn_DisplayMap();
        internal static EventIn_SetMapZoom EventIn_SetMapZoom = new EventIn_SetMapZoom();
        internal static EventOut_MapOnChangeZoom EventOut_MapOnChangeZoom = new EventOut_MapOnChangeZoom();
        internal static EventOut_OnMapClick EventOut_OnMapClick = new EventOut_OnMapClick();
        internal static EventOut_OnMapChangedPosition EventOut_OnMapChangedPosition = new EventOut_OnMapChangedPosition();

        private const int R = 6371; // Radius of the earth in km
        private static double D_LAT = 0d;
        private static double D_LON = 0d;
        private static double A = 0d;
        private static double C = 0d;

        internal static double VarOut_GetDistanceBetweenPoints(GPSPosition point1, GPSPosition point2)
        {
            D_LAT = Deg2Rad(point2.Latitude - point1.Latitude);
            D_LON = Deg2Rad(point2.Longitude - point1.Longitude);
            A = Math.Sin(D_LAT / 2) * Math.Sin(D_LAT / 2) +
                Math.Cos(Deg2Rad(point1.Latitude)) * Math.Cos(Deg2Rad(point2.Latitude)) *
                Math.Sin(D_LON / 2) * Math.Sin(D_LON / 2);
            C = 2 * Math.Atan2(Math.Sqrt(A), Math.Sqrt(1 - A));
            return (R * C) * 1000; // Distance in meters
        }

        private static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        [SerializeField] private OnlineMaps onlineMaps = null;
        [SerializeField] private OnlineMapsTileSetControl onlineMapsTileset = null;
        [SerializeField] private OnlineMapsControlBase onlineMapsControlBase = null;
        [SerializeField] private OnlineMapsMarker3DManager onlineMapsMarker3DManager = null;
        [SerializeField] private UniversalAdditionalCameraData camData = null;
        [SerializeField] private MapObjectsHandler mapObjectsHandler = null;
        [SerializeField] private Light mapLight = null;
        [SerializeField] private GPSHandler gpsHandler = null;

        internal void Init(bool isSceneMain)
        {
            instance = this;

            EventIn_SetMapPosition.AddListenerSingle(SetMapPosition);
            EventIn_SetMapCurrentGPSPosition.AddListenerSingle(SetMapCurrentGPSPosition);
            EventIn_DisplayMap.AddListenerSingle(DisplayMap);
            EventIn_SetMapZoom.AddListenerSingle(SetMapZoom);

            this.mapObjectsHandler.Init(this.onlineMapsMarker3DManager);
            this.camData.renderType = isSceneMain ? CameraRenderType.Overlay : CameraRenderType.Base;
            this.DisplayMap(!isSceneMain);
            this.mapLight.enabled = isSceneMain;

            onlineMapsTileset.OnMapClick += OnMapClick;
            onlineMaps.OnChangeZoom += OnChangeZoom;
            EventOut_MapOnChangeZoom.Invoke(this.onlineMaps.floatZoom);
            onlineMaps.OnChangePosition += OnMapChangedPosition;

            this.gpsHandler.Init();
        }

        private void OnMapClick()
        {
            Vector2 position = onlineMapsControlBase.GetCoords();
            GPSPosition gpsPosition = new GPSPosition(position.x, position.y);
            EventOut_OnMapClick.Invoke(gpsPosition);
        }

        private void DisplayMap(bool display)
        {
            //this.gameObject.SetActive(display);
        }

        private void OnChangeZoom()
        {
            EventOut_MapOnChangeZoom.Invoke(this.onlineMaps.floatZoom);
        }

        private void SetMapZoom(float zoom)
        {
            Debug.Log("Set map zoom: " + zoom);
            onlineMaps.floatZoom = zoom;
        }

        private void SetMapPosition(GPSPosition gpsPosition)
        {
            this.onlineMaps.SetPosition(gpsPosition.Longitude, gpsPosition.Latitude);
        }

        internal static GPSPosition VarOut_GetMapPosition()
        {
            double latitude = 0;
            double longitude = 0;
            instance.onlineMaps.GetPosition(out longitude, out latitude);
            return new GPSPosition(longitude, latitude);
        }

        private void SetMapCurrentGPSPosition()
        {
            this.onlineMaps.SetPosition(GPSHandler.VarOut_LastPosition.Longitude, GPSHandler.VarOut_LastPosition.Latitude);
        }

        private void OnMapChangedPosition()
        {
            EventOut_OnMapChangedPosition.Invoke();
        }
    }
}
