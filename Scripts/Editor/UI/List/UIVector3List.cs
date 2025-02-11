using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
namespace SorryLab.Editor.UI {
    public class UIVector3List : UIElement {
        ReorderableList reorderableList;
        private List<Vector3> _list = new List<Vector3>();
        public UIVector3List(string label, List<Vector3> list) {
            _label = label;
            _list = list;
            reorderableList = new ReorderableList(_list, typeof(string), true, true, true, true);
            reorderableList.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, _label);
            };
            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                _list[index] = EditorGUI.Vector3Field(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), $"[{index}]", _list[index]);
            };
        }
        protected override void OnDraw() { reorderableList.DoLayoutList(); }
    }
}
#endif