using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIHorizontal : UIElement {
        Action onDraw;
        int height;
        public UIHorizontal(Action onDraw, int height = 0) {
            if (height > 0) { SetHeight(height); }
            this.height = height;
            this.onDraw = onDraw;
        }
        protected override void OnDraw() {
            _isBeginHorizontal = true;
            _horizontalHeight = height;
            GUILayout.BeginHorizontal("Box");
            onDraw?.Invoke();
            GUILayout.EndHorizontal();
            _isBeginHorizontal = false;
        }
        public UIHorizontal SetHeight(int height) {
            base.SetHeight(height);
            this.height = height;
            return this;
        }
    }
}
#endif