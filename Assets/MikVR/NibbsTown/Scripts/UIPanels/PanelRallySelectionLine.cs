using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NibbsTown
{
    internal class PanelRallySelectionLine : MonoBehaviour
    {
        private static string TXT_LINERALLY = "&1<size=70%><br>&2";

        [SerializeField] private Button btnSelf = null;
        [SerializeField] private TextMeshProUGUI tmpText = null;
        private string rallyKey = string.Empty;
        private Rally rally;

        internal void Init(string rallyKey, Rally rally)
        {
            this.rallyKey = rallyKey;
            this.rally = rally;
            this.tmpText.text = TXT_LINERALLY.Replace("&1", this.rally.Name).Replace("&2", this.rally.Caption);
            this.btnSelf.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            StationsHandler.EventOut_StationsLoadingDone.AddListenerSingle(OnStationsLoadingDone);
            RalliesHandler.EventIn_SetCurrentRally.Invoke(this.rallyKey);
            //MapObjectsHandler.EventIn_RemoveAllMapObjectsStation.Invoke();
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallyInfo);
        }

        private void OnStationsLoadingDone()
        {
            StationsHandler.EventOut_StationsLoadingDone.RemoveListener(OnStationsLoadingDone);
            Rally rally = RalliesHandler.VarOut_CurrentRally();
            if (rally.Name.Equals(RalliesHandler.NO_RALLY)) { return; }
            PanelRallyInfo.EventIn_DisplayRallyInfo.Invoke(rally.Descr);
        }
    }
}
