using UnityEngine;
using UnityEngine.UI;

namespace NibbsTown
{
    internal class UIElementInfoPicture : AUIElement
    {
        [SerializeField] private RawImage image = null;
        private Texture texture = null;
        private Material material = null;

        internal override void Init(int elementIndex, Description.DescriptionType descriptionType, string elementData)
        {
            base.Init(elementIndex, descriptionType, elementData);
            this.SetTexture(this.descriptionData);
        }

        private void SetTexture(string path)
        {
            this.material = new Material(Tools.GetShader("UI/Unlit/Detail"));
            this.image.material = this.material;
            DatabaseStorage.EventInOut_LoadImage.Invoke(path, OnLoadedImage);
        }

        private void OnLoadedImage(Texture2D texture)
        {
            this.texture = texture;
            float aspectRatio = (float)texture.width / (float)texture.height;
            this.myRectTransform.sizeDelta = new Vector2(
                this.myRectTransform.sizeDelta.x,
                this.myRectTransform.rect.width / aspectRatio);
            this.image.texture = this.texture;
            this.SetElementHeight(this.myRectTransform.sizeDelta.y);
        }
    }
}
