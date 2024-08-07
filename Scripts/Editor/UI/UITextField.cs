using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UITextField : UIElement {
        Action<string> onContentChanged;
        string _content;
        public UITextField(string text, Action<string> onTextChanged = null) {
            _content = text;
            _style = new GUIStyle(GUI.skin.textArea);
            onContentChanged = onTextChanged;
        }
        protected override void OnDraw() {
            string text = _label == "" ?
                GUILayout.TextField(_content, _style, _options.ToArray()) :
                GUILayout.TextField(_content, _style, _options.ToArray());
            if (text != _content) { onContentChanged?.Invoke(text); }
        }
        public UITextField OnContentChanged(Action<string> action) {
            onContentChanged = action;
            return this;
        }
    }
}
#endif