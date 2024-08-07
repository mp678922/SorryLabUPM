using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UILabel : UIElement {
        public UILabel(string label) { this._label = label; }
        protected override void OnDraw() {
            GUILayout.Label(_label, _style, _options.ToArray());
        }
    }
}
#endif