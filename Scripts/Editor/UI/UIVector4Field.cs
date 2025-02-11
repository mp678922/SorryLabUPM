using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIVector4Field : UIElement {
        Action<Vector4> _onValueChanged;
        Vector4 _value;
        public UIVector4Field(string label, Vector3 value, Action<Vector4> onValueChanged = null) {
            _value = value;
            _label = label;
            _onValueChanged = onValueChanged;
        }
        protected override void OnDraw() {
            Vector4 value = EditorGUILayout.Vector4Field(_label, _value, _options.ToArray());
            if (value != _value) { _onValueChanged?.Invoke(value); }
        }
        public UIVector4Field OnValueChanged(Action<Vector4> action) {
            _onValueChanged = action;
            return this;
        }
    }
}
#endif