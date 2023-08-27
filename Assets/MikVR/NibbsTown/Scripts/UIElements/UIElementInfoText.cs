using System.Collections;
using TMPro;
using UnityEngine;

namespace NibbsTown
{
    internal class UIElementInfoText : AUIElement
    {
        [SerializeField] private TextMeshProUGUI tmpText = null;
        private TMP_TextInfo textInfo = null;

        internal override void Init(int elementIndex, Description.DescriptionType descriptionType, string elementData)
        {
            base.Init(elementIndex, descriptionType, elementData);
            tmpText.text = elementData;
            tmpText.ForceMeshUpdate();
            StartCoroutine(this.GetElementHeight());
        }

        private IEnumerator GetElementHeight()
        {
            while(this.textInfo == null)
            {
                yield return new WaitForEndOfFrame();
                this.textInfo = tmpText.textInfo;
            }
            TMP_LineInfo lineInfo = textInfo.lineInfo[textInfo.lineCount-1];
            float elementHeight = 0f;
            for (int i = lineInfo.firstCharacterIndex; i < lineInfo.firstCharacterIndex + lineInfo.characterCount; i++)
            {
                elementHeight = Mathf.Max(elementHeight, Mathf.Abs(textInfo.characterInfo[i].descender));
            }
            this.SetElementHeight(elementHeight);
        }
    }
}
