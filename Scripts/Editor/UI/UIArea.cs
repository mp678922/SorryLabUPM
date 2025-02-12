using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIArea : UIElement {

        static internal GUIStyle _outlineStyle;
        static internal GUIStyle _backgroundStyle;
        protected UIAreaType _areaType;
        protected GUIStyle _areaStyle {
            get { if (_areaType == UIAreaType.Box) { return GUI.skin.box; } else { return GUIStyle.none; } }
        }
        static UIArea() {
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
        }
        public UIArea SetAreaType(UIAreaType type) {
            _areaType = type;
            return this;
        }
    }
}
#endif