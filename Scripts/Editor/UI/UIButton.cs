using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIButton : UIElement {
        protected Action onClicked;
        public UIButton(string text) : this(text, null) { }
        public UIButton(string text, Action onClicked = null) {
            _label = text;
            _style = new GUIStyle(GUI.skin.button);
            this.onClicked = onClicked;
        }
        protected override void OnDraw() {
            if (GUILayout.Button(_label, _style, _options.ToArray())) {
                onClicked?.Invoke();
            }
        }
        public UIButton OnClick(Action action) {
            onClicked = action;
            return this;
        }
    }
}
#endif