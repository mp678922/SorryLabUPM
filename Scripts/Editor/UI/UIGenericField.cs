#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using SorryLab.Editor.UI;
using UnityEngine;
public class UIGenericField<T> : UIElement {
    private List<VariableInfo> _values;
    private T _target;
    private bool _foldout;
    public UIGenericField(string label, T target) {
        _target = target;
        _label = label;
        _values = GetPublicFields(target);
    }
    public override void Draw() {
        Layout.Vertical(() => {
            Layout.Foldout($"{_label}({_target.GetType().Name})", _foldout, v => _foldout = v, () => {
                for (int i = 0; i < _values.Count; i++) {
                    _values[i].Draw(_target);
                }
            }).Draw();
        }).Draw();
    }
    static List<VariableInfo> GetPublicFields(object obj) {
        List<VariableInfo> result = new List<VariableInfo>();
        Type type = obj.GetType();
        foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Instance)) {
            Type fieldType = field.FieldType;
            if (fieldType == typeof(int) || fieldType == typeof(float) ||
                fieldType == typeof(bool) || fieldType == typeof(string) ||
                fieldType == typeof(Vector2) || fieldType == typeof(Vector3) ||
                fieldType == typeof(Vector4) || fieldType == typeof(Color)) {
                result.Add(new VariableInfo {
                    fieldInfo = field,
                    fieldName = field.Name,
                    fieldType = fieldType,
                    fieldValue = field.GetValue(obj)
                });
            }
        }
        return result;
    }
    internal class VariableInfo {
        public FieldInfo fieldInfo { get; set; }
        public string fieldName { get; set; }
        public Type fieldType { get; set; }
        public object fieldValue { get; set; }
        public void Draw(T target) {
            if (fieldType == typeof(string)) {
                Layout.TextField((string)fieldValue, s => fieldInfo.SetValue(target, s)).SetLabel(fieldName).Draw();
            } else if (fieldType == typeof(int)) {
                Layout.IntField(fieldName, (int)fieldValue, s => fieldInfo.SetValue(target, s)).Draw();
            } else if (fieldType == typeof(float)) {
                Layout.FloatField(fieldName, (float)fieldValue, s => fieldInfo.SetValue(target, s)).Draw();
            } else if (fieldType == typeof(Vector2)) {
                Layout.Vector2Field(fieldName, (Vector2)fieldValue, s => fieldInfo.SetValue(target, s)).Draw();
            } else if (fieldType == typeof(Vector3)) {
                Layout.Vector3Field(fieldName, (Vector3)fieldValue, s => fieldInfo.SetValue(target, s)).Draw();
            } else if (fieldType == typeof(Vector4)) {
                Layout.Vector4Field(fieldName, (Vector4)fieldValue, s => fieldInfo.SetValue(target, s)).Draw();
            } else if (fieldType == typeof(bool)) {
                Layout.Toggle(fieldName, (bool)fieldValue, s => fieldInfo.SetValue(target, s)).Draw();
            } else if (fieldType == typeof(Color)) {
                Layout.ColorField(fieldName, (Color)fieldValue, s => fieldInfo.SetValue(target, s)).Draw();
            }
        }
    }
}
#endif