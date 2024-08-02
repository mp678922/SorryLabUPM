using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace SorryLab {
    public class SptireCache : MonoBehaviour {
        static Dictionary<string, Sprite> m_spriteList = new Dictionary<string, Sprite>();
        static List<string> m_loading = new List<string>();
        static SptireCache m_instance;
        void Awake() {
            m_instance = this;
            DontDestroyOnLoad(this);
        }
        public static Sprite GetLoadedSprite(string url) {
            if (IsSpriteLoaded(url)) {
                return m_spriteList[url];
            } else {
                return null;
            }
        }
        static public bool IsSpriteLoaded(string url) {
            return m_spriteList.ContainsKey(url);
        }
        static public void LoadSprite(string url, Action<Sprite> callback = null, Action<string> loadFail = null) {
            if (IsSpriteLoaded(url)) {
                callback?.Invoke(m_spriteList[url]);
            } else {
                m_instance.StartCoroutine(LoadSpriteCoroutine(url, FilterMode.Bilinear, TextureWrapMode.Repeat, callback, loadFail));
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
                        m_spriteList[url] = sprite;
                        callback?.Invoke(sprite);
                    }
                    uwr.Dispose();
                }
                m_loading.Remove(url);
            } else {
                callback?.Invoke(m_spriteList[url]);
            }
        }
        static public void Clear() { m_spriteList.Clear(); }
    }
}