using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using WordSearch;

namespace WordSearchUI
{
    public class WordSearchMain : MonoBehaviour
    {
        private readonly static string TXT_ENDSCREEN = "Du hast alle &1 gesuchten Wörter gefunden. Gut gemacht!<br>Auf zur nächsten Station!";
        private readonly static string TXT_SEARCHED_WORDS = "<b>Gesuchte Wörter:</b><br>&1";
        private readonly static string TXT_STRIKED_WORD = "<color=green><s></color>&1</s>";

        private WordMatrixGenerator wordMatrixGenerator = new WordMatrixGenerator();

        //[SerializeField] private WordSearchUI wordSearch = null;
        //[SerializeField] private WordsMatrix wordsMatrix = null;
        [SerializeField] private WordsMatrixUI wordsMatrixUI = null;
        [SerializeField] private TextMeshProUGUI tmpSearchedWords = null;
        //private List<string> searchedWords = new List<string>();
        //private List<string> foundWords = new List<string>();
        //private bool createNextWordGame = false;

        internal void Awake()
        {
            if (SceneManager.sceneCount <= 1)
            {
                //List<string> words = new List<string> { "HELLO", "WORLD", "UNITY", "PUZZLE", "SEARCH", "MATRIX", "GAMES", "ENGINE", "SCRIPT", "SHADER" };

                List<string> words = new List<string> { "CODE", "GAME", "BYTE", "LOOP", "MESH", "SHAD", "UNIT", "PLAY", "GRID", "VERT" };
                CreateWordSearch(words, 5, 5, 3);
            }
        }

        public void CreateWordSearch(List<string> words, int matrixWidth, int matrixHeight, int wordsCountInMatrix)
        {
            char[,] matrix = this.wordMatrixGenerator.GenerateWordSearchMatrix(words, matrixWidth, matrixHeight, wordsCountInMatrix);
            List<string> wordsInMatrix = this.wordMatrixGenerator.GetGeneratedWordsList();
            Debug.Log("GENERATED MATRIX: " + matrix.GetLength(0) + " " + matrix.GetLength(1) + wordsInMatrix.Count);
            string searched = string.Join(", ", wordsInMatrix);
            this.tmpSearchedWords.text = TXT_SEARCHED_WORDS.Replace("&1", searched);
            this.wordsMatrixUI.CreateWordsMatrix(matrix);
            //wordSearch.CreateWordSearch(matrix);
        }
        

        //internal void SceneChanged()
        //{
        //    //EventIn_CheckEnteredWord.RemoveListener(CheckEnteredWord);
        //    //this.wordsMatrix.SceneChanged();
        //}

        //private void CreateNewWordsGame()
        //{
        //    searchedWords.Clear();
        //    foundWords.Clear();
        //    wordSearch.CreateNextGame();
        //    //this.wordsMatrix.SetLetters(wordSearch.matrix);
        //    //this.searchedWords = this.wordSearch.insertedWords.Keys.ToList();
        //    this.searchedWords.RemoveAll(str => string.IsNullOrEmpty(str));
        //    string searched = string.Join(", ", this.searchedWords);
        //    this.tmpSearchedWords.text = TXT_SEARCHED_WORDS.Replace("&1", searched);
        //    this.createNextWordGame = false;
        //}

        //private void OnEnable()
        //{
        //    if (this.createNextWordGame)
        //    {
        //        CreateNewWordsGame();
        //    }
        //}

        //private void CheckEnteredWord(string word, List<KeyValuePair<int, int>> touchedObjects)
        //{
        //    if (this.searchedWords.Contains(word) && !string.IsNullOrEmpty(word))
        //    {
        //        this.searchedWords.Remove(word);
        //        this.foundWords.Add(word);
        //        this.tmpSearchedWords.text = this.tmpSearchedWords.text.Replace(word, TXT_STRIKED_WORD.Replace("&1", word));
        //        //this.wordsMatrix.IndicateRightWrongWord(touchedObjects, true);
        //        //if (this.searchedWords.Count == 0)
        //        //{
        //        //    PanelOverlay.EventIn_DisplayResult.Invoke(TXT_ENDSCREEN.Replace("&1", this.foundWords.Count.ToString()));
        //        //    this.createNextWordGame = true;
        //        //}
        //    }
        //    else
        //    {
        //        //this.wordsMatrix.IndicateRightWrongWord(touchedObjects, false);
        //    }
        //}
    }
}
