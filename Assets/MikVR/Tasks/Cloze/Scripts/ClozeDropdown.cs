using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

namespace ClozeText
{
    internal class ClozeDropdown : AClozeElement
    {
        [SerializeField] private TextMeshProUGUI tmpLabel = null;
        [SerializeField] private TMP_Dropdown dropdown = null;
        
        internal override void Init(Vector2 bottomLeft, Vector2 topRight, Cloze cloze, WordPool wordPool)
        {
            base.Init(bottomLeft, topRight, cloze, wordPool);
            this.dropdown.onValueChanged.AddListener(OnValueChanged);
            this.Reset();
        }

        internal override void RemoveCloze()
        {
            base.RemoveCloze();
            this.dropdown.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(int valueIndex)
        {
            base.OnWritingFinished(this.tmpLabel.text);
            if (!VarOut_RightWordSelected)
            {
                this.tmpLabel.text = TXT_RED_STROKE + this.tmpLabel.text;
            }
        }

        internal override void Reset()
        {
            base.Reset();
            this.dropdown.options.Clear();
            List<string> strings = new List<string>();
            strings.AddRange(wordPool.Pool);
            Shuffle(strings);
            this.dropdown.options.Add(new TMP_Dropdown.OptionData(ClozeTextHandler.TXT_QUESTIONMARK));
            foreach (string option in strings)
            { 
                this.dropdown.options.Add(new TMP_Dropdown.OptionData(option));
            }
            this.dropdown.value = 0;
            this.dropdown.RefreshShownValue();
            this.tmpLabel.text = this.tmpLabel.text.Replace(TXT_RED_STROKE, "");
        }

        private System.Random rng = new System.Random();
        private void Shuffle(List<string> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
