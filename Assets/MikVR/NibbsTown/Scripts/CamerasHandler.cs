using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NibbsTown
{
    internal class CamerasHandler : MonoBehaviour
    {
        internal static Camera VarOut_CamBase = null;

        [SerializeField] private Camera camBase = null;
        ////[SerializeField] private Camera camUI = null;
        //[SerializeField] private Camera camMap = null;

        internal void Init()
        {
            VarOut_CamBase = camBase;
        //    //UniversalAdditionalCameraData cameraDataBase = camBase.GetUniversalAdditionalCameraData();
        //    ////cameraDataBase.cameraStack.Add(camMap);
        //    //cameraDataBase.cameraStack.Insert(0, camMap);
        }
    }
}
