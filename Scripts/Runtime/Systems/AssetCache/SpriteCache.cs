using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SorryLab.Cache;
using UnityEngine;
using UnityEngine.Networking;
namespace SorryLab {
    public class SpriteCache : MonoBehaviour {
        static Dictionary<string, CacheData<Sprite>> m_spriteList = new Dictionary<string, CacheData<Sprite>>();
        static List<string> m_loading = new List<string>();
        static SpriteCache m_instance;
        static public int maxCacheCount { get; private set; } = -1;//-1 = 不限制
        void Awake() {
            m_instance = this;
            DontDestroyOnLoad(this);
        }
        public static void SetMaxCacheCount(int count) {
            maxCacheCount = count;
            Refresh();
        }
        static void Refresh() {
            if (maxCacheCount > 0 && m_spriteList.Count > maxCacheCount) {
                List<(string key, CacheData<Sprite> value)> cacheList = new();
                foreach (var i in m_spriteList) { cacheList.Add((i.Key, i.Value)); }
                cacheList = cacheList.OrderBy(i => i.value.useTimes).ToList();
                int targetCacheNum = Mathf.RoundToInt(maxCacheCount * 0.75f);
                while (cacheList.Count > targetCacheNum) { cacheList.RemoveAt(0); }
                m_spriteList.Clear();
                for (int i = 0; i < cacheList.Count; i++) { m_spriteList[cacheList[i].key] = cacheList[i].value; }
            }
        }
        public static Sprite GetLoadedSprite(string url) {
            if (IsSpriteLoaded(url)) {
                return m_spriteList[url].GetData();
            } else {
                return null;
            }
        }
        static public bool IsSpriteLoaded(string url) {
            return m_spriteList.ContainsKey(url);
        }
        static public void LoadSprite(string url, Action<Sprite> callback = null, Action<string> loadFail = null) {
            if (IsSpriteLoaded(url)) {
                callback?.Invoke(m_spriteList[url].GetData());
            } else {
                m_instance.StartCoroutine(LoadSpriteCoroutine(url, FilterMode.Bilinear, TextureWrapMode.Clamp, callback, loadFail));
            }
        }
        static public IEnumerator LoadSpriteCoroutine(string url, FilterMode filterMode = FilterMode.Bilinear, TextureWrapMode wrapMode = TextureWrapMode.Clamp, Action<Sprite> callback = null, Action<string> loadFail = null) {
            if (string.IsNullOrEmpty(url)) {
                Debug.LogError("URL is nothing.");
                yield break;
            }
            while (m_loading.Contains(url)) { yield return null; }
            if (!m_spriteList.ContainsKey(url)) {
                m_loading.Add(url);
                using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url)) {
                    uwr.SetRequestHeader("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36");
                    yield return uwr.SendWebRequest();
                    if (uwr.result != UnityWebRequest.Result.Success) {
                        Debug.LogWarning("File load error.\n" + uwr.error);
                        loadFail?.Invoke(url);
                        yield break;
                    } else {
                        Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                        texture.filterMode = filterMode;
                        texture.wrapMode = wrapMode;
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
                        m_spriteList[url] = CacheData<Sprite>.Create(sprite).SetMemory(texture);
                        callback?.Invoke(sprite);
                        Refresh();
                    }
                    uwr.Dispose();
                }
                m_loading.Remove(url);
            } else {
                callback?.Invoke(m_spriteList[url].GetData());
            }
        }
        static public void Clear() { m_spriteList.Clear(); }
    }
}