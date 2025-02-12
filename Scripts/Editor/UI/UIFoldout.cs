#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace SorryLab.Editor.UI {
    public class UIFoldout : UIArea {
        private bool _foldout = true;
        private Action _content;
        private Action _labelContent;
        private Action<bool> _onFoldoutChanged;
        private List<MenuItem> _menuItems = new List<MenuItem>();
        public UIFoldout(string label, bool foldout = true, Action<bool> onFoldoutChanged = null, Action content = null) {
            _label = label;
            _content = content;
            _foldout = foldout;
            _onFoldoutChanged = onFoldoutChanged;
            SetAreaType(UIAreaType.Outline);
        }
        public UIFoldout(string label, bool foldout = true, Action content = null) {
            _label = label;
            _content = content;
            _foldout = foldout;
            SetAreaType(UIAreaType.Outline);
        }
        public UIFoldout AddMenuItem(string menuText, Action action) {
            _menuItems.Add(new MenuItem { menuText = menuText, action = action });
            return this;
        }
        protected override void OnDraw() {
            bool foldout = _foldout;
            UIVertical frame = Layout.Vertical(() => {
                Layout.Horizontal(() => {
                    foldout = EditorGUILayout.Foldout(_foldout, _label);
                    _labelContent?.Invoke();
                    DrawMenuButton();
                }).SetAreaType(UIAreaType.Box).Draw();
                if (foldout) {
                    Layout.Vertical(() => {
                        _content?.Invoke();
                    }).SetAreaType(UIAreaType.None).Draw();
                }
            });
            frame.AddGUILayoutOptions(_options.ToArray());
            frame.SetAreaType(_areaType).Draw();
            if (foldout != _foldout) {
                _onFoldoutChanged?.Invoke(foldout);
                _foldout = foldout;
            }
        }
        void DrawMenuButton() {
            if (_menuItems.Count == 0) { return; }
            UIMenuButton menuButton = Layout.MenuButton("☰");
            for (int i = 0; i < _menuItems.Count; i++) {
                MenuItem item = _menuItems[i];
                menuButton.AddItem(item.menuText, item.action);
            }
            menuButton.SetWidth(20).Draw();
        }
        public UIFoldout AddLabelContent(Action labelContent) {
            _labelContent += labelContent;
            return this;
        }
        public UIFoldout SetFoldout(bool foldout) {
            _foldout = foldout;
            return this;
        }
        internal class MenuItem {
            public string menuText;
            public Action action;
        }
    }
}
#endif