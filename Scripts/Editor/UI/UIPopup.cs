using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIPopup : UIElement {
        Action<int> _onIndexChanged;
        string[] _items;
        int _index;
        public UIPopup(string label, string[] items, int index, Action<int> onIndexChange = null) {
            _index = index;
            _onIndexChanged = onIndexChange;
            _label = label;
            _items = items;
        }
        protected override void OnDraw() {
            int index = EditorGUILayout.Popup(_label, _index, _items);
            if (index != _index) { _onIndexChanged?.Invoke(index); }
        }
        public UIPopup OnIndexChanged(Action<int> action) {
            _onIndexChanged = action;
            return this;
        }
    }
}
#endif