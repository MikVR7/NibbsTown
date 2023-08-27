using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NibbsTown
{
    internal class OverlaysHandler : SerializedMonoBehaviour
    {
        internal enum OverlayType
        {
            None = 0,
            Confirmation = 1,
            Waiting = 2,
        }

        internal static EventIn_DisplayOverlay EventIn_DisplayOverlay = new EventIn_DisplayOverlay();
        internal static EventIn_HideOverlay EventIn_HideOverlay = new EventIn_HideOverlay();

        [SerializeField] private Dictionary<OverlayType, AbstractOverlay> overlayConfirmation = new Dictionary<OverlayType, AbstractOverlay>();

        internal void Init()
        {
            this.overlayConfirmation.ForEach(i => i.Value.Init());
            EventIn_DisplayOverlay.AddListenerSingle(DisplayOverlay);
            EventIn_HideOverlay.AddListenerSingle(HideOverlay);
        }

        private void DisplayOverlay(OverlayType overlay, string text, Action defaultOk, Action cancel)
        {
            overlayConfirmation[overlay].EventIn_DisplayOverlaySpecific.Invoke(text, defaultOk, cancel);
        }

        private void HideOverlay()
        {
            overlayConfirmation.Values.ForEach(i => i.EventIn_HideOverlay.Invoke());
        }
    }
}
