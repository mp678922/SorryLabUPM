using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIVector2Field : UIElement {
        Action<Vector2> _onValueChanged;
        Vector2 _value;
        public UIVector2Field(string label, Vector2 value, Action<Vector2> onValueChanged = null) {
            _value = value;
            _label = label;
            _onValueChanged = onValueChanged;
        }
        protected override void OnDraw() {
            Vector2 value = EditorGUILayout.Vector2Field(_label, _value, _options.ToArray());
            if (value != _value) { _onValueChanged?.Invoke(value); }
        }
        public UIVector2Field OnValueChanged(Action<Vector2> action) {
            _onValueChanged = action;
            return this;
        }
    }
}
#endif