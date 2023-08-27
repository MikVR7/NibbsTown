using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ClozeText
{
    internal abstract class AClozeElement : MonoBehaviour
    {
        protected static readonly string TXT_RED_STROKE = "<color=red><s></color>";
        [SerializeField] private RectTransform myRectTransform = null;
        [SerializeField] private Image imgCheckmark = null;
        [SerializeField] protected TextMeshProUGUI tmpCorrection = null;
        private float textHeight = 44f;
        private Cloze cloze = null;
        protected WordPool wordPool = null;
        internal bool VarOut_RightWordSelected { get; set; } = false;

        internal virtual void Init(Vector2 bottomLeft, Vector2 topRight, Cloze cloze, WordPool wordPool)
        {
            this.cloze = cloze;
            this.wordPool = wordPool;
            myRectTransform.anchoredPosition = new Vector2(bottomLeft.x, bottomLeft.y);
            Vector2 size = new Vector2(topRight.x - bottomLeft.x, textHeight);
            myRectTransform.sizeDelta = size;
            Reset();
        }

        internal virtual void RemoveCloze()
        {

        }

        protected virtual void OnWritingFinished(string value)
        {
            VarOut_RightWordSelected = value.Equals(this.cloze.RightWord);
            if (VarOut_RightWordSelected)
            {
                this.imgCheckmark.enabled = true;
                this.tmpCorrection.text = string.Empty;
            }
            else
            {
                this.tmpCorrection.enabled = true;
                this.tmpCorrection.text = this.cloze.RightWord;
            }
        }

        internal virtual void Reset()
        {
            this.imgCheckmark.enabled = false;
            this.tmpCorrection.enabled = false;
            VarOut_RightWordSelected = false;
        }
    }
}
