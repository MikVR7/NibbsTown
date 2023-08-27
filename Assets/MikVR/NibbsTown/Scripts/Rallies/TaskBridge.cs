using ClozeText;
using PicturePuzzle;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NibbsTown
{
    public class TaskBridge : MonoBehaviour
    {
        public static UnityEvent<RallyTask.Type, string> EventIn_StartTask = new UnityEvent<RallyTask.Type, string>();
        public static UnityEvent<string> EventOut_TaskFinished = new UnityEvent<string>();
        public static Camera VarOut_Camera = null;

        [SerializeField] private GameObject goTask = null;
        [SerializeField] private GameObject prefabEventSystem = null;
        [SerializeField] private AudioListener myAudioListener = null;
        [SerializeField] private Camera myCamera = null;
        private RallyTask.Type taskType = RallyTask.Type.None;

        private void Awake()
        {
            this.CheckEventSystem();
            this.CheckAudioListener();
            VarOut_Camera = this.myCamera;
            EventIn_StartTask.AddListener(StartTask);
        }

        private void CheckEventSystem()
        {
            EventSystem eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                GameObject goEventSystem = GameObject.Instantiate(prefabEventSystem, this.GetComponent<Transform>());
                goEventSystem.name = "event_system";
            }
        }

        private void CheckAudioListener()
        {
            this.myAudioListener.enabled = true;
            AudioListener[] listeners = FindObjectsOfType<AudioListener>();
            this.myAudioListener.enabled = listeners.Length <= 1;
        }

        private void StartTask(RallyTask.Type taskType, string taskData)
        {
            this.taskType = taskType;
            switch(taskType)
            {
                case RallyTask.Type.Task_Cloze:
                    this.goTask.GetComponent<ClozeTextHandler>().InitCloze();
                    ClozeTextHandler.EventOut_ClozeFinished.AddListener(TaskFinished);
                    ClozeTextHandler.EventIn_DisplayCloze.Invoke(taskData);
                    break;
                case RallyTask.Type.Task_PicturePuzzle:
                    this.goTask.GetComponent<PicturePuzzleMain>().InitPuzzle();
                    PicturePuzzleMain.EventOut_PuzzleFinished.AddListener(TaskFinished);
                    PicturePuzzleMain.EventIn_StartPuzzle.Invoke(taskData);
                    break;
            }   
        }

        private void TaskFinished(string taskResult)
        {
            EventOut_TaskFinished.Invoke(taskResult);
        }

        private void OnDisable()
        {
            switch (this.taskType)
            {
                case RallyTask.Type.Task_Cloze:
                    ClozeTextHandler.EventOut_ClozeFinished.RemoveListener(TaskFinished);
                    break;
            }
        }
    }
}
