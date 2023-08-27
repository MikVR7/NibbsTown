using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NibbsTown
{
    internal class PanelLoading : APanel
    {
        private static readonly string TXT_INFO_INTRO = "Starting NibbsTown";
        private static readonly string TXT_INFO_LOADING = "Loading:<br>&1";
        private static readonly string TXT_LOAD_ALL_RALLIES = "Rallies data";

        [SerializeField] private TextMeshProUGUI tmpInfo = null;

        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            this.gameObject.SetActive(true);
            StartCoroutine(StartLoading());
        }

        // intro screen
        private IEnumerator StartLoading()
        {
            // TODO: make that differently - why does this have to happen later?
            this.tmpInfo.text = TXT_INFO_INTRO;
            yield return new WaitForEndOfFrame();
            yield return new WaitForSecondsRealtime(1f);
            LoadAllRallies();
        }

        // loading rallies
        private void LoadAllRallies()
        {
            this.tmpInfo.text = TXT_INFO_LOADING.Replace("&1", TXT_LOAD_ALL_RALLIES);
            RalliesHandler.EventOut_RalliesLoadingDone.AddListenerSingle(LoadingAllRalliesDone);
            RalliesHandler.EventIn_LoadRalliesList.Invoke();
        }
        private void LoadingAllRalliesDone()
        {
            RalliesHandler.EventOut_RalliesLoadingDone.RemoveListener(LoadingAllRalliesDone);
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.Login);
        }
    }
}
