using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
    internal class WordsMatrixUI : MonoBehaviour
    {
        [SerializeField] private GameObject prefabItem = null;
        [SerializeField] private RectTransform rtMatrixHolder = null;
        [SerializeField] private GridLayoutGroup gridLayoutGroup = null;
        
        private int itemsWidth = 0;
        private int itemsHeight = 0;
        private List<List<WordLetter>> letters = new List<List<WordLetter>>();
        private List<(int, int)> touchedLetters = new List<(int, int)>();
        private (int, int) direction = (0, 0);


        private void DestroyOldLetters()
        {
            this.letters.ForEach(i => i.ForEach(j => j.Destroy()));
            letters.Clear();
        }

        internal void CreateWordsMatrix(char[,] matrix)
        {
            this.DestroyOldLetters();
            this.itemsWidth = matrix.GetLength(0);
            this.itemsHeight = matrix.GetLength(1);
            this.gridLayoutGroup.constraintCount = itemsWidth;
            for (int x = 0; x < this.itemsWidth; x++)
            {
                for (int y = 0; y < this.itemsHeight; y++)
                {
                    GameObject item = Instantiate(this.prefabItem, this.rtMatrixHolder);
                    item.name = "letter_" + x + " " + y;
                    RectTransform tItem = item.GetComponent<RectTransform>();
                    item.GetComponent<WordLetter>().Init((x, y), matrix[x, y].ToString(), ActionOnClick, ActionOnDrag, ActionOnRelease);
                }
            }
        }

        private void ActionOnClick((int, int) xyIndices)
        {
            if (this.touchedLetters.Count == 0)
            {
                this.touchedLetters.Add(xyIndices);
                this.letters[xyIndices.Item1][xyIndices.Item2].SetLetterState(WordLetter.LetterState.Touched);
            }
            else if (this.touchedLetters.Count == 1)
            {
                DetermineDirection(this.touchedLetters[0], xyIndices);
                ActionOnDrag(xyIndices);
            }
        }

        private void DetermineDirection((int, int) start, (int, int) end)
        {
            direction = (end.Item1 - start.Item1, end.Item2 - start.Item2);
        }

        private void ActionOnDrag((int, int) xyIndices)
        {
            (int, int) lastTouched = this.touchedLetters[this.touchedLetters.Count - 1];
            while (lastTouched != xyIndices)
            {
                lastTouched = (lastTouched.Item1 + direction.Item1, lastTouched.Item2 + direction.Item2);
                this.touchedLetters.Add(lastTouched);
                this.letters[lastTouched.Item1][lastTouched.Item2].SetLetterState(WordLetter.LetterState.Touched);
            }
        }

        private void ActionOnRelease((int, int) xyIndices)
        {
            this.touchedLetters.Clear();
            direction = (0, 0);
        }
    }
}
