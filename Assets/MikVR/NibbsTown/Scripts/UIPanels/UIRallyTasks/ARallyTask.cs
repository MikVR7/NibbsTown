using UnityEngine;

namespace NibbsTown
{
    internal abstract class ARallyTask : MonoBehaviour
    {
        public RallyTask.Type VarOut_TaskType { get; protected set; } = RallyTask.Type.None;

        internal virtual void Init(RallyTask.Type rallyTask)
        {
            this.VarOut_TaskType = rallyTask;
            this.SetActive(false);
        }

        internal virtual void SetActive(bool active)
        {
            this.gameObject.SetActive(active);
        }
    }
}
