using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIVertical : UIElement {
        Action onDraw;
        int width;
        public UIVertical(Action onDraw, int width = 0) {
            if (width > 0) { SetHeight(width); }
            this.width = width;
            this.onDraw = onDraw;
        }
        protected override void OnDraw() {
            _isBeginVertical = true;
            _verticalWidth = width;
            GUILayout.BeginVertical("Box");
            onDraw?.Invoke();
            GUILayout.EndVertical();
            _isBeginVertical = false;
        }
        public UIVertical SetWidth(int width) {
            base.SetWidth(width);
            this.width = width;
            return this;
        }
    }
}
#endif