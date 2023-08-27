using TMPro;
using UnityEngine;

namespace ClozeText
{
    internal class ClozeInputfield : AClozeElement
    {
        [SerializeField] private TMP_InputField inputField = null;

        internal override void Init(Vector2 bottomLeft, Vector2 topRight, Cloze cloze, WordPool wordPool)
        {
            base.Init(bottomLeft, topRight, cloze, wordPool);
            this.inputField.onEndEdit.AddListener(OnWritingFinished);
            this.inputField.onSelect.AddListener(OnValueChanged);
            (inputField.placeholder as TMP_Text).text = ClozeTextHandler.TXT_QUESTIONMARK;
            //this.inputField.onValueChanged.AddListener(OnValueChanged);
            this.Reset();
        }

        internal override void RemoveCloze()
        {
            base.RemoveCloze();
            this.inputField.onEndEdit.RemoveListener(OnWritingFinished);
            this.inputField.onSelect.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(string value)
        {
            this.inputField.text = this.inputField.text.Replace(TXT_RED_STROKE, "");
            this.tmpCorrection.text = string.Empty;
            this.VarOut_RightWordSelected = false;
        }

        protected override void OnWritingFinished(string value)
        {
            base.OnWritingFinished(value);
            //this.inputField.interactable = false;

            if (!VarOut_RightWordSelected)
            {
                this.inputField.text = TXT_RED_STROKE + this.inputField.text;
            }
        }

        internal override void Reset()
        {
            base.Reset();
            this.inputField.text = string.Empty;
            //this.inputField.interactable = true;
        }
    }
}
