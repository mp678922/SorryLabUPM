using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
namespace SorryLab.Editor.UI {
    public class UIStringList : UIElement {
        ReorderableList reorderableList;
        private List<string> _list = new List<string>();
        public UIStringList(string label, List<string> list) {
            _label = label;
            _list = list;
            reorderableList = new ReorderableList(_list, typeof(string), true, true, true, true);
            reorderableList.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, _label);
            };
            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                _list[index] = EditorGUI.TextField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), _list[index]);
            };
        }
        protected override void OnDraw() { reorderableList.DoLayoutList(); }
    }
}
#endif