using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.PlayerLoop;
namespace SorryLab {

    public static class AssetBundleCache {
        static Dictionary<string, AssetBundle> m_assetBundles = new Dictionary<string, AssetBundle>();
        static List<string> m_loading = new List<string>();
        public static AssetBundle GetLoadedAssetBundle(string url) {
            if (IsAssetBundleLoaded(url)) {
                return m_assetBundles[url];
            } else {
                return null;
            }
        }
        static public bool IsAssetBundleLoaded(string url) {
            return m_assetBundles.ContainsKey(url);
        }
        static public void LoadAssetBundle(string url, Action<AssetBundle> callback = null, Action<string> loadFail = null) {
            if (IsAssetBundleLoaded(url)) {
                callback?.Invoke(m_assetBundles[url]);
            } else {
                StaticCoroutine.StartCoroutine(LoadAssetBundleCoroutine(url, callback, loadFail));
            }
        }
        static IEnumerator LoadAssetBundleCoroutine(string url, Action<AssetBundle> callback = null, Action<string> loadFail = null) {
            if (string.IsNullOrEmpty(url)) {
                Debug.LogError("[AssetBundlesCache]URL IsNullOrEmpty.");
                yield break;
            }
            while (m_loading.Contains(url)) { yield return null; }
            if (!m_assetBundles.ContainsKey(url)) {
                m_loading.Add(url);
                using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(url, 0)) {
                    // uwr.SetRequestHeader("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36");
                    yield return uwr.SendWebRequest();
                    if (uwr.result != UnityWebRequest.Result.Success) {
                        Debug.LogWarning($"[AssetBundlesCache]File load error.\n{uwr.error}\n{url}");
                        loadFail?.Invoke(url);
                        yield break;
                    } else {
                        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(uwr);
                        // AssetBundleInfo ae = new AssetBundleInfo {
                        //     assetBundle = ab,
                        //     name = System.IO.Path.GetFileName(url),
                        //     path = url,
                        //     gameObject = LoadGameObject(ab)
                        // };
                        m_assetBundles[url] = ab;
                        callback?.Invoke(ab);
                    }
                    uwr.Dispose();
                }
                m_loading.Remove(url);
            } else {
                callback?.Invoke(m_assetBundles[url]);
            }
        }
        static public void Clear() {
            foreach (AssetBundle ab in m_assetBundles.Values) { ab.Unload(true); }
            m_assetBundles.Clear();
        }
    }
}