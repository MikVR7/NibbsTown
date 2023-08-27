using Firebase.Storage;
using System;
using UnityEngine;

namespace NibbsTown
{
    internal class DatabaseStorage
    {
        internal static EventIn_UploadImage EventIn_UploadImage = new EventIn_UploadImage();
        internal static EventInOut_LoadImage EventInOut_LoadImage = new EventInOut_LoadImage();

        private FirebaseStorage firebaseStorage = null;
        
        internal void Init(FirebaseStorage firebaseStorage) {
            this.firebaseStorage = firebaseStorage;
            EventIn_UploadImage.AddListenerSingle(UploadImage);
            EventInOut_LoadImage.AddListenerSingle(LoadImage);
        }

        private void UploadImage(string path, Texture2D texture)
        {

        }

        private string tempPath = "Rallies/SchlossbergRally/";
        private async void LoadImage(string path, Action<Texture2D> onLoadImage)
        {
            path = tempPath + path;
            if (LocalStorageHandler.VarOut_FileExistsOnLocalDrive(path))
            {
                LocalStorageHandler.EventInOut_LoadFileFromLocalDrive(path, onLoadImage);
            }
            else
            {
                try
                {
                    // Create a reference to the file you want to download
                    StorageReference fileReference = firebaseStorage.GetReference(path);

                    // Download the file to a new byte array
                    byte[] fileContents = await fileReference.GetBytesAsync(long.MaxValue);

                    Debug.Log("Finished downloading file.");

                    // Convert the downloaded bytes to a Texture2D
                    Texture2D tex = new Texture2D(2, 2);
                    tex.LoadImage(fileContents);
                    LocalStorageHandler.EventInOut_SaveTexture2DToLocalDrive(path, tex);
                    onLoadImage.Invoke(tex);
                    // Use the tex Texture2D as you need
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    // Handle any errors
                }
            }
        }
    }
}
