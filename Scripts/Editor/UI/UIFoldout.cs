using System;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
namespace SorryLab.Editor.UI {
    public class UIFoldout : UIElement {
        private bool _foldout = true;
        private Action _content;
        private Action<bool> _onFoldoutChange;
        public UIFoldout(string label, bool foldout = true, Action<bool> onFoldoutChange = null, Action content = null) {
            _label = label;
            _content = content;
            _foldout = foldout;
            _onFoldoutChange = onFoldoutChange;
        }
        protected override void OnDraw() {
            bool foldout = EditorGUILayout.Foldout(_foldout, _label);
            if (foldout != _foldout) { _onFoldoutChange?.Invoke(foldout); }
            if (foldout) { _content?.Invoke(); }
        }
        public UIFoldout SetFoldout(bool foldout) {
            _foldout = foldout;
            return this;
        }
    }
}
#endif