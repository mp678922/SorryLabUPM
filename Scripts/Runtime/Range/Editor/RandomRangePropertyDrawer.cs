using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SorryLab.Editor;

namespace SorryLab.Range {

    [CustomPropertyDrawer(typeof(IntRange))]
    public class RangeIntPropertyDrawer : PropertyDrawerPlus<IntRange> {

        static int[] clipboard = new int[2];

        protected override void DrawGUI(Rect position, GUIContent label, SerializedProperty property) {
            Rect singleFieldRect = GetDefaultRect(position);
            FunctionMenu(singleFieldRect, property, "Copy", "Paste", "Average");
            Rect[] rects = SliceRectHorizontal(singleFieldRect, true, .4f, .3f, .3f);
            EditorGUI.PrefixLabel(rects[0], 0, label);
            int min = IntField(rects[1], "Min", target.min);
            int max = IntField(rects[2], "Max", target.max);
            m_writeProperty = () => {
                target.min = min;
                target.max = max;
            };
        }

        protected override void OnSelectFunction(string function) {
            switch (function) {
                case "Copy": clipboard[0] = target.min; clipboard[1] = target.max; break;
                case "Paste": target.min = clipboard[0]; target.max = clipboard[1]; break;
                case "Average": target.min = target.max = target.GetAverage(); break;
            }
        }
    }

    [CustomPropertyDrawer(typeof(FloatRange))]
    public class RangeFloatPropertyDrawer : PropertyDrawerPlus<FloatRange> {

        static float[] clipboard = new float[2];

        protected override void DrawGUI(Rect position, GUIContent label, SerializedProperty property) {
            Rect singleFieldRect = GetDefaultRect(position);
            FunctionMenu(singleFieldRect, property, "Copy", "Paste", "Average");
            Rect[] rects = SliceRectHorizontal(singleFieldRect, true, .4f, .3f, .3f);
            EditorGUI.PrefixLabel(rects[0], 0, label);
            float min = FloatField(rects[1], "Min", target.min);
            float max = FloatField(rects[2], "Max", target.max);
            m_writeProperty = () => {
                target.min = min;
                target.max = max;
            };
        }

        protected override void OnSelectFunction(string function) {
            switch (function) {
                case "Copy": clipboard[0] = target.min; clipboard[1] = target.max; break;
                case "Paste": target.min = clipboard[0]; target.max = clipboard[1]; break;
                case "Average": target.min = target.max = target.GetAverage(); break;
            }
        }
    }

    [CustomPropertyDrawer(typeof(ColorRange))]
    public class RangeColorPropertyDrawer : PropertyDrawerPlus<ColorRange> {

        static Color[] clipboard = new Color[2];

        protected override void DrawGUI(Rect position, GUIContent label, SerializedProperty property) {
            Rect singleFieldRect = GetDefaultRect(position);
            FunctionMenu(singleFieldRect, property, "Copy", "Paste", "Average");
            Rect[] rects = SliceRectHorizontal(singleFieldRect, true, .4f, .3f, .3f);
            EditorGUI.PrefixLabel(rects[0], 0, label);
            Color min = ColorField(rects[1], "Min", target.min);
            Color max = ColorField(rects[2], "Max", target.max);
            m_writeProperty = () => {
                target.min = min;
                target.max = max;
            };
        }

        protected override void OnSelectFunction(string function) {
            switch (function) {
                case "Copy": clipboard[0] = target.min; clipboard[1] = target.max; break;
                case "Paste": target.min = clipboard[0]; target.max = clipboard[1]; break;
                case "Average": target.min = target.max = target.GetAverage(); break;
            }
        }
    }

    [CustomPropertyDrawer(typeof(Color32Range))]
    public class RangeColor32PropertyDrawer : PropertyDrawerPlus<Color32Range> {

        static Color32[] clipboard = new Color32[2];

        protected override void DrawGUI(Rect position, GUIContent label, SerializedProperty property) {
            Rect singleFieldRect = GetDefaultRect(position);
            FunctionMenu(singleFieldRect, property, "Copy", "Paste", "Average");
            Rect[] rects = SliceRectHorizontal(singleFieldRect, true, .4f, .3f, .3f);
            EditorGUI.PrefixLabel(rects[0], 0, label);
            Color min = ColorField(rects[1], "Min", target.min);
            Color max = ColorField(rects[2], "Max", target.max);
            m_writeProperty = () => {
                target.min = min;
                target.max = max;
            };
        }

        protected override void OnSelectFunction(string function) {
            switch (function) {
                case "Copy": clipboard[0] = target.min; clipboard[1] = target.max; break;
                case "Paste": target.min = clipboard[0]; target.max = clipboard[1]; break;
                case "Average": target.min = target.max = target.GetAverage(); break;
            }
        }
    }

    [CustomPropertyDrawer(typeof(Vector2Range))]
    public class RangeVector2PropertyDrawer : PropertyDrawerPlus<Vector2Range> {

        static Vector2[] clipboard = new Vector2[2];

        protected override void DrawGUI(Rect position, GUIContent label, SerializedProperty property) {
            Rect singleFieldRect = GetDefaultRect(position);
            FunctionMenu(singleFieldRect, property, "Copy", "Paste", "Average", "Normalized");
            Rect[] rects = SliceRectHorizontal(singleFieldRect, true, .4f, .3f, .3f);
            EditorGUI.PrefixLabel(rects[0], 0, label);
            Vector2 min = V2Field(rects[1], "Min", target.min);
            Vector2 max = V2Field(rects[2], "Max", target.max);
            m_writeProperty = () => {
                target.min = min;
                target.max = max;
            };
        }

        protected override void OnSelectFunction(string function) {
            switch (function) {
                case "Copy": clipboard[0] = target.min; clipboard[1] = target.max; break;
                case "Paste": target.min = clipboard[0]; target.max = clipboard[1]; break;
                case "Average": target.min = target.max = target.GetAverage(); break;
                case "Normalized": target.min = target.min.normalized; target.max = target.max.normalized; break;
            }
        }
    }

    [CustomPropertyDrawer(typeof(Vector3Range))]
    public class RangeVector3PropertyDrawer : PropertyDrawerPlus<Vector3Range> {

        static Vector3[] clipboard = new Vector3[2];

        protected override void DrawGUI(Rect position, GUIContent label, SerializedProperty property) {
            Rect singleFieldRect = GetDefaultRect(position);
            FunctionMenu(singleFieldRect, property, "Copy", "Paste", "Average", "Normalized");
            Rect[] rects = SliceRectHorizontal(singleFieldRect, true, .4f, .3f, .3f);
            EditorGUI.PrefixLabel(rects[0], 0, label);
            Vector3 min = V3Field(rects[1], "Min", target.min);
            Vector3 max = V3Field(rects[2], "Max", target.max);
            m_writeProperty = () => {
                target.min = min;
                target.max = max;
            };
        }

        protected override void OnSelectFunction(string function) {
            switch (function) {
                case "Copy": clipboard[0] = target.min; clipboard[1] = target.max; break;
                case "Paste": target.min = clipboard[0]; target.max = clipboard[1]; break;
                case "Average": target.min = target.max = target.GetAverage(); break;
                case "Normalized": target.min = target.min.normalized; target.max = target.max.normalized; break;
            }
        }
    }

    [CustomPropertyDrawer(typeof(Vector4Range))]
    public class RangeVector4PropertyDrawer : PropertyDrawerPlus<Vector4Range> {

        static Vector4[] clipboard = new Vector4[2];

        protected override void DrawGUI(Rect position, GUIContent label, SerializedProperty property) {
            Rect singleFieldRect = GetDefaultRect(position);
            FunctionMenu(singleFieldRect, property, "Copy", "Paste", "Average", "Normalized");
            Rect[] rects = SliceRectHorizontal(singleFieldRect, true, .4f, .3f, .3f);
            EditorGUI.PrefixLabel(rects[0], 0, label);
            Vector4 min = V4Field(rects[1], "Min", target.min);
            Vector4 max = V4Field(rects[2], "Max", target.max);
            m_writeProperty = () => {
                target.min = min;
                target.max = max;
            };
        }

        protected override void OnSelectFunction(string function) {
            switch (function) {
                case "Copy": clipboard[0] = target.min; clipboard[1] = target.max; break;
                case "Paste": target.min = clipboard[0]; target.max = clipboard[1]; break;
                case "Average": target.min = target.max = target.GetAverage(); break;
                case "Normalized": target.min = target.min.normalized; target.max = target.max.normalized; break;
            }
        }
    }

    [CustomPropertyDrawer(typeof(QuaternionRange))]
    public class RangeQuaternionPropertyDrawer : PropertyDrawerPlus<QuaternionRange> {

        static Quaternion[] clipboard = new Quaternion[2];

        protected override void DrawGUI(Rect position, GUIContent label, SerializedProperty property) {
            Rect singleFieldRect = GetDefaultRect(position);
            FunctionMenu(singleFieldRect, property, "Copy", "Paste", "Average");
            Rect[] rects = SliceRectHorizontal(singleFieldRect, true, .4f, .3f, .3f);
            EditorGUI.PrefixLabel(rects[0], 0, label);
            Vector3 min = V3Field(rects[1], "Min", target.minEular);
            Vector3 max = V3Field(rects[2], "Max", target.maxEular);
            m_writeProperty = () => {
                target.minEular = min;
                target.maxEular = max;
            };
        }

        protected override void OnSelectFunction(string function) {
            switch (function) {
                case "Copy": clipboard[0] = target.min; clipboard[1] = target.max; break;
                case "Paste": target.min = clipboard[0]; target.max = clipboard[1]; break;
                case "Average": target.min = target.max = target.GetAverage(); break;
            }
        }
    }

}

