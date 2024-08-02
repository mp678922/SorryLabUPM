using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System;
using System.Reflection;

#if UNITY_EDITOR
namespace SorryLab.Editor {

    public class PropertyDrawerPlus<T> : PropertyDrawer {

        const float FUNCTION_MENU_WIDTH = 17f;
        const float FUNCTION_MENU_SPACE = 35f;

        protected T target;
        protected SerializedProperty m_property;
        protected Action m_writeProperty;

        protected int IntField(Rect rect, string lable, int value) {
            float lableWidth = Mathf.Min(GetTextWidth(lable), rect.width / 2f);
            GUI.Label(new Rect(rect.x, rect.y, lableWidth, rect.height), lable);
            return EditorGUI.IntField(new Rect(rect.x + lableWidth, rect.y, rect.width - lableWidth, rect.height), value);
        }

        protected float FloatField(Rect rect, string lable, float value) {
            float lableWidth = Mathf.Min(GetTextWidth(lable), rect.width / 2f);
            GUI.Label(new Rect(rect.x, rect.y, lableWidth, rect.height), lable);
            return EditorGUI.FloatField(new Rect(rect.x + lableWidth, rect.y, rect.width - lableWidth, rect.height), value);
        }

        protected static string TextField(Rect rect, string lable, string value) {
            float lableWidth = Mathf.Min(GetTextWidth(lable), rect.width / 2f);
            GUI.Label(new Rect(rect.x, rect.y, lableWidth, rect.height), lable);
            return EditorGUI.TextField(new Rect(rect.x + lableWidth, rect.y, rect.width - lableWidth, rect.height), value);
        }

        protected static Color ColorField(Rect rect, string lable, Color value) {
            float lableWidth = Mathf.Min(GetTextWidth(lable), rect.width / 2f);
            GUI.Label(new Rect(rect.x, rect.y, lableWidth, rect.height), lable);
            return EditorGUI.ColorField(new Rect(rect.x + lableWidth, rect.y, rect.width - lableWidth, rect.height), GUIContent.none, value, false, true, false);
        }

        protected static Vector2 V2Field(Rect rect, string lable, Vector2 value) {
            float lableWidth = Mathf.Min(GetTextWidth(lable), rect.width / 2f);
            GUI.Label(new Rect(rect.x, rect.y, lableWidth, rect.height), lable);
            return EditorGUI.Vector2Field(new Rect(rect.x + lableWidth, rect.y, rect.width - lableWidth, rect.height), GUIContent.none, value);
        }

        protected static Vector3 V3Field(Rect rect, string lable, Vector3 value) {
            float lableWidth = Mathf.Min(GetTextWidth(lable), rect.width / 2f);
            GUI.Label(new Rect(rect.x, rect.y, lableWidth, rect.height), lable);
            return EditorGUI.Vector3Field(new Rect(rect.x + lableWidth, rect.y, rect.width - lableWidth, rect.height), GUIContent.none, value);
        }

        protected static Vector3 V4Field(Rect rect, string lable, Vector3 value) {
            float lableWidth = Mathf.Min(GetTextWidth(lable), rect.width / 2f);
            GUI.Label(new Rect(rect.x, rect.y, lableWidth, rect.height), lable);
            return EditorGUI.Vector4Field(new Rect(rect.x + lableWidth, rect.y, rect.width - lableWidth, rect.height), GUIContent.none, value);
        }

        protected static Rect[] SliceRectHorizontal(Rect rect, bool hasMenu, params float[] clips) {
            if (hasMenu) { rect.width = rect.width - 35f; }
            Rect[] slice = new Rect[clips.Length];
            float total = 0;
            float x = rect.x;
            foreach (var i in clips) { total += i; }
            for (int i = 0; i < clips.Length; i++) {
                float width = rect.width * (clips[i] / total);
                slice[i] = new Rect(x, rect.y, width, rect.height);
                x += width;
            }
            return slice;
        }

        public static Rect GetDefaultRect(Rect rect) {
            return new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
        }

        static int GetTextWidth(string text) {
            return Mathf.RoundToInt(GUI.skin.label.CalcSize(new GUIContent(text)).x);
        }

        sealed public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            m_property = property;
            target = (T)fieldInfo.GetValue(property.serializedObject.targetObject);
            if (target != null) {
                UnityEngine.Object targetComponent = (UnityEngine.Object)GetParent(property);
                EditorGUI.BeginChangeCheck();
                DrawGUI(position, label, property);
                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(targetComponent, "Change PropertyDrawerPlus");
                    if (m_writeProperty != null) {
                        m_writeProperty();
                        m_writeProperty = null;
                    }
                    EditorUtility.SetDirty(targetComponent);
                }
            }
        }

        protected virtual void DrawGUI(Rect position, GUIContent label, SerializedProperty property) { }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            int fieldCount = 1;
            return (fieldCount * EditorGUIUtility.singleLineHeight) + 4f;
        }

        protected void FunctionMenu(Rect rect, SerializedProperty property, params string[] functions) {
            List<string> FunctionList = new List<string>();
            FunctionList.Add("");
            FunctionList.AddRange(functions);
            int function = EditorGUI.Popup(new Rect(rect.width - FUNCTION_MENU_WIDTH, rect.y, FUNCTION_MENU_WIDTH, rect.height), 0, FunctionList.ToArray());
            if (function != 0) {
                Undo.RecordObject((UnityEngine.Object)GetParent(property), "Using Function Meun");
                OnSelectFunction(FunctionList[function]);
            }
        }

        protected virtual void OnSelectFunction(string functionName) { }

        object GetParent(SerializedProperty prop) {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements.Take(elements.Length - 1)) {
                if (element.Contains("[")) {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue(obj, elementName, index);
                } else {
                    obj = GetValue(obj, element);
                }
            }
            return obj;
        }

        object GetValue(object source, string name) {
            if (source == null)
                return null;
            var type = source.GetType();
            var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (f == null) {
                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p == null)
                    return null;
                return p.GetValue(source, null);
            }
            return f.GetValue(source);
        }

        object GetValue(object source, string name, int index) {
            var enumerable = GetValue(source, name) as IEnumerable;
            var enm = enumerable.GetEnumerator();
            while (index-- >= 0)
                enm.MoveNext();
            return enm.Current;
        }

    }

}
#endif