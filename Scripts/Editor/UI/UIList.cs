using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIList<T> : UIElement {
        CustomMenu _menu = new CustomMenu();
        protected Func<T, T> _onDrawElement;
        private List<T> _list;
        private bool _foldout;
        private Action<bool> _onFoldoutChanged;
        private Color _frameColor = Color.white;
        public UIList(string label, List<T> list, bool foldout, Action<bool> onFoldoutChanged, Func<T, T> onDrawElement) {
            _label = label;
            _list = list;
            _onDrawElement = onDrawElement;
            _foldout = foldout;
            _onFoldoutChanged = onFoldoutChanged;
        }
        protected override void OnDraw() {
            Layout.Vertical(() => {
                Layout.Foldout(_label, _foldout, _onFoldoutChanged, () => {
                    for (int i = 0; i < _list.Count; i++) {
                        _list[i] = _onDrawElement.Invoke(_list[i]);
                    }
                }).Draw();
            }).SetColor(_frameColor).Draw();
        }
        public void SetFrameColor(Color color) { _frameColor = color; }
    }
}
#endif