using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PicturePuzzle
{
    public class PicturePuzzleMain : MonoBehaviour
    {
        public static UnityEvent<string> EventIn_StartPuzzle = new UnityEvent<string>();
        public static UnityEvent<string> EventOut_PuzzleFinished = new UnityEvent<string>();

        [SerializeField] private ImageSlicer imageSlicer = null;
        [SerializeField] private Button btnContinue = null;

        private void Awake()
        {
            InitPuzzle();
        }

        public void InitPuzzle()
        {
            RemoveAllListeners();
            this.btnContinue.onClick.AddListener(OnBtnContinue);
            EventIn_StartPuzzle.AddListener(StartPuzzle);
            this.imageSlicer.Init();
        }

        private void RemoveAllListeners()
        {
            this.btnContinue.onClick.RemoveListener(OnBtnContinue);
            EventIn_StartPuzzle.RemoveListener(StartPuzzle);
        }

        private void StartPuzzle(string data)
        {
            this.imageSlicer.Init();
        }

        private void OnBtnContinue()
        {
            EventOut_PuzzleFinished.Invoke("");
        }
    }
}