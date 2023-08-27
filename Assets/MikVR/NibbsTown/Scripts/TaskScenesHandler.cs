using ClozeText;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace NibbsTown
{
    public class TaskScenesHandler
    {
        internal EventIn_StartTaskScene EventIn_StartTaskScene = new EventIn_StartTaskScene();
        internal EventOut_FinishedTaskScene EventOut_FinishedTaskScene = new EventOut_FinishedTaskScene();

        private Dictionary<RallyTask.Type, string> sceneNames = new Dictionary<RallyTask.Type, string>();
        
        internal void Init()
        {
            EventIn_StartTaskScene.AddListenerSingle(StartTaskScene);
            this.sceneNames.Clear();
            this.sceneNames.Add(RallyTask.Type.Task_Cloze, "task_cloze");
        }
        
        private void StartTaskScene(RallyTask task)
        {
            NibbsTownMainMenu.VarOut_MonoBehaviour.StartCoroutine(LoadTaskSceneAsync(task));
        }

        private IEnumerator LoadTaskSceneAsync(RallyTask task)
        {
            if (sceneNames.ContainsKey(task.TType))
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNames[task.TType], LoadSceneMode.Additive);
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }
                TaskBridge.EventOut_TaskFinished.AddListener(OnTaskFinished);
                TaskBridge.EventIn_StartTask.Invoke(task.TType, task.Descr[0].Data);
                this.SetCameraRenderType(TaskBridge.VarOut_Camera, CameraRenderType.Overlay);
                this.AddToCameraStack(CamerasHandler.VarOut_CamBase, TaskBridge.VarOut_Camera);
            }
            else
            {
                Debug.LogWarning("Task scene not found: " + task.TType);
            }
        }

        private void SetCameraRenderType(Camera cam, CameraRenderType renderType)
        {
            UniversalAdditionalCameraData additionalCameraData = cam.gameObject.GetComponent<UniversalAdditionalCameraData>();
            cam.depth = 5;
            if (additionalCameraData != null)
            {
                additionalCameraData.renderType = renderType;
            }
        }

        private void AddToCameraStack(Camera baseCamera, Camera newOverlayCamera)
        {
            UniversalAdditionalCameraData baseCameraData = baseCamera.GetComponent<UniversalAdditionalCameraData>();
            if (baseCameraData != null)
            {
                var cameraStack = baseCameraData.cameraStack;

                // Find the position to insert the new camera (just as an example after camera3)
                int insertPosition = 2;

                // Insert the new camera into the stack at the desired position
                cameraStack.Insert(insertPosition, newOverlayCamera);

                // Note: The cameraStack property directly references the internal list, so we don't need to re-assign it.
            }
        }

        private string result = string.Empty;
        private void OnTaskFinished(string result)
        {
            this.result = result;
            this.AddToCameraStack(CamerasHandler.VarOut_CamBase, TaskBridge.VarOut_Camera);
            OverlaysHandler.EventIn_DisplayOverlay.Invoke(OverlaysHandler.OverlayType.Confirmation, "You finished cloze game with result of: NO DATA YET!", OnOverlayContinue, null);   
        }

        private void OnOverlayContinue()
        {
            Debug.Log("On task finished " + result);
            ClozeTextHandler.EventOut_ClozeFinished.RemoveListener(OnTaskFinished);
            NibbsTownMainMenu.VarOut_MonoBehaviour.StartCoroutine(UnloadTaskSceneAsync());
        }

        private IEnumerator UnloadTaskSceneAsync() {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("task_cloze");
            while (!asyncUnload.isDone)
            {
                yield return null;
            }
            EventOut_FinishedTaskScene.Invoke();
        }
    }
}
