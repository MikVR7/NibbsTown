//using System;
//using UnityEngine;

//namespace NibbsTown
//{
//    internal class TownRallyHandler : MonoBehaviour
//    {
//        internal static EventOut_OnUpdate EventOut_OnUpdate = new EventOut_OnUpdate();

//        [SerializeField] private PanelsHandler panelsHandlerBody = null;
//        [SerializeField] private TaskBarHandler taskBarHandler = null;
//        [SerializeField] private MapsHandler mapsHandler = null;
//        //[SerializeField] private CamerasHandler camerasHandler = null;
//        [SerializeField] private OverlaysHandler overlaysHandler = null;
//        [SerializeField] private DatabaseHandler databaseHandler = null;// new DatabaseHandler();
//        private RalliesHandler ralliesHandler = new RalliesHandler();
//        private PlayerPrefsHandler playerPrefsHandler = new PlayerPrefsHandler();

//        private void Awake()
//        {
//            //#if UNITY_ANDROID
//            //            Screen.fullScreen = false;
//            //#endif
//            this.overlaysHandler.Init();
//            this.databaseHandler.Init();
//            this.playerPrefsHandler.Init();
//            //this.camerasHandler.Init();
//            this.ralliesHandler.Init();
//            this.taskBarHandler.Init();
//            this.panelsHandlerBody.Init();
//            this.mapsHandler.Init(true);
//        }

//        private void Update()
//        {
//            EventOut_OnUpdate.Invoke();
//            if(Input.GetKeyDown(KeyCode.Escape))
//            {
//                PanelsHandler.EventIn_OnBtnPanelBack.Invoke();
//            }
//        }
//    }
//}
