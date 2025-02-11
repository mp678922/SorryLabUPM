using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIList<T> : UIElement {
        static ScriptableCache _cache;
        private List<T> _list;
        public UIList(string label, List<T> list) {
            _label = label;
            _list = list;
        }
        protected override void OnDraw() {
            if (_cache == null) { _cache = ScriptableObject.CreateInstance<ScriptableCache>(); }
            _cache.list = _list;
            SerializedObject serializedObject = new SerializedObject(_cache);
            SerializedProperty stringListProperty = serializedObject.FindProperty("list");
            EditorGUILayout.PropertyField(stringListProperty, new GUIContent("_label"), true);
            serializedObject.ApplyModifiedProperties();
        }
        public class ScriptableCache : ScriptableObject {
            public List<T> list = new List<T>();
        }
    }
}
#endif