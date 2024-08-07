using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UITextArea : UIElement {
        Action<string> onContentChanged;
        string _content;
        public UITextArea(string content, Action<string> onContentChanged = null) {
            _content = content;
            _style = new GUIStyle(GUI.skin.textArea);
            this.onContentChanged = onContentChanged;
        }
        protected override void OnDraw() {
            string text = GUILayout.TextArea(_content, _style, _options.ToArray());
            if (text != _content) { onContentChanged?.Invoke(text); }
        }
        public UITextArea OnContentChanged(Action<string> action) {
            onContentChanged = action;
            return this;
        }
    }
}
#endif