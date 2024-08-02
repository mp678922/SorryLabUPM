using System.Collections.Generic;
using System.IO;
using System;
using SorryLab.Expansion;
using UnityEngine;
using UObject = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor {
    public class AssetBundleBuilder {
        [MenuItem("{SorryLab}/AssetBundle/Build WebGL")]
        static void BuildAssetBundles_WebGL() {
            if (CreatePrefabs()) {
                DeleteAllFilesInBuildPath();
                BuildPipeline.BuildAssetBundles(AssetBundleConfig.BUILD_PATH, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.WebGL);
                DeleteAllManifest();
                OpenAssetBundleFolder();
            } else {
                SelectOriginPrefabPath();
            }
        }
        [MenuItem("{SorryLab}/AssetBundle/Build PC")]
        static void BuildAssetBundles_PC() {
            if (CreatePrefabs()) {
                DeleteAllFilesInBuildPath();
                BuildPipeline.BuildAssetBundles(AssetBundleConfig.BUILD_PATH, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
                DeleteAllManifest();
                OpenAssetBundleFolder();
            } else {
                SelectOriginPrefabPath();
            }
        }
        [MenuItem("{SorryLab}/AssetBundle/Create Prefabs")]
        static void MenuCreatePrefabs() {
            if (CreatePrefabs()) {
                SelectBuildPrefabPath();
            } else {
                SelectOriginPrefabPath();
            }
        }
        [MenuItem("{SorryLab}/AssetBundle/Open AssetBundle Folder")]
        static void OpenAssetBundleFolder() {
            if (!Directory.Exists(AssetBundleConfig.BUILD_PATH)) {
                Directory.CreateDirectory(AssetBundleConfig.BUILD_PATH);
            }
            System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(AssetBundleConfig.BUILD_PATH));
        }
        [MenuItem("{SorryLab}/AssetBundle/Delete AssetBundle Folder")]
        static void DeleteExternalAssetFolder() {
            if (Directory.Exists(AssetBundleConfig.BUILD_PATH)) {
                Directory.Delete(AssetBundleConfig.BUILD_PATH, true);
            }
            Debug.Log("AssetBundles have been deleted.");
        }
        static void SelectOriginPrefabPath() {
            Debug.Log($"Please place the Prefab into {AssetBundleConfig.ORIGIN_PREFAB_PATH}.");
            AssetDatabase.Refresh();
            UObject folder = AssetDatabase.LoadAssetAtPath<UObject>(AssetBundleConfig.ORIGIN_PREFAB_PATH);
            Selection.activeObject = folder;
            EditorGUIUtility.PingObject(folder);
        }
        static void SelectBuildPrefabPath() {
            AssetDatabase.Refresh();
            UObject folder = AssetDatabase.LoadAssetAtPath<UObject>(AssetBundleConfig.BUILD_PREFAB_PATH);
            Selection.activeObject = folder;
            EditorGUIUtility.PingObject(folder);
        }
        static bool CreatePrefabs() {
            DeleteAllAssetBundlePrefabs();
            if (!Directory.Exists(AssetBundleConfig.ORIGIN_PREFAB_PATH)) {
                Directory.CreateDirectory(AssetBundleConfig.ORIGIN_PREFAB_PATH);
                return false;
            }
            List<FileInfo> files = GetAllDeepFiles(AssetBundleConfig.ORIGIN_PREFAB_PATH, ".prefab");
            if (files.Count > 0) {
                for (int i = 0; i < files.Count; i++) {
                    FileInfo file = files[i];
                    try {
                        GameObject obj = UObject.Instantiate<GameObject>(AssetDatabase.LoadAssetAtPath<GameObject>(file.path));
                        EditorUtility.DisplayProgressBar("Building Prefabs", obj.name, i.ToFloat() / files.Count.ToFloat());
                        string targetFolder = $"{AssetBundleConfig.BUILD_PREFAB_PATH}/{file.folder}";
                        string targetPath = $"{targetFolder}/{file.name}";
                        string bundleName = Path.GetFileNameWithoutExtension(file.name) + AssetBundleConfig.EXTENSION;
                        if (file.folder != "") { bundleName = file.folder + @"/" + bundleName; }
                        bundleName = bundleName.ToLower();
                        if (!Directory.Exists(targetFolder)) {
                            Directory.CreateDirectory(targetFolder);
                        }
                        foreach (Type type in AssetBundleBuilderHandler.GetDerivedTypes()) {
                            AssetBundleBuilderHandler instance = (AssetBundleBuilderHandler)Activator.CreateInstance(type);
                            instance.Handle(obj);
                        }
                        EditorUtility.SetDirty(obj);
                        AssetDatabase.DeleteAsset(targetPath);
                        PrefabUtility.SaveAsPrefabAsset(obj, targetPath);
                        AssetImporter importer = AssetImporter.GetAtPath(targetPath);
                        importer.assetBundleName = bundleName;
                        UObject.DestroyImmediate(obj);
                    } catch (Exception err) {
                        Debug.LogError(err);
                    }
                }
                EditorUtility.ClearProgressBar();
                AssetDatabase.Refresh();
                return true;
            } else {
                return false;
            }
        }
        static void DeleteAllFilesInBuildPath() {
            if (Directory.Exists(AssetBundleConfig.BUILD_PATH)) {
                Directory.Delete(AssetBundleConfig.BUILD_PATH, true);
            } else {
                Directory.CreateDirectory(AssetBundleConfig.BUILD_PATH);
            }
        }
        static void DeleteAllAssetBundlePrefabs() {
            if (Directory.Exists(AssetBundleConfig.BUILD_PREFAB_PATH)) {
                Directory.Delete(AssetBundleConfig.BUILD_PREFAB_PATH, true);
            }
        }
        static List<FileInfo> GetAllDeepFiles(string folderPath, string extension, string parent = "") {
            List<FileInfo> result = new List<FileInfo>();
            string[] files = Directory.GetFiles(folderPath);
            for (int i = 0; i < files.Length; i++) {
                string path = files[i];
                if (CheckExtension(path, extension)) {
                    string name = Path.GetFileName(path);
                    result.Add(new FileInfo(name, path, parent));
                }
            }
            string[] subFolders = Directory.GetDirectories(folderPath);
            for (int i = 0; i < subFolders.Length; i++) {
                string folder = parent;
                folder += folder == "" ? "" : "/";
                folder += Path.GetFileName(subFolders[i]);
                result.AddRange(GetAllDeepFiles(subFolders[i], extension, folder));
            }
            return result;
        }
        static void DeleteAllManifest() {
            List<FileInfo> files = GetAllDeepFiles(AssetBundleConfig.BUILD_PATH, ".manifest");
            for (int i = 0; i < files.Count; i++) {
                FileInfo file = files[i];
                if (File.Exists(file.path)) { File.Delete(file.path); }
            }
            string assetBundleFile = $"{AssetBundleConfig.BUILD_PATH}/AssetBundles";
            if (File.Exists(assetBundleFile)) { File.Delete(assetBundleFile); }
        }
        static bool CheckExtension(string path, string extension) {
            if (path.Length <= extension.Length) { return false; }
            return path.Substring(path.Length - extension.Length) == extension;
        }
        class FileInfo {
            public string path, name, folder;
            public FileInfo(string name, string path, string folder) {
                this.name = name;
                this.path = path.Replace(@"\", @"/");
                this.folder = folder.Replace(@"\", @"/");
            }
        }
    }
}
#endif