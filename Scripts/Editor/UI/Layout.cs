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
        static public UIFoldout Foldout(string label, bool foldout = true, Action<bool> onFoldoutChanged = null, Action content = null) { return new UIFoldout(label, foldout, onFoldoutChanged, content); }
        static public UIPopupEnum<T> PopupEnum<T>(string label, int index, Action<int> onIndexChanged) where T : Enum { return new UIPopupEnum<T>(label, index, onIndexChanged); }
        static public UIStringList StringList(string label, List<string> list) { return new UIStringList(label, list); }
        static public UIIntField IntField(string label, int value, Action<int> onValueChanged) { return new UIIntField(label, value, onValueChanged); }
        static public UIFloatField FloatField(string label, float value, Action<float> onValueChanged) { return new UIFloatField(label, value, onValueChanged); }
        static public UIVector2Field Vector2Field(string label, Vector2 value, Action<Vector2> onValueChanged) { return new UIVector2Field(label, value, onValueChanged); }
        static public UIVector3Field Vector3Field(string label, Vector3 value, Action<Vector3> onValueChanged) { return new UIVector3Field(label, value, onValueChanged); }
        static public UIVector4Field Vector4Field(string label, Vector4 value, Action<Vector4> onValueChanged) { return new UIVector4Field(label, value, onValueChanged); }
        static public UIScrollView ScrollView(Vector2 position, Action<Vector2> onPositionChanged, Action content) { return new UIScrollView(position, onPositionChanged, content); }
    }
}
#endif