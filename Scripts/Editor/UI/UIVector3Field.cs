using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIVector3Field : UIElement {
        Action<Vector3> _onValueChanged;
        Vector3 _value;
        public UIVector3Field(string label, Vector3 value, Action<Vector3> onValueChanged = null) {
            _value = value;
            _label = label;
            _onValueChanged = onValueChanged;
        }
        protected override void OnDraw() {
            Vector3 value = EditorGUILayout.Vector3Field(_label, _value, _options.ToArray());
            if (value != _value) { _onValueChanged?.Invoke(value); }
        }
        public UIVector3Field OnValueChanged(Action<Vector3> action) {
            _onValueChanged = action;
            return this;
        }
    }
}
#endif