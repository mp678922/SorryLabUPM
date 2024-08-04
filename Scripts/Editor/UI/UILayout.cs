using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;
using System.Reflection.Emit;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer.Operations;



#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor {
    public static class UILayout {
        static bool _isBeginHorizontal = false;
        static int _horizontalHeight;
        static bool _isBeginVertical = false;
        static int _verticalWidth;
        static public UIHorizontal Horizontal(Action content) { return new UIHorizontal(content); }
        static public UIButton Button(string text, Action onClicked = null) { return new UIButton(text, onClicked); }
        static public UITextArea TextArea(string text, Action<string> onContentChanged = null) { return new UITextArea(text, onContentChanged); }
        static public UILabel Label(string text) { return new UILabel(text); }
        static public UIMenuButton MenuButton(string text) { return new UIMenuButton(text); }
        static public UIObjectField<T> ObjectField<T>(string text, T obj, bool allowSceneObjects = true, Action<T> onContentChanged = null) where T : UnityEngine.Object {
            return new UIObjectField<T>(text, obj, allowSceneObjects, onContentChanged);
        }
        static public UIObjectField<T> ObjectField<T>(T obj, bool allowSceneObjects = true, Action<T> onContentChanged = null) where T : UnityEngine.Object {
            return new UIObjectField<T>("", obj, allowSceneObjects, onContentChanged);
        }
        public class UILabel : UIElement {
            public UILabel(string label) { this.label = label; }
            protected override void OnDraw() {
                GUILayout.Label(label, _style, _options.ToArray());
            }
        }
        public class UIHorizontal : UIElement {
            Action onDraw;
            int height;
            public UIHorizontal(Action onDraw, int height = 0) {
                if (height > 0) { SetHeight(height); }
                this.height = height;
                this.onDraw = onDraw;
            }
            protected override void OnDraw() {
                _isBeginHorizontal = true;
                _horizontalHeight = height;
                GUILayout.BeginHorizontal("Box");
                onDraw?.Invoke();
                GUILayout.EndHorizontal();
                _isBeginHorizontal = false;
            }
            public UIHorizontal SetHeight(int height) {
                base.SetHeight(height);
                this.height = height;
                return this;
            }
        }
        public class UIVertical : UIElement {
            Action onDraw;
            int width;
            public UIVertical(Action onDraw, int width = 0) {
                if (width > 0) { SetHeight(width); }
                this.width = width;
                this.onDraw = onDraw;
            }
            protected override void OnDraw() {
                _isBeginVertical = true;
                _verticalWidth = width;
                GUILayout.BeginHorizontal("Box");
                onDraw?.Invoke();
                GUILayout.EndHorizontal();
                _isBeginVertical = false;
            }
            public UIVertical SetWidth(int width) {
                base.SetWidth(width);
                this.width = width;
                return this;
            }
        }
        public class UIObjectField<T> : UIElement where T : UnityEngine.Object {
            Action<T> onContentChanged;
            T _content;
            bool allowSceneObjects;
            public UIObjectField(string label, T obj, bool allowSceneObjects = true, Action<T> onContentChanged = null) {
                this.label = label;
                _content = obj;
                _style = new GUIStyle(GUI.skin.textField);
                this.onContentChanged = onContentChanged;
                this.allowSceneObjects = allowSceneObjects;
            }
            protected override void OnDraw() {
                T changeable = label == "" ?
                    (T)EditorGUILayout.ObjectField(_content, typeof(T), allowSceneObjects, _options.ToArray()) :
                    (T)EditorGUILayout.ObjectField(label, _content, typeof(T), true, _options.ToArray());
                if (changeable != _content) { onContentChanged?.Invoke(changeable); }
            }
            public UIObjectField<T> OnContentChanged(Action<T> action) {
                onContentChanged = action;
                return this;
            }
        }
        public class UITextArea : UIElement {
            Action<string> onContentChanged;
            string _content;
            public UITextArea(string content, Action<string> onContentChanged = null) {
                _content = content;
                _style = new GUIStyle(GUI.skin.textArea);
                this.onContentChanged = onContentChanged;
            }
            protected override void OnDraw() {
                string changeable = GUILayout.TextArea(_content, _style, _options.ToArray());
                if (changeable != _content) { onContentChanged?.Invoke(changeable); }
            }
            public UITextArea OnContentChanged(Action<string> action) {
                onContentChanged = action;
                return this;
            }
        }
        public class UIMenuButton : UIElement {
            CustomMenu _menu = new CustomMenu();
            protected Action onClicked;
            public UIMenuButton(string text) {
                label = text;
                _style = new GUIStyle(GUI.skin.button);
                onClicked = () => { _menu.Show(); };
            }
            protected override void OnDraw() {
                if (GUILayout.Button(label, _style, _options.ToArray())) {
                    onClicked?.Invoke();
                }
            }
            public UIMenuButton AddItem(string text, Action action, bool enable = true) {
                _menu.AddItem(text, action, enable);
                return this;
            }
            public UIMenuButton AddDivider() {
                _menu.AddDivider();
                return this;
            }
        }
        public class UIButton : UIElement {
            protected Action onClicked;
            public UIButton(string text) : this(text, null) { }
            public UIButton(string text, Action onClicked = null) {
                label = text;
                _style = new GUIStyle(GUI.skin.button);
                this.onClicked = onClicked;
            }
            protected override void OnDraw() {
                if (GUILayout.Button(label, _style, _options.ToArray())) {
                    onClicked?.Invoke();
                }
            }
            public UIButton OnClick(Action action) {
                onClicked = action;
                return this;
            }
        }

        public class UIElement {
            protected string label = "";
            protected Color _color = Color.white;
            protected bool _isEnable = true;
            protected List<GUILayoutOption> _options = new List<GUILayoutOption>();
            virtual protected void OnDraw() { }
            protected GUIStyle _style;
            public UIElement() { _style = new GUIStyle(GUI.skin.label); }
            public UIElement SetTextAnchor(TextAnchor alignment) {
                _style.alignment = alignment;
                return this;
            }
            public UIElement SetColor(Color color) {
                _color = color;
                return this;
            }
            public UIElement SetEnable(bool enable) {
                _isEnable = enable;
                return this;
            }
            public UIElement SetWidth(float width) {
                _options.Add(GUILayout.Width(width));
                return this;
            }
            public UIElement SetHeight(float height) {
                _options.Add(GUILayout.Height(height));
                _style.fixedHeight = height;
                return this;
            }
            public UIElement AddGUILayoutOptions(params GUILayoutOption[] options) {
                _options.AddRange(options);
                return this;
            }
            public void Draw() {
                if (_isBeginHorizontal && _horizontalHeight > 0) { SetHeight(_horizontalHeight); }
                if (_isBeginVertical && _verticalWidth > 0) { SetWidth(_verticalWidth); }
                Color originColor = GUI.color;
                bool originEnable = GUI.enabled;
                GUI.color = _color;
                GUI.enabled = _isEnable;
                OnDraw();
                GUI.color = originColor;
                GUI.enabled = originEnable;
            }
        }
    }
}
#endif