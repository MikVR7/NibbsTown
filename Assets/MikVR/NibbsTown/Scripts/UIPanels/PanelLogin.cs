using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NibbsTown
{
    internal class PanelLogin : APanel
    {
        internal static EventOut_UsernameChanged EventOut_UsernameChanged = new EventOut_UsernameChanged();
        internal static string VarOut_Username { get; private set; } = string.Empty;

        [SerializeField] private Button btnContinue = null;
        [SerializeField] private TMP_InputField inputName = null;
        
        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            this.btnContinue.onClick.AddListener(OnBtnContinue);
            this.btnContinue.interactable = false;
            this.inputName.onValueChanged.AddListener(OnValidateInputName);
            inputName.text = "MikTest";
        }

        private void OnValidateInputName(string value)
        {
            this.btnContinue.interactable = this.inputName.text.Length > 2;
        }

        private void OnBtnContinue()
        {
            VarOut_Username = this.inputName.text;
            EventOut_UsernameChanged.Invoke(VarOut_Username);
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallySelection);
        }
    }
}
