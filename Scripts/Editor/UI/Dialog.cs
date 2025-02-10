#if UNITY_EDITOR
using System;
using UnityEditor;
namespace SorryLab.Editor.UI {
    public static class Dialog {
        public static void AskConfirmOrCancel(string title, string context, Action onConfirm, Action onCancel = null) {
            bool confirm = EditorUtility.DisplayDialog(
                title,
                context,
                "是",
                "否"
            );
            if (confirm) { onConfirm?.Invoke(); } else { onCancel?.Invoke(); }
        }
    }
}
#endif