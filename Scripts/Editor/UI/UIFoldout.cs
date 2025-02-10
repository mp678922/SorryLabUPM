using System;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
namespace SorryLab.Editor.UI {
    public class UIFoldout : UIElement {
        private bool _foldout = true;
        private Action _content;
        public UIFoldout(string label, Action content = null, bool foldout = true) {
            _label = label;
            _content = content;
            _foldout = foldout;
        }
        protected override void OnDraw() {
            EditorGUILayout.Foldout(_foldout, _label);
            if (_foldout) { _content?.Invoke(); }
        }
        public UIFoldout SetFoldout(bool foldout) {
            _foldout = foldout;
            return this;
        }
    }
}
#endif