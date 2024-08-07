using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIObjectField<T> : UIElement where T : UnityEngine.Object {
        Action<T> onContentChanged;
        T _content;
        bool allowSceneObjects;
        public UIObjectField(string label, T obj, bool allowSceneObjects = true, Action<T> onContentChanged = null) {
            this._label = label;
            _content = obj;
            _style = new GUIStyle(GUI.skin.textField);
            this.onContentChanged = onContentChanged;
            this.allowSceneObjects = allowSceneObjects;
        }
        protected override void OnDraw() {
            T obj = _label == "" ?
                (T)EditorGUILayout.ObjectField(_content, typeof(T), allowSceneObjects, _options.ToArray()) :
                (T)EditorGUILayout.ObjectField(_label, _content, typeof(T), true, _options.ToArray());
            if (obj != _content) { onContentChanged?.Invoke(obj); }
        }
        public UIObjectField<T> OnContentChanged(Action<T> action) {
            onContentChanged = action;
            return this;
        }
    }
}
#endif