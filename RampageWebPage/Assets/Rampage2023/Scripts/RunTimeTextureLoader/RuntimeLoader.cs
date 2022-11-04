using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace RunTimeTextureLoader
{
    public class RuntimeLoader : MonoBehaviour
    {
        internal IEnumerator LoadImageFromUrl(string url, Action<Texture2D> onTextureDownloaded, Action<string> onDownloadError)
        {
            using UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                onDownloadError?.Invoke(uwr.error);
                yield break;
            }

            //Set texture
            Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
            onTextureDownloaded?.Invoke(texture);
        }
    }
}