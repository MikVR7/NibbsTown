using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace NibbsTown
{
    internal class LocalStorageHandler
    {
        internal void Init() { }

        internal static bool VarOut_FileExistsOnLocalDrive(string path)
        {
            string fullPath = string.Concat(Application.persistentDataPath, path);
            return File.Exists(fullPath);
        }

        internal static void EventInOut_LoadFileFromLocalDrive(string path, Action<Texture2D> onLoadImage)
        {
            string fullPath = string.Concat(Application.persistentDataPath, path);
            if (File.Exists(fullPath))
            {
                // Start the coroutine to load the texture
                NibbsTownMainMenu.VarOut_MonoBehaviour.StartCoroutine(LoadTextureFromPath(fullPath, onLoadImage));
                Debug.Log("Loaded texture from local: " + fullPath);
            }
            else
            {
                Debug.Log("Error: file does not exist: " + fullPath);
            }
        }

        private static IEnumerator LoadTextureFromPath(string path, Action<Texture2D> onLoadImage)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file://" + path))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    // Get the downloaded texture
                    onLoadImage.Invoke(DownloadHandlerTexture.GetContent(uwr));
                }
            }
        }

        internal static void EventInOut_SaveTexture2DToLocalDrive(string path, Texture2D texture)
        {
            byte[] jpgData = texture.EncodeToJPG();
            if (jpgData != null)
            {
                string fullPath = string.Concat(Application.persistentDataPath, path);
                string directoryName = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                File.WriteAllBytes(fullPath, jpgData);
                Debug.Log("Saved texture to: " + fullPath);
            }
        }
    }
}
