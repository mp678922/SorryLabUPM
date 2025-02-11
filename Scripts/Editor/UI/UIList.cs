using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIList<T> : UIElement {
        protected Action<T, int> _onDrawElement;
        private List<T> _list;
        private bool _foldout;
        private Action<bool> _onFoldoutChanged;
        private Color _frameColor = Color.white;
        private Action _drawOtherContent;
        public UIList(string label, List<T> list, bool foldout, Action<bool> onFoldoutChanged, Action<T, int> onDrawElement) {
            _label = label;
            _list = list;
            _onDrawElement = onDrawElement;
            _foldout = foldout;
            _onFoldoutChanged = onFoldoutChanged;
        }
        protected override void OnDraw() {
            Layout.Vertical(() => {
                Layout.Foldout($"{_label}[{_list.Count}]", _foldout, _onFoldoutChanged, () => {
                    for (int i = 0; i < _list.Count; i++) {
                        _onDrawElement.Invoke(_list[i], i);
                    }
                    _drawOtherContent?.Invoke();
                }).Draw();
            }).SetColor(_frameColor).Draw();
        }
        public UIList<T> SetFrameColor(Color color) {
            _frameColor = color;
            return this;
        }
        public UIList<T> DrawOtherContent(Action content) {
            _drawOtherContent = content;
            return this;
        }
    }
}
#endif