using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SorryLab.Expansion;
namespace SorryLab {
    public static class Clipboard {
        static ClipboardData _data;
        static string _currentSystem = "";
        static Clipboard() {
            if (PersistentData.ExistsFile("SorryLab", "clipboard.json")) {
                _data = JsonUtility.FromJson<ClipboardData>(PersistentData.ReadAllText("SorryLab", "clipboard.json"));
            } else {
                _data = new ClipboardData();
            }
        }
        public static void Write(string text) {
            if (_currentSystem == "") {
                _currentSystem = text;
            } else {
                _currentSystem = $"{_currentSystem}\n{text}";
            }
        }
        public static bool Apply() {
            if (_currentSystem == "") { return false; }
            if (_data.system.Count >= 10) { _data.system.Dequeue(); }
            _data.system.Add(_currentSystem);
            _currentSystem = "";
            Save();
#if UNITY_EDITOR
            Editor.EditorUIClipboard.ShowNewClipboardInfo();
#endif
            return true;
        }
        public static void AppendNote(string text) {
            _data.note.Add(text);
            Save();
        }
        public static void WriteNote(int index, string text) {
            if (index < _data.note.Count) {
                _data.note[index] = text;
            }
            Save();
        }
        public static void Delete(int index) {
            if (index < _data.system.Count) {
                _data.system.RemoveAt(index);
            }
            Save();
        }
        public static void DeleteNote(int index) {
            if (index < _data.note.Count) {
                _data.note.RemoveAt(index);
            }
            Save();
        }
        public static string Get(int index) {
            if (index < _data.system.Count && index >= 0) {
                return _data.system[index];
            }
            return "";
        }
        public static string GetNote(int index) {
            if (index < _data.note.Count && index >= 0) {
                return _data.note[index];
            }
            return "";
        }
        public static int GetSystemCount() { return _data.system.Count; }
        public static int GetNoteCount() { return _data.note.Count; }
        static void Save() {
            PersistentData.WriteAllText(JsonUtility.ToJson(_data), "SorryLab", "clipboard.json");
        }
    }
}
