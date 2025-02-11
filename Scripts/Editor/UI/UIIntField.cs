using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIIntField : UIElement {
        Action<int> _onValueChanged;
        int _value;
        public UIIntField(string label, int value, Action<int> onValueChanged = null) {
            _value = value;
            _label = label;
            _onValueChanged = onValueChanged;
        }
        protected override void OnDraw() {
            int value = EditorGUILayout.IntField(_label, _value, _options.ToArray());
            if (value != _value) { _onValueChanged?.Invoke(value); }
        }
        public UIIntField OnValueChanged(Action<int> action) {
            _onValueChanged = action;
            return this;
        }
    }
}
#endif