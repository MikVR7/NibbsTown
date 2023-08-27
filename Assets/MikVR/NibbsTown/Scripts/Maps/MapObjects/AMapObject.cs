using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace NibbsTown
{
    internal abstract class AMapObject : SerializedMonoBehaviour
    {
        internal enum MapObjectType
        {
            None = -1,
            Station = 0,
            Character = 1,
        }

        [ShowInInspector] private GPSPosition gpsPosition;

        protected MapObjectType objectType = MapObjectType.None;
        internal MapObjectType VarOut_MapObjectType() { return objectType; }

        //[SerializeField] internal string VarOut_ID { get; private set; } = string.Empty;
        internal OnlineMapsMarker3D VarOut_OnlineMapsMarker3D { get; private set; } = null;
        internal Transform MyTransform { get; private set; } = null;

        internal virtual void Init(OnlineMapsMarker3D marker)
        {
            this.MyTransform = this.GetComponent<Transform>();
            this.VarOut_OnlineMapsMarker3D = marker;
            NibbsTownMainMenu.VarOut_MonoBehaviour.StartCoroutine(GetGPSPosition());
        }

        private IEnumerator GetGPSPosition()
        {
            yield return new WaitForEndOfFrame();
            double longitude = 0d;
            double latitude = 0d;
            this.VarOut_OnlineMapsMarker3D.GetPosition(out longitude, out latitude);
            this.gpsPosition = new GPSPosition(longitude, latitude);
        }

        internal virtual void DestroyInstance()
        {
        }
    }
}
