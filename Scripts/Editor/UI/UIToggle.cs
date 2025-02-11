#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIToggle : UIElement {
        Action<bool> _onValueChanged;
        bool _value;
        public UIToggle(string label, bool value, Action<bool> onValueChanged = null) {
            _value = value;
            _label = label;
            _onValueChanged = onValueChanged;
        }
        protected override void OnDraw() {
            bool value = EditorGUILayout.Toggle(_label, _value, _options.ToArray());
            if (value != _value) { _onValueChanged?.Invoke(value); }
        }
        public UIToggle OnValueChanged(Action<bool> action) {
            _onValueChanged = action;
            return this;
        }
    }
}
#endif