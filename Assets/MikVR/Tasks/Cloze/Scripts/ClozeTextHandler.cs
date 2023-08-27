using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ClozeText
{
    public class ClozeTextHandler : MonoBehaviour
    {
        public static UnityEvent<string> EventIn_DisplayCloze = new UnityEvent<string>();
        public static UnityEvent<string> EventOut_ClozeFinished = new UnityEvent<string>();
        
        internal static readonly string TXT_QUESTIONMARK = "<align=center><color=grey>?";
        private string TXT_EXAMPLE1 = "Fülle die Lücken sinnvoll und korrekt aus.\n\nDie Geschichte des Uhrturms liegt lange zurück, denn bereits <cl-dd>1265</cl-dd> finden sich erste Aufzeichnungen des Turms als Teil der Festungsanlage am Schloßberg, und seit 1560 steht er in seiner heutigen Form an diesem Platz verankert. Damals diente er noch als <cl-dd>Feuerwache</cl-dd>, denn die Feuerwächter konnten Brände und Feuer in der Stadt erkennen und die <cl-dd>Feuerglocke</cl-dd> läuten. Die <cl-dd>Anzahl</cl-dd> der Schläge der Feuerglocke stand für jeweils einen anderen Grazer Bezirk in dem es brannte. Als die <cl-dd>Franzosen</cl-dd> Graz belagerten, begannen sie damit die Grazer Festungsanlage zu <cl-in>schleifen</cl-in>. Dem Uhrturm blieb dieses Schicksal erspart, denn er wurde von Grazer Bürgern <cl-in>freigekauft</cl-in>.";
        private string TXT_EXAMPLE2 = "Second text! Die Geschichte des Uhrturms liegt lange zurück, denn bereits <cl-dd>1265</cl-dd> finden sich erste Aufzeichnungen des Turms als Teil der Festungsanlage am Schloßberg, und seit 1560 steht er in seiner heutigen Form an diesem Platz verankert. Damals diente er noch als <cl-dd>Feuerwache</cl-dd>, denn die Feuerwächter konnten Brände und Feuer in der Stadt erkennen und die <cl-dd>Feuerglocke</cl-dd> läuten. Die <cl-dd>Anzahl</cl-dd> der Schläge der Feuerglocke stand für jeweils einen anderen Grazer Bezirk in dem es brannte. Als die <cl-dd>Franzosen</cl-dd> Graz belagerten, begannen sie damit die Grazer Festungsanlage zu <cl-in>schleifen</cl-in>. Dem Uhrturm blieb dieses Schicksal erspart, denn er wurde von Grazer Bürgern <cl-in>freigekauft</cl-in>.";


        [SerializeField] [TextArea(3, 5)] private string clozeText = string.Empty;
        [SerializeField] private ScrollRect scrollRect = null;
        [SerializeField] private TextMeshProUGUI tmpText = null;
        [SerializeField] private GameObject prefabDropdown = null;
        [SerializeField] private GameObject prefabInputField = null;
        [SerializeField] private RectTransform rtClozesHolder; // The UI element where you want to place the dropdowns and input fields.
        [SerializeField] private float additionalContentSpace = 200f;
        [SerializeField] private Button btnContinue = null;
        private ClozeParser clozeParser = new ClozeParser();
        private List<AClozeElement> clozeElements = new List<AClozeElement>();

        private void Awake()
        {
            InitCloze();
        }

        public void InitCloze() {
            RemoveAllListeners();
            this.btnContinue.onClick.AddListener(OnBtnContinue);
            EventIn_DisplayCloze.AddListener(RebuildCloze);
        }

        private void OnDisable()
        {
            RemoveAllListeners();
        }

        private void RemoveAllListeners() {
            this.btnContinue.onClick.RemoveListener(OnBtnContinue);
            EventIn_DisplayCloze.RemoveListener(RebuildCloze);
        }

        private void ClearCloze()
        {
            for(int i = clozeElements.Count-1; i >= 0; i--)
            {
                clozeElements[i].RemoveCloze();
                Destroy(clozeElements[i].gameObject);
            }
            this.clozeElements.Clear();

        }

        private IEnumerator BuildCloze() {
            ClearCloze();
            
            TMP_TextInfo textInfo = tmpText.textInfo;
            scrollRect.verticalNormalizedPosition = 1f;
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < this.clozeParser.VarOut_GetClozes().Count; i++)
            {
                Cloze cloze = this.clozeParser.VarOut_GetClozes()[i];
                WordPool wordPool = this.clozeParser.VarOut_GetPools().FirstOrDefault(wp => wp.Index == cloze.PoolIndex);
                Vector3 bottomLeft = textInfo.characterInfo[cloze.StartIndexFinal].bottomLeft;
                Vector3 topRight = textInfo.characterInfo[(cloze.StartIndexFinal+wordPool.LongestWord.Length)-1].topRight;
                if (this.clozeParser.VarOut_GetClozes()[i].ClozeType == Cloze.Type.Dropdown)
                {
                    GameObject goDropdown = Instantiate(prefabDropdown, rtClozesHolder);
                    ClozeDropdown dropdown = goDropdown.GetComponent<ClozeDropdown>();
                    this.clozeElements.Add(dropdown);
                    dropdown.Init(bottomLeft, topRight, cloze, wordPool);
                }
                else if(this.clozeParser.VarOut_GetClozes()[i].ClozeType == Cloze.Type.InputField)
                {
                    GameObject goInputField = Instantiate(prefabInputField, rtClozesHolder);
                    ClozeInputfield inputField = goInputField.GetComponent<ClozeInputfield>();
                    this.clozeElements.Add(inputField);
                    inputField.Init(bottomLeft, topRight, cloze, wordPool);
                }
            }
            
            // set content box size
            Vector3 bottomPosition = GetBottomOfLastLine(tmpText);
            this.scrollRect.content.sizeDelta = new Vector2(this.scrollRect.content.sizeDelta.x, Mathf.Abs(bottomPosition.y) + additionalContentSpace);
        }

        internal void RebuildCloze(string text)
        {
            this.clozeText = text;
            this.clozeParser.ParseClozes(clozeText);
            this.tmpText.text = this.clozeParser.VarOut_FinalText;
            this.btnContinue.onClick.AddListener(OnBtnContinue);
            StartCoroutine(BuildCloze());
        }

        private Vector3 GetBottomOfLastLine(TextMeshProUGUI textMesh)
        {
            if (textMesh == null)
                return Vector3.zero;

            // Force update of text mesh
            textMesh.ForceMeshUpdate();

            TMP_TextInfo textInfo = textMesh.textInfo;

            if (textInfo.lineCount == 0)
                return Vector3.zero;

            TMP_LineInfo lastLine = textInfo.lineInfo[textInfo.lineCount - 1];

            float bottomY = lastLine.baseline - lastLine.lineHeight;

            // Assuming you want the bottom center point. Adjust as needed for other positions.
            return new Vector3(0, bottomY, 0);
        }

        private void OnBtnContinue()
        {
            EventOut_ClozeFinished.Invoke("");
        }
    }
}
