using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RightOrWrong
{
    public class RightOrWrongMain : MonoBehaviour
    {
        private static readonly string TXT_COUNTER = "&1/&2";
        private static readonly string TXT_RESULT = "<b>Ergebnis:</b><br><br>Du hast &1 von &2 Richtig!";

        [SerializeField] private TextMeshProUGUI tmpQuestion = null;
        [SerializeField] private TextMeshProUGUI tmpCounter = null;
        [SerializeField] private Button btnRight = null;
        [SerializeField] private Button btnWrong = null;
        [SerializeField] private Image imgRight = null;
        [SerializeField] private Image imgWrong = null;
        [SerializeField] private Image imgQuestion = null;
        [SerializeField] private GameObject goPanelResult = null;
        [SerializeField] private TextMeshProUGUI tmpResult = null;
        [SerializeField] private Button btnResultContinue = null;

        private Coroutine coroutine = null;
        private List<Fact> facts = new List<Fact>();
        private int questionIndex = 0;
        private Action<string> actionFinished = null;

        private void Awake()
        {
            this.btnWrong.onClick.AddListener(OnBtnFalse);
            this.btnRight.onClick.AddListener(OnBtnRight);
            this.btnResultContinue.onClick.AddListener(OnBtnResultContinue);

            if (SceneManager.sceneCount <= 1)
            {
                this.facts.Clear();
                this.facts.Add(new Fact("TEST Der Grazer Uhrturm wurde 2003 erbaut.", false));
                this.facts.Add(new Fact("TEST Früher diente der Uhrturm der Feuerwache, denn die Feuerwehrmänner konnten direkt vom Uhrturm aus Brände löschen.", false));
                this.facts.Add(new Fact("TEST Die Franzosen hätten den Uhrturm fast vollständig geschliffen.", true));
                this.facts.Add(new Fact("TEST Eine Festung zu schleifen bedeutet sie neu zu erbauen.", false));
                this.facts.Add(new Fact("TEST Der Zwilling des Grazer Uhrturms wurde verkauft und steht nun in Frankreich.", false));
                StartRightOrWrong(this.facts, null);
            }
        }

        public void StartRightOrWrong(List<Fact> facts, Action<string> actionFinished)
        {
            this.actionFinished = actionFinished;
            this.goPanelResult.SetActive(false);
            this.facts = facts;
            this.questionIndex = 0;
            SetQuestion(this.questionIndex);
        }

        private void SetQuestion(int questionIndex)
        {
            this.questionIndex = questionIndex;
            this.btnWrong.interactable = true;
            this.btnRight.interactable = true;
            this.tmpQuestion.text = facts[questionIndex].Text;
            this.tmpCounter.text = TXT_COUNTER.Replace("&1", (questionIndex + 1).ToString()).Replace("&2", facts.Count.ToString());
            this.imgQuestion.enabled = true;
            this.imgRight.enabled = false;
            this.imgWrong.enabled = false;
        }

        private void OnBtnFalse()
        {
            this.imgRight.enabled = !this.facts[this.questionIndex].IsRight;
            this.imgWrong.enabled = this.facts[this.questionIndex].IsRight;
            this.QuestionAnswered();
        }

        private void OnBtnRight()
        {
            this.imgRight.enabled = this.facts[this.questionIndex].IsRight;
            this.imgWrong.enabled = !this.facts[this.questionIndex].IsRight;
            this.QuestionAnswered();
        }

        private void QuestionAnswered()
        {
            this.facts[this.questionIndex].IsAnsweredRight = this.imgRight.enabled;
            this.imgQuestion.enabled = false;
            this.btnWrong.interactable = false;
            this.btnRight.interactable = false;
            this.coroutine = StartCoroutine(StartNextQuestion());
        }

        private IEnumerator StartNextQuestion()
        {
            yield return new WaitForSecondsRealtime(2f);
            this.questionIndex++;
            if (this.questionIndex >= this.facts.Count)
            {
                this.DisplayResult();
            }
            else
            {
                SetQuestion(this.questionIndex);
            }
            this.coroutine = null;
        }

        private void OnDisable()
        {
            if (this.coroutine != null)
            {
                StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }

        private void DisplayResult()
        {
            int totalFacts = this.facts.Count;
            int rightFacts = 0;
            this.facts.ForEach(f => { if (f.IsAnsweredRight) { rightFacts++; } });
            this.goPanelResult.SetActive(true);
            this.tmpResult.text = TXT_RESULT.Replace("&1", rightFacts.ToString()).Replace("&2", totalFacts.ToString());
        }

        private void OnBtnResultContinue()
        {
            this.actionFinished?.Invoke(JsonConvert.SerializeObject(facts));
        }
    }
}