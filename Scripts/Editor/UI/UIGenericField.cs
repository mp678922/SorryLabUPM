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
    private List<string> _cullingFields = new List<string>();
    public UIGenericField(string label, T target) {
        _target = target;
        _label = label;
        _values = GetPublicFields(target);
    }
    public UIGenericField<T> SetCullingFields(params string[] fieldNames) {
        for (int i = 0; i < fieldNames.Length; i++) {
            if (!_cullingFields.Contains(fieldNames[i])) { _cullingFields.Add(fieldNames[i]); }
        }
        return this;
    }
    public override void Draw() {
        Layout.Vertical(() => {
            Layout.Foldout($"{_label}({_target.GetType().Name})", _foldout, v => _foldout = v, () => {
                for (int i = 0; i < _values.Count; i++) {
                    if (_cullingFields.Contains(_values[i].fieldName)) { continue; }
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
                fieldType == typeof(Vector4) || fieldType == typeof(Color) ||
                fieldType == typeof(List<int>) || fieldType == typeof(List<float>) ||
                fieldType == typeof(List<bool>) || fieldType == typeof(List<int>) ||
                fieldType == typeof(List<Vector2>) || fieldType == typeof(List<Vector3>) ||
                fieldType == typeof(List<Vector4>) || fieldType == typeof(List<Color>)) {
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
        private UIBoolList _boolList;
        private UIStringList _stringList;
        private UIFloatList _floatList;
        private UIIntList _intList;
        private UIVector2List _vector2List;
        private UIVector3List _vector3List;
        private UIVector4List _vector4List;
        private UIColorList _colorList;
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
            } else if (fieldType == typeof(List<int>)) {
                if (_intList == null) { _intList = Layout.IntList(fieldName, (List<int>)fieldValue); }
                _intList.Draw();
            } else if (fieldType == typeof(List<float>)) {
                if (_floatList == null) { _floatList = Layout.FloatList(fieldName, (List<float>)fieldValue); }
                _floatList.Draw();
            } else if (fieldType == typeof(List<bool>)) {
                if (_boolList == null) { _boolList = Layout.BoolList(fieldName, (List<bool>)fieldValue); }
                _boolList.Draw();
            } else if (fieldType == typeof(List<string>)) {
                if (_stringList == null) { _stringList = Layout.StringList(fieldName, (List<string>)fieldValue); }
                _stringList.Draw();
            } else if (fieldType == typeof(List<Vector2>)) {
                if (_vector2List == null) { _vector2List = Layout.Vector2List(fieldName, (List<Vector2>)fieldValue); }
                _vector2List.Draw();
            } else if (fieldType == typeof(List<Vector3>)) {
                if (_vector3List == null) { _vector3List = Layout.Vector3List(fieldName, (List<Vector3>)fieldValue); }
                _vector3List.Draw();
            } else if (fieldType == typeof(List<Vector4>)) {
                if (_vector4List == null) { _vector4List = Layout.Vector4List(fieldName, (List<Vector4>)fieldValue); }
                _vector4List.Draw();
            } else if (fieldType == typeof(List<Color>)) {
                if (_colorList == null) { _colorList = Layout.ColorList(fieldName, (List<Color>)fieldValue); }
                _colorList.Draw();
            }
        }
    }
}
#endif