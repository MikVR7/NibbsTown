using System;
using System.Collections.Generic;

namespace WordSearchUI
{
    internal class WordMatrixGenerator
    {
        private int width = 0;
        private int height = 0;
        private char[,] matrix;
        private List<string> directions = new List<string> { "R", "L", "D", "U", "DR", "DL", "UR", "UL" };
        private List<string> words = new List<string>();
        
        internal char[,] GenerateWordSearchMatrix(List<string> words, int width, int height, int wordsInMatrix)
        {
            this.width = width;
            this.height = height;
            this.matrix = new char[height, width];
            this.words.Clear();

            // Initialize matrix with placeholders
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = '.';
                }
            }

            // Randomly select 5 words
            Shuffle(words);
            List<string> selectedWords = words.GetRange(0, Math.Min(wordsInMatrix, words.Count));

            foreach (string word in selectedWords)
            {
                bool placed = false;
                while (!placed)
                {
                    string direction = directions[UnityEngine.Random.Range(0, directions.Count)];
                    int row = UnityEngine.Random.Range(0, height);
                    int col = UnityEngine.Random.Range(0, width);

                    if (CanPlaceWord(word, row, col, direction))
                    {
                        PlaceWord(word, row, col, direction);
                        placed = true;
                    }
                }
            }

            // Fill empty spaces with random letters
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (matrix[i, j] == '.')
                    {
                        matrix[i, j] = (char)UnityEngine.Random.Range(65, 91); // ASCII values for A-Z
                    }
                }
            }

            return matrix;
        }

        internal List<string> GetGeneratedWordsList() {
            return this.words;
        }

        private bool CanPlaceWord(string word, int row, int col, string direction)
        {
            for (int i = 0; i < word.Length; i++)
            {
                switch (direction)
                {
                    case "R":
                        if (col + i >= width || (matrix[row, col + i] != '.' && matrix[row, col + i] != word[i]))
                            return false;
                        break;
                    case "L":
                        if (col - i < 0 || (matrix[row, col - i] != '.' && matrix[row, col - i] != word[i]))
                            return false;
                        break;
                    case "D":
                        if (row + i >= height || (matrix[row + i, col] != '.' && matrix[row + i, col] != word[i]))
                            return false;
                        break;
                    case "U":
                        if (row - i < 0 || (matrix[row - i, col] != '.' && matrix[row - i, col] != word[i]))
                            return false;
                        break;
                    case "DR":
                        if (row + i >= height || col + i >= width || (matrix[row + i, col + i] != '.' && matrix[row + i, col + i] != word[i]))
                            return false;
                        break;
                    case "DL":
                        if (row + i >= height || col - i < 0 || (matrix[row + i, col - i] != '.' && matrix[row + i, col - i] != word[i]))
                            return false;
                        break;
                    case "UR":
                        if (row - i < 0 || col + i >= width || (matrix[row - i, col + i] != '.' && matrix[row - i, col + i] != word[i]))
                            return false;
                        break;
                    case "UL":
                        if (row - i < 0 || col - i < 0 || (matrix[row - i, col - i] != '.' && matrix[row - i, col - i] != word[i]))
                            return false;
                        break;
                }
            }
            return true;
        }

        private void PlaceWord(string word, int row, int col, string direction)
        {
            this.words.Add(word);
            for (int i = 0; i < word.Length; i++)
            {
                switch (direction)
                {
                    case "R":
                        matrix[row, col + i] = word[i];
                        break;
                    case "L":
                        matrix[row, col - i] = word[i];
                        break;
                    case "D":
                        matrix[row + i, col] = word[i];
                        break;
                    case "U":
                        matrix[row - i, col] = word[i];
                        break;
                    case "DR":
                        matrix[row + i, col + i] = word[i];
                        break;
                    case "DL":
                        matrix[row + i, col - i] = word[i];
                        break;
                    case "UR":
                        matrix[row - i, col + i] = word[i];
                        break;
                    case "UL":
                        matrix[row - i, col - i] = word[i];
                        break;
                }
            }
        }

        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int r = UnityEngine.Random.Range(0, i);
                T temp = list[i];
                list[i] = list[r];
                list[r] = temp;
            }
        }
    }
}
