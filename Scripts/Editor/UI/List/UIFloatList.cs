using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
namespace SorryLab.Editor.UI {
    public class UIFloatList : UIElement {
        ReorderableList reorderableList;
        private List<float> _list = new List<float>();
        public UIFloatList(string label, List<float> list) {
            _label = label;
            _list = list;
            reorderableList = new ReorderableList(_list, typeof(string), true, true, true, true);
            reorderableList.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, _label);
            };
            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                _list[index] = EditorGUI.FloatField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), _list[index]);
            };
        }
        protected override void OnDraw() { reorderableList.DoLayoutList(); }
    }
}
#endif