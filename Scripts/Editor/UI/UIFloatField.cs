using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIFloatField : UIElement {
        Action<float> _onValueChanged;
        float _value;
        public UIFloatField(string label, float value, Action<float> onValueChanged = null) {
            _value = value;
            _label = label;
            _onValueChanged = onValueChanged;
        }
        protected override void OnDraw() {
            float value = (float)EditorGUILayout.FloatField(_label, _value, _options.ToArray());
            if (value != _value) { _onValueChanged?.Invoke(value); }
        }
        public UIFloatField OnValueChanged(Action<float> action) {
            _onValueChanged = action;
            return this;
        }
    }
}
#endif