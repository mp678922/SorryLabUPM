using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SorryLab {
    public static class ColorLog {
        static public void Log(params object[] prints) {
            for (int i = 0; i < prints.Length; i++) { Debug.Log(prints[i]); }
        }
        static public void LogWarning(params object[] prints) {
            for (int i = 0; i < prints.Length; i++) { Debug.LogWarning(prints[i]); }
        }
        static public void LogError(params object[] prints) {
            for (int i = 0; i < prints.Length; i++) { Debug.LogError(prints[i]); }
        }
        static public void Log(object message, Color color) {
            Debug.Log(string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(color), message.ToString()));
        }
        public static void Log(this Debug deb, object message, Color color) {
            Log(message, color);
        }
    }
}
