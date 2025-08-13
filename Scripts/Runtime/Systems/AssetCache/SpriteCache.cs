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
        static public int maxMemoryBytes { get; private set; } = -1;//-1 = 不限制
        void Awake() {
            m_instance = this;
            DontDestroyOnLoad(this);
        }
        /// <summary>
        /// 設定最大圖片數量，超過則釋放Cache。
        /// 可與SetMaxMemorySizeMB一併進行。
        /// </summary>
        /// <param name="count">小於0為不限制，大於0則開始限制。</param>
        public static void SetMaxCacheCount(int count) {
            maxCacheCount = count;
            Refresh();
        }
        /// <summary>
        /// 設定最大暫存記憶體，超過則釋放Cache。
        /// 可與SetMaxCacheCount一併進行。
        /// </summary>
        /// <param name="mb">以Mb為單位，小於0為不限制，大於0則開始限制。</param>
        public static void SetMaxMemorySizeMB(float mb) {
            maxMemoryBytes = (int)(mb * 1048576f);
            Refresh();
        }
        static void Refresh() {
            RefreshByCacheCount();
            RefreshByMemorySize();
        }
        static void RefreshByCacheCount() {
            if (maxCacheCount > 0 && m_spriteList.Count > maxCacheCount) { Release(); }
        }
        static void RefreshByMemorySize() {
            if (maxMemoryBytes > 0 && GetTotalMemoryBytes() > maxMemoryBytes) { Release(); }
        }
        static void Release() {
            List<(string key, CacheData<Sprite> value)> cacheList = new();
            foreach (var i in m_spriteList) { cacheList.Add((i.Key, i.Value)); }
            cacheList = cacheList
                .OrderBy(i => i.value.visitCount)
                .ThenByDescending(i => i.value.memoryBytes)
                .ThenBy(i => i.value.lastVisitTime).ToList();
            int targetCacheNum = Mathf.RoundToInt(cacheList.Count * 0.75f);
            targetCacheNum = Mathf.Max(3, targetCacheNum);
            while (cacheList.Count > targetCacheNum) { cacheList.RemoveAt(0); }
            m_spriteList.Clear();
            for (int i = 0; i < cacheList.Count; i++) { m_spriteList[cacheList[i].key] = cacheList[i].value; }
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
                        Refresh();
                        Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                        texture.filterMode = filterMode;
                        texture.wrapMode = wrapMode;
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
                        m_spriteList[url] = CacheData<Sprite>.Create(sprite).SetMemory(texture);
                        callback?.Invoke(sprite);
                    }
                    uwr.Dispose();
                }
                m_loading.Remove(url);
            } else {
                callback?.Invoke(m_spriteList[url].GetData());
            }
        }
        static public int GetTotalMemoryBytes() {
            int bytes = 0;
            foreach (var i in m_spriteList) { bytes += i.Value.memoryBytes; }
            return bytes;
        }
        static public void Clear() { m_spriteList.Clear(); }
    }
}