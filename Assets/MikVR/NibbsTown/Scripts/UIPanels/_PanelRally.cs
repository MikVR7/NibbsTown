//using System.Collections.Generic;
//using UnityEngine;

//namespace NibbsTown
//{
//    internal class PanelRally : APanel
//    {
//        [SerializeField] private Dictionary<RallyTask.Type, ARallyTask> rallyTasks = new Dictionary<RallyTask.Type, ARallyTask>(); 

//        internal override void Init(PanelsHandler.PanelType panelType)
//        {
//            base.Init(panelType);
//            foreach(RallyTask.Type task in this.rallyTasks.Keys)
//            {
//                this.rallyTasks[task].Init(task);
//            }
//            //RalliesHandler.EventOut_RallyStationTaskChanged.AddListenerSingle(RallyStationTaskChanged);
//        }

//        internal override void SetActive(bool active)
//        {
//            base.SetActive(active);
//            if (active) {
//                RallyStationTaskChanged();
//            }
//        }

//        private void RallyStationTaskChanged()
//        {
//            RallyTask currentTask = RalliesHandler.VarOut_CurrentTask();
//            foreach (RallyTask.Type taskType in this.rallyTasks.Keys)
//            {
//                rallyTasks[taskType].SetActive(currentTask.TType.Equals(taskType));
//            }
//        }
//    }
//}
