using TMPro;
using UnityEngine;

namespace NibbsTown
{
    // MAP STYLES: https://www.lockedownseo.com/adding-custom-styles-to-google-maps/
    // MAP STYLES: https://snazzymaps.com/
    // https://snazzymaps.com/style/287720/modest

    internal class MapObjectCharacter : AMapObject
    {
        internal enum CharacterType
        {
            Self = 0,
            Peer = 1,
        }

        [SerializeField] private TextMeshPro tmpHeader = null;
        internal string VarOut_Name = string.Empty;
        
        internal override void Init(OnlineMapsMarker3D marker)
        {
            this.gameObject.SetActive(true);
            this.objectType = MapObjectType.Character;
            base.Init(marker);
        }

        internal override void DestroyInstance()
        {
            base.DestroyInstance();
        }

        internal void SetCharacterData(string name)
        {
            VarOut_Name = name;
            this.tmpHeader.text = name;
        }
    }
}
