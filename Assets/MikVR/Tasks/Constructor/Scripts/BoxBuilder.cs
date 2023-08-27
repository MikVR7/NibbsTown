using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Constructor
{
    internal class BoxBuilder : SerializedMonoBehaviour
    {
        private enum ElementType
        {
            None = 0,
            Wall = 1,
            Door = 2,
        }


        [SerializeField] private Dictionary<ElementType, GameObject> prefabElements = new Dictionary<ElementType, GameObject>();
        [SerializeField] private Transform elementsHolder = null;
        [SerializeField] private Camera cameraMain = null;
        private System.Random _rand = new System.Random();

        internal void RebuildPuzzle()
        {
            this.DestroyAllBoxElements();
            BuildBoxElements(3, 3, 1, 1, 1);
        }

        internal void DestroyAllBoxElements()
        {
            foreach (Transform child in elementsHolder)
            {
                Destroy(child.gameObject);
            }
        }

        internal void BuildBoxElements(int boxesX, int boxesY, int sideDoors, int centerDoors, int centerWalls)
        {
            int totalPossibleElements = CountTotalPossibleElements(boxesX, boxesY);
            int sideElementsCount = ((boxesX - 1) * 2) + ((boxesY - 1) * 2);
            Debug.Log("Side elements count: " + sideElementsCount);

            float startPosX = -(boxesX / 2f) + 0.5f;
            float startPosY = (boxesY / 2f) - 0.5f;
            int sideIndex = 0;
            int centerIndex = 0;

            List<ElementType> sideElementTypes = GetSideElementTypesRandom(sideElementsCount, sideDoors);
            List<ElementType> centerElementTypes = GetCenterElementTypesRandom(totalPossibleElements - sideElementsCount, centerDoors, centerWalls);

            for (int i = 0; i < boxesX; i++)
            {
                for (int j = 0; j < boxesY; j++)
                {
                    bool isLeftWall = i == 0;
                    bool isRightWall = i == (boxesX - 1);
                    bool isTopWall = j == 0;
                    bool isBottomWall = j == (boxesY - 1);
                    bool isWallPart = isLeftWall || isRightWall || isTopWall || isBottomWall;
                    Vector3 position = new Vector3(startPosX + i, startPosY - j, 0f);

                    if (isWallPart && (sideElementTypes.Count > sideIndex))
                    {
                        float rotationZ = (isTopWall && !isRightWall) || (isBottomWall && !isRightWall) ? 0f : 270f;
                        CreateObject(sideElementTypes[sideIndex], position, rotationZ, sideIndex);
                        sideIndex++;
                        if(isTopWall && isLeftWall && (sideElementTypes.Count > sideIndex))
                        {
                            rotationZ = 270f;// (isTopWall && !isRightWall) || (isBottomWall && !isRightWall) ? 0f : 270f;
                            CreateObject(sideElementTypes[sideIndex], position, rotationZ, sideIndex);
                            sideIndex++;
                        }
                    }
                    // create the center elements
                    //else if(centerElementTypes.Count > centerIndex)
                    //{
                    //    CreateObject(centerElementTypes[centerIndex++], position, 0f, centerIndex);
                    //    CreateObject(centerElementTypes[centerIndex++], position, 270f, centerIndex);
                    //}
                }
            }
        }

        private int CountTotalPossibleElements(int pointsX, int pointsY)
        {
            int count = 0;
            for (int i = 0; i < pointsX; i++)
            {
                for (int j = 0; j < pointsY; j++)
                {
                    bool isRightWall = i == (pointsX - 1);
                    bool isBottomWall = j == (pointsY - 1);
                    if(!isRightWall) { count++; }
                    if(!isBottomWall) { count++; }
                }
            }
            return count;
        }

        private void CreateObject(ElementType elementType, Vector3 localPosition, float rotationZ, int index)
        {
            if(elementType == ElementType.None) { return; }
            GameObject go = GameObject.Instantiate(this.prefabElements[elementType], elementsHolder);
            go.name = elementType.ToString().ToLower() + "_" + index;
            Transform t = go.GetComponent<Transform>();
            t.localPosition = localPosition;
            t.localEulerAngles = new Vector3(t.localEulerAngles.x, t.localEulerAngles.y, rotationZ);
            if(elementType == ElementType.Door)
            {
                DoorKnob door = go.GetComponent<DoorKnob>();
                door.Init(cameraMain);
            }
        }

        private List<ElementType> GetSideElementTypesRandom(int range, int amountDoors)
        {
            if (range < amountDoors)
            {
                throw new ArgumentException("The amount of random numbers requested is more than the range provided.");
            }
            List<int> numbers = Enumerable.Range(0, range).ToList();

            numbers = ShuffleList(numbers);
            List<int> indicesDoors = numbers.GetRange(0, amountDoors);
            numbers.RemoveRange(0, amountDoors);
            List<ElementType> wallTypes = new List<ElementType>();
            for(int i = 0; i < range; i++)
            {
                wallTypes.Add(indicesDoors.Contains(i) ? ElementType.Door : ElementType.Wall);
            }
            return wallTypes;
        }

        private List<ElementType> GetCenterElementTypesRandom(int centerElementsCount, int amountDoors, int amountWalls)
        {
            if (centerElementsCount < (amountDoors + amountWalls))
            {
                throw new ArgumentException("The amount of random numbers requested is more than the range provided.");
            }
            List<int> numbers = Enumerable.Range(0, centerElementsCount).ToList();

            numbers = ShuffleList(numbers);

            // cut off doors
            List<int> indicesDoors = numbers.GetRange(0, amountDoors);
            numbers.RemoveRange(0, amountDoors);

            // cut off walls
            List<int> indicesWalls = numbers.GetRange(0, amountWalls);
            numbers.RemoveRange(0, amountWalls);

            List<ElementType> wallTypes = new List<ElementType>();
            for (int i = 0; i < centerElementsCount; i++)
            {
                if (indicesDoors.Contains(i))
                {
                    wallTypes.Add(ElementType.Door);
                }
                else if (indicesWalls.Contains(i))
                {
                    wallTypes.Add(ElementType.Wall);
                }
                else
                {
                    wallTypes.Add(ElementType.None);
                }
            }
            return wallTypes;
        }

        private List<int> ShuffleList(List<int> numbers)
        {
            // Shuffle the list
            int n = numbers.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = _rand.Next(i + 1); // Random non-negative integer less than i+1
                // Swap numbers[i] and numbers[j]
                int temp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = temp;
            }
            return numbers;
        }
    }
}
