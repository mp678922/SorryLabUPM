using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SorryLab.Cache;
using UnityEngine;
using UnityEngine.Networking;
namespace SorryLab {
    public class TextureCache : MonoBehaviour {
        static Dictionary<string, CacheData<Texture2D>> m_textureList = new Dictionary<string, CacheData<Texture2D>>();
        static List<string> m_loading = new List<string>();
        static TextureCache m_instance;
        static public int maxCacheCount { get; private set; } = -1;//-1 = 不限制
        void Awake() {
            m_instance = this;
            transform.parent = null;
            hideFlags = HideFlags.HideInHierarchy;
            DontDestroyOnLoad(gameObject);
        }
        public static void SetMaxCacheCount(int count) {
            maxCacheCount = count;
            Refresh();
        }
        static void Refresh() {
            if (maxCacheCount > 0 && m_textureList.Count > maxCacheCount) {
                List<(string key, CacheData<Texture2D> value)> cacheList = new();
                foreach (var i in m_textureList) { cacheList.Add((i.Key, i.Value)); }
                cacheList = cacheList.OrderBy(i => i.value.useTimes).ToList();
                int targetCacheNum = Mathf.RoundToInt(maxCacheCount * 0.75f);
                while (cacheList.Count > targetCacheNum) { cacheList.RemoveAt(0); }
                m_textureList.Clear();
                for (int i = 0; i < cacheList.Count; i++) { m_textureList[cacheList[i].key] = cacheList[i].value; }
            }
        }
        public static Texture2D GetLoadedTexture(string url) {
            if (IsTextureLoaded(url)) {
                return m_textureList[url].GetData();
            } else {
                return null;
            }
        }
        static public bool IsTextureLoaded(string url) {
            return m_textureList.ContainsKey(url);
        }
        static public void LoadTexture(string url, Action<Texture2D> callback = null, Action<string> loadFail = null) {
            if (IsTextureLoaded(url)) {
                callback?.Invoke(m_textureList[url].GetData());
            } else {
                m_instance.StartCoroutine(LoadTextureCoroutine(url, FilterMode.Bilinear, TextureWrapMode.Clamp, callback, loadFail));
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
                        Refresh();
                        Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                        texture.filterMode = filterMode;
                        texture.wrapMode = wrapMode;
                        texture.name = url;
                        m_textureList[url] = CacheData<Texture2D>.Create(texture).SetMemory(texture);
                        callback?.Invoke(texture);
                    }
                    uwr.Dispose();
                }
                m_loading.Remove(url);
            } else {
                callback?.Invoke(m_textureList[url].GetData());
            }
        }
        static public void Clear() { m_textureList.Clear(); }
    }
}