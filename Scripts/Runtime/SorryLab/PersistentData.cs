using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace SorryLab {
    static public class PersistentData {
        static public string FolderPath(string folder) { return Path.Combine(Application.persistentDataPath, "Save", folder); }
        static public string FilePath(string folder, string fileName) { return Path.Combine(Application.persistentDataPath, "Save", folder, fileName); }
        public static void WriteAllText(string text, string folder, string fileName, bool overwrite = true) {
            CreateFolder(folder);
            string filePath = FilePath(folder, fileName);
            if (File.Exists(filePath)) {
                if (!overwrite) { return; }
                File.Delete(filePath);
            }
            File.WriteAllText(filePath, text);
        }
        public static void WriteAllBytes(byte[] bytes, string folder, string fileName, bool overwrite = true) {
            CreateFolder(folder);
            string filePath = FilePath(folder, fileName);
            if (File.Exists(filePath)) {
                if (!overwrite) { return; }
                File.Delete(filePath);
            }
            File.WriteAllBytes(filePath, bytes);
        }
        public static string ReadAllText(string folder, string fileName) {
            string filePath = FilePath(folder, fileName);
            if (File.Exists(filePath)) {
                return File.ReadAllText(filePath);
            } else {
                return "";
            }
        }
        public static void WriteAllLines(List<string> lines, string folder, string fileName, bool overwrite = true) {
            CreateFolder(folder);
            string filePath = FilePath(folder, fileName);
            if (File.Exists(filePath)) {
                if (!overwrite) { return; }
                File.Delete(filePath);
            }
            File.WriteAllLines(filePath, lines.ToArray());
        }
        public static List<string> ReadAllLines(string folder, string fileName) {
            string filePath = FilePath(folder, fileName);
            filePath = filePath.Replace(@"/", @"\");
            if (File.Exists(filePath)) {
                return new List<string>(File.ReadAllLines(filePath));
            } else {
                return new List<string>();
            }
        }
        public static void CreateFolder(string folder) {
            string folderPath = FolderPath(folder);
            if (!Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
            }
        }

        public static bool ExistsFile(string folder, string fileName) {
            string filePath = FilePath(folder, fileName);
            filePath = filePath.Replace(@"/", @"\");
            return File.Exists(filePath);
        }

        public static bool ExistsFolder(string folder) {
            string folderPath = FolderPath(folder);
            folderPath = folderPath.Replace(@"/", @"\");
            return Directory.Exists(folderPath);
        }

        public static List<string> GetAllFilePath(string folder) {
            string folderPath = FolderPath(folder);
            folderPath = folderPath.Replace(@"/", @"\");
            if (!Directory.Exists(folderPath)) { return new List<string>(); }
            return new List<string>(Directory.GetFiles(folderPath));
        }
        public static List<string> GetAllFileNames(string folder) {
            List<string> files = GetAllFilePath(folder);
            List<string> fileNames = new List<string>();
            for (int i = 0; i < files.Count; i++) {
                fileNames.Add(Path.GetFileName(files[i]));
            }
            return fileNames;
        }
        public static List<string> ReadAllTextInFolder(string folder) {
            List<string> texts = new List<string>();
            foreach (string path in GetAllFilePath(folder)) { texts.Add(File.ReadAllText(path)); }
            return texts;
        }
        public static void ShowFileInExplorer(string folder, string fileName) {
            string filePath = Path.Combine(Application.persistentDataPath, "Save", folder, fileName);
            filePath = filePath.Replace(@"/", @"\");
            System.Diagnostics.Process.Start("explorer.exe", "/select," + @filePath);
        }
        public static void ShowFileInExplorer() {
            string filePath = Path.Combine(Application.persistentDataPath, "Save");
            if (!Directory.Exists(filePath)) {
                Directory.CreateDirectory(filePath);
            }
            filePath = filePath.Replace(@"/", @"\");
            System.Diagnostics.Process.Start("explorer.exe", @filePath);
        }

        public static void ShowFolderInExplorer(string folder) {
            string folderPath = FolderPath(folder);
            folderPath = folderPath.Replace(@"/", @"\");
            System.Diagnostics.Process.Start("explorer.exe", @folderPath);
        }

        public static bool RenameFolder(string folder, string newName) {
            string oldPath = FolderPath(folder);
            string newPath = FolderPath(newName);
            if (!Directory.Exists(oldPath)) { return false; }
            if (Directory.Exists(newPath)) {
                return false;
            } else {
                Directory.Move(oldPath, newPath);
                return true;
            }
        }

    }
}