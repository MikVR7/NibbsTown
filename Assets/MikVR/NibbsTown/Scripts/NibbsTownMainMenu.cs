using System;
using UnityEngine;

namespace NibbsTown
{
    internal class NibbsTownMainMenu : MonoBehaviour
    {
        internal static EventOut_OnUpdate EventOut_OnUpdate = new EventOut_OnUpdate();
        internal static EventOut_OnUpdateAt2 EventOut_OnUpdateAt2 = new EventOut_OnUpdateAt2();
        internal static EventOut_OnUpdateAt50 EventOut_OnUpdateAt50 = new EventOut_OnUpdateAt50();
        internal static MonoBehaviour VarOut_MonoBehaviour = null;

        [SerializeField] private CamerasHandler camerasHandler = null;
        [SerializeField] private PanelsHandler panelsHandlerBody = null;
        [SerializeField] private TaskBarHandler taskBarHandler = null;
        [SerializeField] private MapsHandler mapsHandler = null;
        [SerializeField] private OverlaysHandler overlaysHandler = null;
        private PlayersHandler playersHandler = new PlayersHandler();
        private DatabaseHandler databaseHandler = new DatabaseHandler();
        private RalliesHandler ralliesHandler = new RalliesHandler();
        private PlayerPrefsHandler playerPrefsHandler = new PlayerPrefsHandler();
        private LocalStorageHandler localStorageHandler = new LocalStorageHandler();

        private void Awake()
        {
            //#if UNITY_ANDROID
            //            Screen.fullScreen = false;
            //#endif
            VarOut_MonoBehaviour = this;
            this.camerasHandler.Init();
            this.overlaysHandler.Init();
            this.databaseHandler.Init();
            this.playerPrefsHandler.Init();
            this.ralliesHandler.Init();
            this.taskBarHandler.Init();
            this.panelsHandlerBody.Init();
            this.mapsHandler.Init(true);
            this.playersHandler.Init();
            this.localStorageHandler.Init();
        }

        private int counter50 = 0;
        private int counter2 = 0;
        private void Update()
        {
            if (EventOut_OnUpdate.HasListeners())
            {
                EventOut_OnUpdate.Invoke();
            }

            counter50++;
            if (counter50 >= 50)
            {
                EventOut_OnUpdateAt50.Invoke();
                counter50 = 0;
            }

            counter2++; 
            if (counter2 >= 2)
            {
                EventOut_OnUpdateAt2.Invoke();
                counter2 = 0;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PanelsHandler.EventIn_OnBtnPanelBack.Invoke();
            }
        }
    } 
}