using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace SorryLab {
    public class TextureCache : MonoBehaviour {
        static Dictionary<string, Texture2D> m_textureList = new Dictionary<string, Texture2D>();
        static List<string> m_loading = new List<string>();
        static TextureCache m_instance;
        void Awake() {
            m_instance = this;
            transform.parent = null;
            hideFlags = HideFlags.HideInHierarchy;
            DontDestroyOnLoad(gameObject);
        }
        public static Texture2D GetLoadedTexture(string url) {
            if (IsTextureLoaded(url)) {
                return m_textureList[url];
            } else {
                return null;
            }
        }
        static public bool IsTextureLoaded(string url) {
            return m_textureList.ContainsKey(url);
        }
        static public void LoadTexture(string url, Action<Texture2D> callback = null, Action<string> loadFail = null) {
            if (IsTextureLoaded(url)) {
                callback?.Invoke(m_textureList[url]);
            } else {
                m_instance.StartCoroutine(LoadTextureCoroutine(url, FilterMode.Bilinear, TextureWrapMode.Repeat, callback, loadFail));
            }
        }
        static public IEnumerator LoadTextureCoroutine(string url, FilterMode filterMode = FilterMode.Bilinear, TextureWrapMode wrapMode = TextureWrapMode.Clamp, Action<Texture2D> callback = null, Action<string> loadFail = null) {
            if (string.IsNullOrEmpty(url)) {
                Debug.LogError("[TextureCache]URL IsNullOrEmpty.");
                yield break;
            }
            while (m_loading.Contains(url)) { yield return null; }
            if (!m_textureList.ContainsKey(url)) {
                m_loading.Add(url);
                using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url)) {
                    uwr.SetRequestHeader("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36");
                    yield return uwr.SendWebRequest();
                    if (uwr.result != UnityWebRequest.Result.Success) {
                        Debug.LogWarning($"[TextureCache]File load error.\n{uwr.error}\n{url}");
                        loadFail?.Invoke(url);
                        yield break;
                    } else {
                        Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                        texture.filterMode = filterMode;
                        texture.wrapMode = wrapMode;
                        texture.name = url;
                        m_textureList[url] = texture;
                        callback?.Invoke(texture);
                    }
                    uwr.Dispose();
                }
                m_loading.Remove(url);
            } else {
                callback?.Invoke(m_textureList[url]);
            }
        }
        static public void Clear() { m_textureList.Clear(); }
    }
}