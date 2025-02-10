using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public static class Layout {
        static public UIHorizontal Horizontal(Action content) { return new UIHorizontal(content); }
        static public UIVertical Vertical(Action content) { return new UIVertical(content); }
        static public UIButton Button(string text, Action onClicked = null) { return new UIButton(text, onClicked); }
        static public UITextArea TextArea(string text, Action<string> onContentChanged = null) { return new UITextArea(text, onContentChanged); }
        static public UILabel Label(string label) { return new UILabel(label); }
        static public UIMenuButton MenuButton(string text) { return new UIMenuButton(text); }
        static public UIObjectField<T> ObjectField<T>(string label, T obj, bool allowSceneObjects = true, Action<T> onContentChanged = null) where T : UnityEngine.Object {
            return new UIObjectField<T>(label, obj, allowSceneObjects, onContentChanged);
        }
        static public UIObjectField<T> ObjectField<T>(T label, bool allowSceneObjects = true, Action<T> onContentChanged = null) where T : UnityEngine.Object {
            return new UIObjectField<T>("", label, allowSceneObjects, onContentChanged);
        }
        static public UIDropdown Dropdown(string label, int index, IEnumerable<string> items, Action<int> onIndexChanged = null) { return new UIDropdown(label, index, items, onIndexChanged); }
        static public UIDropdown Dropdown(int index, IEnumerable<string> items, Action<int> onIndexChanged = null) { return new UIDropdown("", index, items, onIndexChanged); }
        static public UITextField TextField(string text, Action<string> onTextChanged = null) { return new UITextField(text, onTextChanged); }
        static public UIFoldout Foldout(string label, bool foldout = true, Action<bool> onValueChanged = null, Action content = null) { return new UIFoldout(label, foldout, onValueChanged, content); }
        static public UIPopupEnum<T> PopupEnum<T>(string label, int index, Action<int> onIndexChanged) where T : Enum { return new UIPopupEnum<T>(label, index, onIndexChanged); }
    }
}
#endif