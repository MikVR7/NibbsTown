using System.Collections.Generic;
using UnityEngine;

namespace NibbsTown
{
    internal class OffscreenIndicatorsHandler : MonoBehaviour
    {
        internal EventIn_CreateOffscreenIndicator EventIn_CreateOffscreenIndicator = new EventIn_CreateOffscreenIndicator();
        //internal EventIn_DeleteOffscreenIndicator EventIn_DeleteOffscreenIndicator = new EventIn_DeleteOffscreenIndicator();
        internal EventIn_DeleteAllOffscreenIndicators EventIn_DeleteAllOffscreenIndicators = new EventIn_DeleteAllOffscreenIndicators();

        [SerializeField] private GameObject prefabOffscreenIndicator = null;
        private List<OffscreenIndicator> offscreenIndicators = new List<OffscreenIndicator>();
        private Transform myTransform = null;

        internal void Init() {
            EventIn_CreateOffscreenIndicator.AddListenerSingle(CreateOffscreenIndicator);
            //EventIn_DeleteOffscreenIndicator.AddListenerSingle(DeleteOffscreenIndicator);
            EventIn_DeleteAllOffscreenIndicators.AddListenerSingle(DeleteAllOffscreenIndicators);
            this.myTransform = this.GetComponent<Transform>();
        }

        private void CreateOffscreenIndicator(AMapObject mapObject)
        {
            GameObject goOffscreenIndicator = Instantiate(prefabOffscreenIndicator, this.myTransform);
            OffscreenIndicator offscreenIndicator = goOffscreenIndicator.GetComponent<OffscreenIndicator>();
            offscreenIndicator.Init(mapObject);
            this.offscreenIndicators.Add(offscreenIndicator);
        }

        private void DeleteAllOffscreenIndicators()
        {
            this.offscreenIndicators.ForEach(i => i.DestroyObject());
            this.offscreenIndicators.Clear();
        }

        //private void DeleteOffscreenIndicator(string mapObjectId)
        //{
        //    for(int i=this.offscreenIndicators.Count-1; i>=0; i--) {
        //        if (this.offscreenIndicators[i].VarOut)
        //    }
        //    this.offscreenIndicators[mapObjectId].DestroyObject();
        //    this.offscreenIndicators.Remove(mapObjectId);
        //}
    }
}
