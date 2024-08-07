using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIElement {
        static protected bool _isBeginHorizontal = false;
        static protected int _horizontalHeight;
        static protected bool _isBeginVertical = false;
        static protected int _verticalWidth;
        protected string _label = "";
        protected Color _color = Color.white;
        protected bool _isEnable = true;
        protected List<GUILayoutOption> _options = new List<GUILayoutOption>();
        virtual protected void OnDraw() { }
        protected GUIStyle _style;
        public UIElement() { _style = new GUIStyle(GUI.skin.label); }
        public UIElement SetTextAnchor(TextAnchor alignment) {
            _style.alignment = alignment;
            return this;
        }
        public UIElement SetColor(Color color) {
            _color = color;
            return this;
        }
        public UIElement SetEnable(bool enable) {
            _isEnable = enable;
            return this;
        }
        public UIElement SetWidth(float width) {
            _options.Add(GUILayout.Width(width));
            return this;
        }
        public UIElement SetHeight(float height) {
            _options.Add(GUILayout.Height(height));
            _style.fixedHeight = height;
            return this;
        }
        public UIElement AddGUILayoutOptions(params GUILayoutOption[] options) {
            _options.AddRange(options);
            return this;
        }
        public void Draw() {
            if (_isBeginHorizontal && _horizontalHeight > 0) { SetHeight(_horizontalHeight); }
            if (_isBeginVertical && _verticalWidth > 0) { SetWidth(_verticalWidth); }
            Color originColor = GUI.color;
            bool originEnable = GUI.enabled;
            GUI.color = _color;
            GUI.enabled = _isEnable;
            OnDraw();
            GUI.color = originColor;
            GUI.enabled = originEnable;
        }
    }
}
#endif