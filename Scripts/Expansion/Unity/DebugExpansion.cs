using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SorryLab.Expansion {
    public static class DebugExpansion {
        static public void Log(params object[] prints) {
            for (int i = 0; i < prints.Length; i++) { Debug.Log(prints[i]); }
        }
        static public void LogWarning(params object[] prints) {
            for (int i = 0; i < prints.Length; i++) { Debug.LogWarning(prints[i]); }
        }
        static public void LogError(params object[] prints) {
            for (int i = 0; i < prints.Length; i++) { Debug.LogError(prints[i]); }
        }
        static public void Log<T>(IEnumerable<T> objects) {
            foreach (T i in objects) { Debug.Log(i.ToString()); }
        }
        static public void LogWarning<T>(IEnumerable<T> objects) {
            foreach (T i in objects) { Debug.LogWarning(i.ToString()); }
        }
        static public void LogError<T>(IEnumerable<T> objects) {
            foreach (T i in objects) { Debug.LogError(i.ToString()); }
        }
        static public void Log(object message, Color color) {
            Debug.Log(StringUtils.RichTextColorRGB(message.ToString(), color));
        }
    }
}
