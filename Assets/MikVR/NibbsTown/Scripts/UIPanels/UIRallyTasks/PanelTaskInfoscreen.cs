using UnityEngine;
using UnityEngine.UI;

namespace NibbsTown
{
    internal class PanelTaskInfoscreen : ARallyTask
    {
        [SerializeField] private Button btnContinue = null;
        internal override void Init(RallyTask.Type rallyTask)
        {
            base.Init(rallyTask);
            btnContinue.onClick.AddListener(OnBtnContinue);
        }

        internal override void SetActive(bool active)
        {
            base.SetActive(active);
        }

        private void OnBtnContinue()
        {
            //RalliesHandler.EventIn_CurrentTaskFinished.Invoke();
        }
    }
}
