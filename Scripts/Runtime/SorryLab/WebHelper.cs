using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UnityEngine;
namespace SorryLab {
    public class WebRequest<TRequest, TResponse> {
        private List<(string, string)> headers = new List<(string, string)> { ("Content-Type", "application/json") };
        private string url;
        public string errorMessage;
        public bool isError { get { return !string.IsNullOrEmpty(errorMessage); } }
        public WebRequest(string url) { this.url = url; }
        public void AppendHeader(string key, string value) { headers.Add((key, value)); }
        public Coroutine SendRequest(TRequest content, Action<TResponse> callback = null) {
            return StaticCoroutine.StartCoroutine(SendRequestAsync(content, callback));
        }
        IEnumerator SendRequestAsync(TRequest content, Action<TResponse> callback) {
            string contentJsonString = JsonUtility.ToJson(content);
            using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST")) {
                for (int i = 0; i < headers.Count; i++) {
                    webRequest.SetRequestHeader(headers[i].Item1, headers[i].Item2);
                }
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(contentJsonString);
                webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.disposeDownloadHandlerOnDispose = true;
                webRequest.disposeUploadHandlerOnDispose = true;
                yield return webRequest.SendWebRequest();
                if (webRequest.result == UnityWebRequest.Result.Success) {
                    TResponse response = JsonUtility.FromJson<TResponse>(webRequest.downloadHandler.text);
                    errorMessage = "";
                    callback?.Invoke(response);
                } else {
                    errorMessage = webRequest.error;
#if UNITY_EDITOR
                    Debug.LogWarning(errorMessage);
#endif
                    callback?.Invoke(default);
                }
                webRequest.Dispose();
            }
        }
    }
}
