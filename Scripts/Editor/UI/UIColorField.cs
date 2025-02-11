#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace SorryLab.Editor.UI {
    public class UIColorField : UIElement {
        Action<Color> _onValueChanged;
        Color _value;
        public UIColorField(string label, Color value, Action<Color> onValueChanged = null) {
            _value = value;
            _label = label;
            _onValueChanged = onValueChanged;
        }
        protected override void OnDraw() {
            Color value = EditorGUILayout.ColorField(_label, _value, _options.ToArray());
            if (value != _value) { _onValueChanged?.Invoke(value); }
        }
        public UIColorField OnValueChanged(Action<Color> action) {
            _onValueChanged = action;
            return this;
        }
    }
}
#endif