using Sirenix.OdinInspector;
using UnityEngine;

namespace NibbsTown
{
    internal abstract class APanel : SerializedMonoBehaviour
    {
        public PanelsHandler.PanelType VarOut_PanelType { get; protected set; } = PanelsHandler.PanelType.None;
        
        internal virtual void Init(PanelsHandler.PanelType panelType)
        {
            this.VarOut_PanelType = panelType;
        }
        
        internal virtual void SetActive(bool active)
        {
            this.gameObject.SetActive(active);
        }
    }
}
