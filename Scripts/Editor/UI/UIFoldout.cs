using System;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
namespace SorryLab.Editor.UI {
    public class UIFoldout : UIElement {
        private bool _foldout = true;
        private Action _content;
        private Action<bool> _onFoldoutChanged;
        public UIFoldout(string label, bool foldout = true, Action<bool> onFoldoutChanged = null, Action content = null) {
            _label = label;
            _content = content;
            _foldout = foldout;
            _onFoldoutChanged = onFoldoutChanged;
        }
        protected override void OnDraw() {
            bool foldout = EditorGUILayout.Foldout(_foldout, _label);
            if (foldout != _foldout) { _onFoldoutChanged?.Invoke(foldout); }
            if (foldout) { _content?.Invoke(); }
        }
        public UIFoldout SetFoldout(bool foldout) {
            _foldout = foldout;
            return this;
        }
    }
}
#endif