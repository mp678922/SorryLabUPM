using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIMenuButton : UIElement {
        CustomMenu _menu = new CustomMenu();
        protected Action onClicked;
        public UIMenuButton(string text) {
            _label = text;
            _style = new GUIStyle(GUI.skin.button);
            onClicked = () => { _menu.Show(); };
        }
        protected override void OnDraw() {
            if (GUILayout.Button(_label, _style, _options.ToArray())) {
                onClicked?.Invoke();
            }
        }
        public UIMenuButton AddItem(string text, Action action, bool enable = true) {
            _menu.AddItem(text, action, enable);
            return this;
        }
        public UIMenuButton AddDivider() {
            _menu.AddDivider();
            return this;
        }
    }
}
#endif