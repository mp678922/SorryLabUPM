using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIVertical : UIElement {
        Action onDraw;
        int width;
        static GUIStyle _outlineStyle;
        static GUIStyle _backgroundStyle;
        bool _drawOutline = false;
        public UIVertical(Action onDraw, int width = 0) {
            if (width > 0) { SetHeight(width); }
            this.width = width;
            this.onDraw = onDraw;
        }
        protected override void OnDraw() {
            _isBeginVertical = true;
            _verticalWidth = width;
            if (_drawOutline) {
                DrawOutline();
            } else {
                DrawDefault();
            }

            _isBeginVertical = false;
        }
        void DrawOutline() {
            if (_outlineStyle == null || _backgroundStyle == null) {
                Texture2D borderTexture = new Texture2D(1, 1);
                borderTexture.SetPixel(0, 0, GUI.color = EditorGUIUtility.isProSkin ? new Color(0.18f, 0.18f, 0.18f) : new Color(0.71f, 0.71f, 0.71f));
                borderTexture.Apply();
                _outlineStyle = new GUIStyle(GUI.skin.box);
                _outlineStyle.normal.background = borderTexture;
                borderTexture = new Texture2D(1, 1);
                borderTexture.SetPixel(0, 0, GUI.color = EditorGUIUtility.isProSkin ? new Color(0.22f, 0.22f, 0.22f) : new Color(0.76f, 0.76f, 0.76f));
                borderTexture.Apply();
                _backgroundStyle = new GUIStyle(GUI.skin.box);
                _backgroundStyle.normal.background = borderTexture;
                _outlineStyle.padding = new RectOffset(-10, -10, -10, -10);
            }
            GUILayout.BeginVertical(_outlineStyle);
            GUILayout.BeginVertical(_backgroundStyle);
            onDraw?.Invoke();
            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }
        void DrawDefault() {
            GUILayout.BeginVertical("Box");
            onDraw?.Invoke();
            GUILayout.EndVertical();
        }
        public UIVertical SetOutline(bool value) {
            _drawOutline = value;
            return this;
        }
        public UIVertical SetWidth(int width) {
            base.SetWidth(width);
            this.width = width;
            return this;
        }
    }
}
#endif