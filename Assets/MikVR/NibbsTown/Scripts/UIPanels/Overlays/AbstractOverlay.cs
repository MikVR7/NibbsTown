using System;
using TMPro;
using UnityEngine;

namespace NibbsTown
{
    internal abstract class AbstractOverlay : MonoBehaviour
    {
        internal EventIn_DisplayOverlaySpecific EventIn_DisplayOverlaySpecific = new EventIn_DisplayOverlaySpecific();
        internal EventIn_HideOverlay EventIn_HideOverlay = new EventIn_HideOverlay();

        [SerializeField] private TextMeshProUGUI tmpInfo = null;
        protected Action actionDefaultYes = null;
        protected Action actionCancel = null;

        internal virtual void Init()
        {
            EventIn_DisplayOverlaySpecific.AddListenerSingle(DisplayOverlay);
            EventIn_HideOverlay.AddListenerSingle(this.Close);
            this.Close();
        }

        internal virtual void DestroyObject()
        {
            EventIn_DisplayOverlaySpecific.RemoveListener(DisplayOverlay);
        }

        private void DisplayOverlay(string message, Action actionDefaultYes, Action actionCancel)
        {
            this.gameObject.SetActive(true);
            this.actionDefaultYes = actionDefaultYes;
            this.actionCancel = actionCancel;
            this.tmpInfo.text = message;
        }

        protected void OnBtnDefaultYes()
        {
            this.actionDefaultYes?.Invoke();
            this.Close();
        }

        protected void OnBtnCancel()
        {
            this.actionCancel?.Invoke();
            this.Close();
        }

        private void Close()
        {
            this.actionDefaultYes = null;
            this.actionCancel = null;
            this.gameObject.SetActive(false);
        }
    }
}
