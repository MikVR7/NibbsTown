using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

namespace NibbsTown
{
    internal class GPSHandler : SerializedMonoBehaviour
    {
        internal static EventOut_OnNewGPSCoordinates EventOut_OnNewGPSCoordinates = new EventOut_OnNewGPSCoordinates();

        internal static GPSPosition VarOut_LastPosition { get; private set; }
        internal static bool VarOut_HasPosition() { return VarOut_LastPosition.Equals(Vector2.zero); }

        //private Coroutine coroutine = null;
        //bool locationIsReady = false;
        //bool locationGrantedAndroid = false;

        [InlineButton("OnBtnDebugUp", SdfIconType.ArrowDown)]
        [InlineButton("OnBtnDebugDown", SdfIconType.ArrowUp)]
        [SerializeField] private double DebugLatitude = 47.05461414246786d;
        
        [InlineButton("OnBtnDebugLeft", SdfIconType.ArrowRight)]
        [InlineButton("OnBtnDebugRight", SdfIconType.ArrowLeft)]
        [SerializeField] private double DebugLongitude = 15.868048031120157d;

        [InlineButton("MoveToStation1", "Move to station 1")]
        [SerializeField] private double Station1Longitude = 15.868548031120158d;
        [SerializeField] private double Station1Latitude = 47.057114142467945d;
        [InlineButton("MoveToStation2", "Move to station 2")]
        [SerializeField] private double Station2Longitude = 15.866948031120161d;
        [SerializeField] private double Station2Latitude = 47.061314142468085d;
        [InlineButton("MoveToStation3", "Move to station 3")]
        [SerializeField] private double Station3Longitude = 15.868048031120159d;
        [SerializeField] private double Station3Latitude = 47.060514142468058d;
        [InlineButton("MoveToStation4", "Move to station 4")]
        [SerializeField] private double Station4Longitude = 15.87194803112015d;
        [SerializeField] private double Station4Latitude = 47.060914142468071d;
        [InlineButton("MoveToStation5", "Move to station 5")]
        [SerializeField] private double Station5Longitude = 15.87194803112015d;
        [SerializeField] private double Station5Latitude = 47.057114142467945d;
        [InlineButton("MoveToStation6", "Move to station 6")]
        [SerializeField] private double Station6Longitude = 15.867948031120159d;
        [SerializeField] private double Station6Latitude = 47.060614142468062d;
        [InlineButton("MoveToStation7", "Move to station 7")]
        [SerializeField] private double Station7Longitude = 15.867948031120159d;
        [SerializeField] private double Station7Latitude = 47.060614142468062d;


        //[InlineButton("OnBtnDebugSetPosition", "Set position")]

        internal void Init() {
            Debug.Log("INIT GPSHandler");
        }

        private void OnEnable()
        {
            //if(this.coroutine != null)
            //{
            //    StopCoroutine(this.coroutine);
            //    this.coroutine = null;
            //}
#if !UNITY_EDITOR
                InitializeGPSConnection();
#endif
            Debug.Log(" ON ENABLE GPS CONNECTION!");
            OnUpdateAt50();
            NibbsTownMainMenu.EventOut_OnUpdateAt50.AddListenerSingle(OnUpdateAt50);
        }

            private void OnDisable()
        {
            Debug.Log(" ON DISABLE GPS CONNECTION!");
            NibbsTownMainMenu.EventOut_OnUpdateAt50.RemoveListener(OnUpdateAt50);
            VarOut_LastPosition = new GPSPosition(0d, 0d);
        }

        private void InitializeGPSConnection()
        {
#if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }
            else
            {
                //locationGrantedAndroid = true;
                /*locationIsReady = */NativeGPSPlugin.StartLocation();
            }

#elif PLATFORM_IOS
        locationIsReady = NativeGPSPlugin.StartLocation();
#endif
        }

        private void OpenSettings()
        {
#if UNITY_ANDROID
            Debug.Log("OpenSettings!");
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    using (var settingsIntent = new AndroidJavaObject("android.content.Intent", "android.settings.LOCATION_SOURCE_SETTINGS"))
                    {
                        currentActivity.Call("startActivity", settingsIntent);
                    }
                }));
            }
            Debug.Log("OPEN SETTINGS DONE!");
#endif
        }

        private void OnUpdateAt50()
        {
            //Debug.Log("HELLO!!");
#if UNITY_EDITOR
            VarOut_LastPosition = new GPSPosition(DebugLongitude, DebugLatitude);
            //Debug.Log("UPDATE! EDITOR: " + VarOut_LastPosition.Longitude + " " + VarOut_LastPosition.Latitude);
#else
            VarOut_LastPosition = new GPSPosition(NativeGPSPlugin.GetLongitude(), NativeGPSPlugin.GetLatitude());
            //Debug.Log("UPDATE! ANDROID: " + VarOut_LastPosition.Longitude + " " + VarOut_LastPosition.Latitude + " " + NativeGPSPlugin.GetLongitude()+ " " + NativeGPSPlugin.GetLatitude());
#endif
            EventOut_OnNewGPSCoordinates.Invoke(VarOut_LastPosition);
        }


        private void OnBtnDebugLeft()
        {
            DebugLongitude += 0.0001d;
        }
        private void OnBtnDebugRight()
        {
            DebugLongitude -= 0.0001d;
        }
        private void OnBtnDebugUp()
        {
            DebugLatitude -= 0.0001d;
        }
        private void OnBtnDebugDown()
        {
            DebugLatitude += 0.0001d;
        }

        private void MoveToStation1()
        {
            DebugLatitude = Station1Latitude;
            DebugLongitude = Station1Longitude;
        }
        private void MoveToStation2()
        {
            DebugLatitude = Station2Latitude;
            DebugLongitude = Station2Longitude;
        }
        private void MoveToStation3()
        {
            DebugLatitude = Station3Latitude;
            DebugLongitude = Station3Longitude;
        }
        private void MoveToStation4()
        {
            DebugLatitude = Station4Latitude;
            DebugLongitude = Station4Longitude;
        }
        private void MoveToStation5()
        {
            DebugLatitude = Station5Latitude;
            DebugLongitude = Station5Longitude;
        }
        private void MoveToStation6()
        {
            DebugLatitude = Station6Latitude;
            DebugLongitude = Station6Longitude;
        }
        private void MoveToStation7()
        {
            DebugLatitude = Station7Latitude;
            DebugLongitude = Station7Longitude;
        }



        //private IEnumerator InitializeGPSConnectionUnity()
        //{
        //    // Check if user has location service enabled
        //    if (!Input.location.isEnabledByUser)
        //    {
        //        Debug.Log("User has not enabled GPS");
        //        OpenSettings();
        //        yield break;
        //    }

        //    // Start service before querying location
        //    Input.location.Start();

        //    // Wait until service initializes
        //    int maxWait = 20;
        //    while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        //    {
        //        yield return new WaitForSeconds(1);
        //        maxWait--;
        //    }

        //    // Service didn't initialize in 20 seconds
        //    if (maxWait < 1)
        //    {
        //        Debug.Log("Timed out");
        //        yield break;
        //    }

        //    // Connection has failed
        //    if (Input.location.status == LocationServiceStatus.Failed)
        //    {
        //        Debug.Log("Unable to determine device location");
        //        yield break;
        //    }
        //    else
        //    {
        //        // Access granted and location value could be retrieved
        //        OnUpdateAt50();
        //        NibbsTownMainMenu.EventOut_OnUpdateAt50.AddListenerSingle(OnUpdateAt50);
        //    }
        //    this.coroutine = null;
        //}
    }
}
