using ClozeText;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NibbsTown
{
    internal class RallyTasksHandler
    {
        internal static EventIn_LoadTasks EventIn_LoadTasks = new EventIn_LoadTasks();
        internal static EventIn_StartRallyTasks EventIn_StartRallyTasks = new EventIn_StartRallyTasks();
        internal static EventIn_FinishedRallyTask EventIn_FinishedRallyTask = new EventIn_FinishedRallyTask();
        internal static EventOut_TasksLoadingDone EventOut_TasksLoadingDone = new EventOut_TasksLoadingDone();
        internal static EventOut_FinishedRallyTasks EventOut_FinishedRallyTasks = new EventOut_FinishedRallyTasks();

        internal static Station VarOut_CurrentStation { get; private set; }

        private TaskScenesHandler taskScenesHandler = new TaskScenesHandler();
        private List<RallyTask> rallyTasks = new List<RallyTask>();
        private int taskIndex = 0;
        //private RallyTask currentTask = null;

        internal void Init() {
            EventIn_LoadTasks.AddListenerSingle(LoadTasks);
            EventIn_StartRallyTasks.AddListenerSingle(StartRallyTasks);
            EventIn_FinishedRallyTask.AddListenerSingle(FinishedRallyTask);
            this.taskScenesHandler.Init();
            this.taskScenesHandler.EventOut_FinishedTaskScene.AddListenerSingle(FinishedRallyTask);
        }

        private void LoadTasks(Station station)
        {
            VarOut_CurrentStation = station;
            DatabaseRallies.EventInOut_LoadDBTasks.Invoke(RalliesHandler.VarOut_CurrentRallyKey, station.Key, OnLoadingTasksDone);
        }

        private void OnLoadingTasksDone(Dictionary<string, RallyTask> response)
        {
            Debug.Log("On loading tasks done: " + response.Count);
            foreach (string key in response.Keys)
            {
                response[key].Key = key;
            }
            rallyTasks = response.Values.ToList();
            EventOut_TasksLoadingDone.Invoke();
        }

        private void StartRallyTasks()
        {
            if (this.rallyTasks.Count > 0)
            {
                PerformRallyTask(0);
            }
        }

        private void PerformRallyTask(int taskIndex)
        {
            this.taskIndex = taskIndex;
            RallyTask task = rallyTasks.FirstOrDefault(t => t.Id == taskIndex);
            if (task == null)
            {
                RallyTasksHandler.EventIn_FinishedRallyTask.Invoke();
                Debug.LogWarning("Task was null (task index:" + taskIndex + ")");
                return;
            }
            Debug.Log("Perform rally task type: " + task.TType + " - task index: " + taskIndex);
            if (task.TType == RallyTask.Type.InfoScreen)
            {
                PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallyInfo);
                PanelRallyInfo.EventIn_DisplayRallyInfo.Invoke(task.Descr);
            }
            else
            {
                PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.None);
                this.taskScenesHandler.EventIn_StartTaskScene.Invoke(task);
            }
        }

        private void FinishedRallyTask()
        {
            taskIndex++;
            if (taskIndex >= this.rallyTasks.Count)
            {
                this.rallyTasks.Clear();
                //this.currentTask = null;
                Debug.Log("RALLY TASK FINISHED!");
                EventOut_FinishedRallyTasks.Invoke();
            }
            else
            {
                PerformRallyTask(taskIndex);
            }
        }
    }
}
