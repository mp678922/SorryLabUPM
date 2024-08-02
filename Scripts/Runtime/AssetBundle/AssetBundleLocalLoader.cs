using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
namespace SorryLab {
    public static class AssetBundleLocalLoader {
        static public void LoadFilesAsync(Action<List<AssetBundleInfo>> callback = null) {
            List<FileInfo> files = GetAllDeepFiles(AssetBundleConfig.LOAD_PATH);
            int count = 0;
            List<AssetBundleInfo> elements = new List<AssetBundleInfo>();
            Action checkComplete = () => {
                if (count == files.Count) {
                    callback?.Invoke(elements);
                }
            };
            for (int i = 0; i < files.Count; i++) {
                FileInfo file = files[i];
                AssetBundleCache.LoadAssetBundle(file.fullPath, ab => {
                    count++;
                    AssetBundleInfo abi = new AssetBundleInfo {
                        assetBundle = ab,
                        name = Path.GetFileName(file.fullPath),
                        path = file.fullPath,
                        folder = file.folder,
                        gameObject = LoadGameObject(ab)
                    };
                    elements.Add(abi);
                    checkComplete.Invoke();
                }, (err) => {
                    count++;
                    checkComplete.Invoke();
                });
            }
        }
        static List<FileInfo> GetAllDeepFiles(string folderPath, string parent = "") {
            List<FileInfo> result = new List<FileInfo>();
            string[] files = Directory.GetFiles(folderPath);
            for (int i = 0; i < files.Length; i++) {
                string path = files[i];
                if (IsAssetBundle(path)) {
                    string name = Path.GetFileName(path);
                    result.Add(new FileInfo(name, path, parent));
                }
            }
            string[] subFolders = Directory.GetDirectories(folderPath);
            for (int i = 0; i < subFolders.Length; i++) {
                string folder = parent;
                folder += folder == "" ? "" : "/";
                folder += Path.GetFileName(subFolders[i]);
                result.AddRange(GetAllDeepFiles(subFolders[i], folder));
            }
            return result;
        }
        static bool IsAssetBundle(string path) {
            string extension = AssetBundleConfig.EXTENSION;
            if (path.Length <= extension.Length) { return false; }
            return path.Substring(path.Length - extension.Length) == extension;
        }
        static GameObject LoadGameObject(AssetBundle ab) {
            GameObject[] gameObjects = ab.LoadAllAssets<GameObject>();
            for (int i = 0; i < gameObjects.Length; i++) {
                if (gameObjects[i].transform.parent == null) { return gameObjects[i]; }
            }
            return null;
        }
        class FileInfo {
            public string localPath, name, folder, fullPath;
            public FileInfo(string name, string path, string folder) {
                this.name = name;
                this.folder = folder.Replace(@"\", @"/");
                localPath = path.Replace(@"\", @"/");
                fullPath = Path.GetFullPath(path);
            }
        }
    }

}