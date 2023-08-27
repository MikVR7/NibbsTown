using UnityEngine;
using UnityEngine.UI;

namespace NibbsTown
{
    internal class OverlayConfirmation : AbstractOverlay
    {
        [SerializeField] private Button btnYes = null;
        [SerializeField] private Button btnNo = null;
        [SerializeField] private Button btnClose = null;

        internal override void Init()
        {
            base.Init();
            this.btnYes.onClick.AddListener(OnBtnDefaultYes);
            this.btnNo.onClick.AddListener(OnBtnCancel);
            this.btnClose.onClick.AddListener(OnBtnCancel);
        }

        internal override void DestroyObject()
        {
            base.DestroyObject();
            this.btnYes.onClick.RemoveListener(OnBtnDefaultYes);
            this.btnNo.onClick.RemoveListener(OnBtnCancel);
            this.btnClose.onClick.RemoveListener(OnBtnCancel);
        }
    }
}
