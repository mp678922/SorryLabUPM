using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIDropdown : UIElement {
        int _index = 0;
        List<string> _items = new List<string>();
        Action<int> onItemChanged;
        public UIDropdown(string label, int index, IEnumerable<string> items = null, Action<int> action = null) {
            if (items != null) { AddItems(items); }
            _index = index;
            this._label = label;
            onItemChanged = action;
        }
        public UIDropdown AddItems(IEnumerable<string> items) {
            _items.AddRange(items);
            return this;
        }
        protected override void OnDraw() {
            int index = _label == "" ?
                EditorGUILayout.Popup(_index, _items.ToArray(), _options.ToArray()) :
                EditorGUILayout.Popup(_label, _index, _items.ToArray(), _options.ToArray());
            if (index != _index) { onItemChanged?.Invoke(index); }
        }
        public UIDropdown OnContentChanged(Action<int> action) {
            onItemChanged = action;
            return this;
        }
    }
}
#endif