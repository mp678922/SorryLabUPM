#if UNITY_EDITOR
using System;
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
            bool foldout = _foldout;
            Layout.Vertical(() => {
                Layout.Vertical(() => {
                    foldout = EditorGUILayout.Foldout(_foldout, _label);
                }).Draw();
                Layout.Vertical(() => {
                    if (foldout != _foldout) { _onFoldoutChanged?.Invoke(foldout); }
                    if (foldout) { _content?.Invoke(); }
                }).Draw();
            }).Draw();
        }
        public UIFoldout SetFoldout(bool foldout) {
            _foldout = foldout;
            return this;
        }
    }
}
#endif