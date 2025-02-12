#if UNITY_EDITOR
using System;
using UnityEngine;
namespace SorryLab.Editor.UI {
    public class UIVertical : UIArea {
        Action onDraw;
        public UIVertical(Action onDraw, int width = 0) {
            if (width > 0) { SetHeight(width); }
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
            GUILayout.BeginVertical(_outlineStyle, _options.ToArray());
            GUILayout.BeginVertical(_backgroundStyle);
            onDraw?.Invoke();
            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }
        void DrawDefault() {
            GUILayout.BeginVertical(_areaStyle, _options.ToArray());
            onDraw?.Invoke();
            GUILayout.EndVertical();
        }
    }
}
#endif