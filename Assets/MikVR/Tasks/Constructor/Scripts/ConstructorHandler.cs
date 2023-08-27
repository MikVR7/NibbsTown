using UnityEngine;
using UnityEngine.UI;

namespace Constructor
{
    public class ConstructorHandler : MonoBehaviour
    {
        [SerializeField] private BoxRotator boxRotator = null;
        [SerializeField] private BoxBuilder boxBuilder = null;
        [SerializeField] private ObjectsHandler objectsHandler = null;
        [SerializeField] private Button btnRestart = null;

        private void Awake()
        {
            this.boxRotator.Init();
            this.boxBuilder.RebuildPuzzle();
            this.btnRestart.onClick.AddListener(OnBtnRestart);
            this.objectsHandler.Init();
        }

        private void OnBtnRestart()
        {
            this.boxBuilder.RebuildPuzzle();
            this.objectsHandler.ResetObjects();
        }
    }
}