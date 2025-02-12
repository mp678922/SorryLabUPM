using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIElement {
        protected string _label = "";
        protected Color _color = Color.white;
        protected bool _isEnable = true;
        protected List<GUILayoutOption> _options {
            get {
                List<GUILayoutOption> options = new List<GUILayoutOption>();
                if (_widthOption != null) { options.Add(_widthOption); }
                if (_heightOption != null) { options.Add(_heightOption); }
                if (_otherOptions.Count != 0) { options.AddRange(_otherOptions); }
                return options;
            }
        }
        protected GUILayoutOption _widthOption;
        protected GUILayoutOption _heightOption;
        private List<GUILayoutOption> _otherOptions = new List<GUILayoutOption>();
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
        public virtual UIElement SetWidth(float width) {
            _widthOption = GUILayout.Width(width);
            return this;
        }
        public virtual UIElement SetHeight(float height) {
            _heightOption = GUILayout.Height(height);
            _style.fixedHeight = height;
            return this;
        }
        public UIElement AddGUILayoutOptions(params GUILayoutOption[] options) {
            _otherOptions = options.ToList();
            return this;
        }
        public UIElement SetLabel(string label) {
            _label = label;
            return this;
        }
        public virtual void Draw() {
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