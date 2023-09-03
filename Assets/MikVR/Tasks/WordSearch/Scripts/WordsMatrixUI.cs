using UnityEngine;

namespace WordSearch
{
    internal class WordsMatrixUI : MonoBehaviour
    {
        [SerializeField] private GameObject prefabItem = null;
        private int itemsWidth = 0;
        private int itmesHeight = 0;

        internal void CreateWordsMatrix(char[,] matrix)
        {
            this.itemsWidth = matrix.GetLength(0);
            this.itmesHeight = matrix.GetLength(1);
            for (int x = 0; x < this.itemsWidth; x++)
            {
                for (int y = 0; y < this.itmesHeight; y++)
                {
                    GameObject item = Instantiate(this.prefabItem, this.transform);
                    item.transform.localPosition = new Vector3(x, y, 0);
                    item.GetComponent<WordLetter>().Init((x, y), matrix[x, y].ToString());
                }
            }
        }
    }
}
