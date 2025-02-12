using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIHorizontal : UIArea {
        Action onDraw;
        int height;
        public UIHorizontal(Action onDraw, int height = 0) {
            if (height > 0) { SetHeight(height); }
            this.height = height;
            this.onDraw = onDraw;
        }
        protected override void OnDraw() {
            if (_areaType == UIAreaType.Outline) {
                DrawOutline();
            } else {
                DrawDefault();
            }
        }
        void DrawOutline() {
            GUILayout.BeginHorizontal(_outlineStyle, _options.ToArray());
            GUILayout.BeginHorizontal(_backgroundStyle);
            onDraw?.Invoke();
            GUILayout.EndHorizontal();
            GUILayout.EndHorizontal();
        }
        void DrawDefault() {
            GUILayout.BeginHorizontal(_areaStyle, _options.ToArray());
            onDraw?.Invoke();
            GUILayout.EndHorizontal();
        }
        public UIHorizontal SetHeight(int height) {
            base.SetHeight(height);
            this.height = height;
            return this;
        }
    }
}
#endif