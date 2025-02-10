using System;
#if UNITY_EDITOR
using UnityEditor;
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
            _foldout = EditorGUILayout.Foldout(_foldout, _label, _style);
        }
        public UIFoldout SetFoldout(bool foldout) {
            _foldout = foldout;
            return this;
        }
        public override void Draw() {
            base.Draw();
            if (_foldout) { _content?.Invoke(); }
        }
    }
}
#endif