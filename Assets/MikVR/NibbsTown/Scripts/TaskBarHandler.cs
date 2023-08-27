using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NibbsTown
{
    internal class TaskBarHandler : MonoBehaviour
    {
        internal static EventOut_OnBtnDebug EventOut_OnBtnDebug = new EventOut_OnBtnDebug();

        [SerializeField] private Button btnBack = null;
        [SerializeField] private Button btnDebug = null;
        [SerializeField] private TextMeshProUGUI tmpUsername = null;
        [SerializeField] private TextMeshProUGUI tmpRallyname = null;
        //[SerializeField] private Image imgDebugActive = null;

        internal void Init()
        {
            this.btnBack.onClick.AddListener(OnBtnBack);
            this.btnDebug.onClick.AddListener(OnBtnDebug);
            this.tmpUsername.text = string.Empty;
            this.tmpRallyname.text = string.Empty;
            PanelLogin.EventOut_UsernameChanged.AddListenerSingle(OnUsernameChanged);
            StationsHandler.EventOut_StationsLoadingDone.AddListenerSingle(OnSelectedRallyChanged);
            PanelsHandler.EventOut_OnPanelChanged.AddListenerSingle(OnCurrentPanelChanged);
        }

        private void OnUsernameChanged(string username)
        {
            this.tmpUsername.text = username;
        }

        private void OnSelectedRallyChanged()
        {
            this.tmpRallyname.text = RalliesHandler.VarOut_CurrentRally().Name;
        }

        private void OnCurrentPanelChanged(PanelsHandler.PanelType panelType)
        {
            this.btnBack.gameObject.SetActive(
                !panelType.Equals(PanelsHandler.PanelType.Loading) &&
                !panelType.Equals(PanelsHandler.PanelType.Login) &&
                //!panelType.Equals(PanelsHandler.PanelType.Rally) &&
                !panelType.Equals(PanelsHandler.PanelType.None));
        }

        private void OnBtnBack()
        {
            PanelsHandler.EventIn_OnBtnPanelBack.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                OnBtnDebug();
            }
        }

        private void OnBtnDebug()
        {
            Debug.Log("ON DEBUG!");
            StartCoroutine(OnBtnDebugDelayed());
        }

        private IEnumerator OnBtnDebugDelayed() {
            Debug.Log("PART 1");
            DatabaseRallies.EventIn_DeleteAllRalliesFromDB.Invoke();
            yield return new WaitForSecondsRealtime(2);
            Debug.Log("PART 2");
            RallyCreator.EventIn_AddNewRallyToServer.Invoke(RallyCreator.NewRallyType.RallyGrazSchlossberg);
            yield return new WaitForSecondsRealtime(2);
            Debug.Log("PART 3");
            RallyCreator.EventIn_AddNewRallyToServer.Invoke(RallyCreator.NewRallyType.RallyGrazMur);
            yield return new WaitForSecondsRealtime(2);
            Debug.Log("PART 4");
            RallyCreator.EventIn_AddNewRallyToServer.Invoke(RallyCreator.NewRallyType.RallyOedWald);




            //Settings.IsDebugModeOn = !Settings.IsDebugModeOn;
            //this.imgDebugActive.gameObject.SetActive(Settings.IsDebugModeOn);
            //EventOut_OnBtnDebug.Invoke();
        }
    }
}
