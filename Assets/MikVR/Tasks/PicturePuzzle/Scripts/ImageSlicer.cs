using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PicturePuzzle
{
    internal class ImageSlicer : SerializedMonoBehaviour
    {
        [SerializeField] private GameObject prefabPuzzlePart = null;
        [SerializeField] private RectTransform rtHolderImagePlayground = null;
        [SerializeField] private RectTransform rtHolderTemplate = null;
        [SerializeField] private Texture2D sourceImage = null;
        [SerializeField] internal int rows = 3;
        [SerializeField] internal int columns = 2;
        [SerializeField] private GameObject goEndScreen = null;

        private List<List<PuzzlePart>> templatePuzzleParts = new List<List<PuzzlePart>>();
        private List<List<PuzzlePart>> puzzleParts = new List<List<PuzzlePart>>();

        internal void Init()
        {
            this.CleanupPreviousPuzzle();
            StartCoroutine(InitDelayed());
        }

        private void CleanupPreviousPuzzle()
        {
            // TODO: Cleanup previous!
        }

        private IEnumerator InitDelayed() {
            yield return new WaitForEndOfFrame();
            this.goEndScreen.SetActive(false);
            this.CreatePuzzleParts();
            this.SliceAndAssign();
            this.RandomizePuzzleParts();
        }

        private void CreatePuzzleParts()
        {
            this.templatePuzzleParts.Clear();
            this.puzzleParts.Clear();

            for (int i = 0; i < rows; i++)
            {
                List<PuzzlePart> parts = new List<PuzzlePart>();
                List<PuzzlePart> templateParts = new List<PuzzlePart>();

                for (int j = 0; j < columns; j++)
                {
                    parts.Add(CreatePuzzlePart(i, j, rtHolderImagePlayground, false));
                    templateParts.Add(CreatePuzzlePart(i, j, rtHolderTemplate, true));
                }

                this.puzzleParts.Add(parts);
                this.templatePuzzleParts.Add(templateParts);
            }
        }

        private PuzzlePart CreatePuzzlePart(int i, int j, RectTransform holder, bool isTemplate)
        {
            GameObject goPuzzlePart = GameObject.Instantiate(this.prefabPuzzlePart, holder);
            goPuzzlePart.name = "img_part_" + i + "_" + j;
            PuzzlePart puzzlePart = goPuzzlePart.GetComponent<PuzzlePart>();
            puzzlePart.Init((i, j), isTemplate, IsPuzzleStartDrag, /*IsPuzzlePartDragged,*/ IsPuzzlePartReleased);
            return puzzlePart;
        }

        private void RandomizePuzzleParts()
        {
            float posMinX = float.MaxValue;
            float posMinY = float.MaxValue;
            float posMaxX = float.MinValue;
            float posMaxY = float.MinValue;
            for (int i = 0; i < this.puzzleParts.Count; i++)
            {
                for (int j = 0; j < this.puzzleParts[i].Count; j++)
                {
                    posMinX = Mathf.Min(posMinX, this.puzzleParts[i][j].myRectTransform.anchoredPosition.x);
                    posMinY = Mathf.Min(posMinY, this.puzzleParts[i][j].myRectTransform.anchoredPosition.y);
                    posMaxX = Mathf.Max(posMaxX, this.puzzleParts[i][j].myRectTransform.anchoredPosition.x);
                    posMaxY = Mathf.Max(posMaxY, this.puzzleParts[i][j].myRectTransform.anchoredPosition.y);
                }
            }
            for (int i = 0; i < this.puzzleParts.Count; i++)
            {
                for (int j = 0; j < this.puzzleParts[i].Count; j++)
                {
                    this.puzzleParts[i][j].myRectTransform.anchoredPosition =
                        new Vector2(
                            BiasedRandomWithExpandedRange(posMinX, posMaxX),
                            BiasedRandomWithExpandedRange(posMinY, posMaxY));
                }
            }
            this.ShuffleChildren();
        }

        //private float BiasedRandom(float minValue, float maxValue)
        //{
        //    float value = Random.value;
        //    if (value < 0.5f)
        //    {
        //        // Lower half: skew towards 0
        //        value = Mathf.Sqrt(value * 2);
        //    }
        //    else
        //    {
        //        // Upper half: skew towards 1
        //        value = 1 - Mathf.Sqrt((1 - value) * 2);
        //    }
        //    return Mathf.Lerp(minValue, maxValue, value);
        //}
        public float BiasedRandomWithExpandedRange(float minValue, float maxValue, float percentage = 0.1f)
        {
            float rangeExpansion = (maxValue - minValue) * percentage;

            minValue -= rangeExpansion / 2;
            maxValue += rangeExpansion / 2;

            float value = Random.value;
            if (value < 0.5f)
            {
                // Lower half: skew towards 0
                value = Mathf.Sqrt(value * 2);
            }
            else
            {
                // Upper half: skew towards 1
                value = 1 - Mathf.Sqrt((1 - value) * 2);
            }

            return Mathf.Lerp(minValue, maxValue, value);
        }

        private void ShuffleChildren()
        {
            int childCount = this.rtHolderImagePlayground.childCount;
            for (int i = 0; i < childCount; i++)
            {
                this.rtHolderImagePlayground.GetChild(i).SetSiblingIndex(Random.Range(0, childCount));
            }
        }

        private void SliceAndAssign(/*RectTransform rtHolder*/)
        {
            int sliceWidth = sourceImage.width / columns;
            int sliceHeight = sourceImage.height / rows; 
            float scaleFactor = CalculateScaleFactor(Mathf.Max(sourceImage.width, sourceImage.height));

            // Total width and height of all puzzle pieces together
            float totalWidth = sliceWidth * scaleFactor * columns;
            float totalHeight = sliceHeight * scaleFactor * rows;

            // Starting position to ensure the puzzle is centered
            float canvasWidth = this.rtHolderImagePlayground.rect.width;
            float canvasHeight = this.rtHolderImagePlayground.rect.height;
            float startX = (canvasWidth - totalWidth) / 2f; 
            float startY = (canvasHeight - totalHeight) / 2f; 

            for (int i = 0; i < rows; i++) 
            { 
                for (int j = 0; j < columns; j++)
                {
                    Texture2D slice = new Texture2D(sliceWidth, sliceHeight);
                    slice.SetPixels(sourceImage.GetPixels(j * sliceWidth, i * sliceHeight, sliceWidth, sliceHeight));
                    slice.Apply();

                    this.puzzleParts[i][j].SetTexturePart(slice);
                    this.puzzleParts[i][j].SetOverlayAlpha(0f);
                    this.templatePuzzleParts[i][j].SetTexturePart(slice);
                    this.templatePuzzleParts[i][j].SetOverlayAlpha(1f);
                    Vector2 size = new Vector2(sliceWidth * scaleFactor, sliceHeight * scaleFactor);
                    Vector2 position = new Vector2(startX + j * size.x, startY + i * size.y);
                    this.puzzleParts[i][j].SetSizePosition(size, position);
                    this.templatePuzzleParts[i][j].SetSizePosition(size, position);
                }
            }
        }

        private float CalculateScaleFactor(int dimension)
        {
            float canvasWidth = this.rtHolderImagePlayground.rect.width;
            float desiredWidth = canvasWidth * 0.6f; // 60% of canvas width
            return desiredWidth / sourceImage.width;
        }

        private void IsPuzzleStartDrag((int, int) ijIndex)
        {
            this.puzzleParts[ijIndex.Item1][ijIndex.Item2].myRectTransform.SetAsLastSibling();
            this.templatePuzzleParts.ForEach(pi => pi.ForEach(pj => pj.RemovePuzzlePart(ijIndex)));
        }

        private void IsPuzzlePartReleased((int, int) ijIndex)
        {
            PuzzlePart pPart = this.puzzleParts[ijIndex.Item1][ijIndex.Item2];
            Rect referenceRect = GetWorldRect(pPart.myRectTransform);
            List<PuzzlePart> overlappingTemplateParts = new List<PuzzlePart>();
            for (int m = 0; m < this.templatePuzzleParts.Count; m++)
            {
                for (int n = 0; n < this.templatePuzzleParts[m].Count; n++)
                {
                    Rect tempRect = GetWorldRect(this.templatePuzzleParts[m][n].myRectTransform);
                    if (referenceRect.Overlaps(tempRect))
                    {
                        overlappingTemplateParts.Add(this.templatePuzzleParts[m][n]);
                    }
                }
            }

            if (overlappingTemplateParts.Count > 0)
            {
                PuzzlePart nearestTemplate = overlappingTemplateParts.OrderBy(
                    template =>
                    Vector3.Distance(pPart.myRectTransform.position, template.myRectTransform.position)
                ).First();

                pPart.myRectTransform.position = nearestTemplate.myRectTransform.position;
                nearestTemplate.AddPuzzlePart(ijIndex);
                this.CheckIfAllPuzzlePartsAreCorrect();
            }
        }

        private void CheckIfAllPuzzlePartsAreCorrect()
        {
            
            for(int i = 0; i < this.templatePuzzleParts.Count; i++)
            {
                for (int j = 0; j < this.templatePuzzleParts[i].Count; j++)
                {
                    if (!this.templatePuzzleParts[i][j].VarOut_IsCorrectPuzzlePart)
                    {
                        return;
                    }
                }
            }
            DisplayEndScreen();
        }

        private void DisplayEndScreen()
        {
            this.goEndScreen.SetActive(true);
        }

        private Rect GetWorldRect(RectTransform rt)
        {
            Vector2 sizeDelta = rt.sizeDelta;
            float rectWidth = sizeDelta.x * rt.lossyScale.x;
            float rectHeight = sizeDelta.y * rt.lossyScale.y;

            Vector3 position = rt.position;
            return new Rect(position.x - rectWidth * 0.5f, position.y - rectHeight * 0.5f, rectWidth, rectHeight);
        }
    }
}
