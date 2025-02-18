#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace SorryLab.Editor.UI {
    public class UIFoldout : UIArea {
        private bool _foldout = true;
        private Action _content;
        public Action onPreMenuButtonClick;
        private Dictionary<string, Action> _labelContents = new Dictionary<string, Action>();
        private Action<bool> _onFoldoutChanged;
        // private List<MenuItem> _menuItems = new List<MenuItem>();
        private Dictionary<string, MenuItem> _menuItems = new Dictionary<string, MenuItem>();
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
        public UIFoldout SetMenuItem(string menuText, Action action, bool enable = true) {
            _menuItems[menuText] = new MenuItem { menuText = menuText, action = action, enable = enable };
            return this;
        }
        protected override void OnDraw() {
            bool foldout = _foldout;
            UIVertical frame = Layout.Vertical(() => {
                Layout.Horizontal(() => {
                    foldout = EditorGUILayout.Foldout(_foldout, _label);
                    foreach (string i in _labelContents.Keys) { _labelContents[i]?.Invoke(); }
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
            onPreMenuButtonClick?.Invoke();
            if (_menuItems.Count == 0) { return; }
            UIMenuButton menuButton = Layout.MenuButton("☰");
            List<string> keys = new List<string>(_menuItems.Keys);
            for (int i = 0; i < keys.Count; i++) {
                MenuItem item = _menuItems[keys[i]];
                menuButton.AddItem(item.menuText, item.action, item.enable);
            }
            menuButton.SetWidth(20).Draw();
        }
        public UIFoldout SetLabelContent(string key, Action labelContent) {
            _labelContents[key] = labelContent;
            return this;
        }
        public UIFoldout SetFoldout(bool foldout) {
            _foldout = foldout;
            return this;
        }
        internal class MenuItem {
            public string menuText;
            public Action action;
            public bool enable;
        }
    }
}
#endif