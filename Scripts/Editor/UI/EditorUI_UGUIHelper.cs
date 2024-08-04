using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;
using SFieldInfo = System.Reflection.FieldInfo;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor {
    public class EditorUI_UGUIHelper : EditorWindow {
        static List<Type> _typeList = new List<Type> {
            typeof(TMP_Text),
            typeof(TMP_InputField),
            typeof(TMP_Dropdown),
            typeof(Button),
            typeof(Toggle),
            typeof(Slider),
        };
        static Dictionary<Type, string> ExecuteMenuItemCommand = new Dictionary<Type, string>() {
            {typeof(TMP_Text),"GameObject/UI/Text - TextMeshPro"},
            {typeof(TMP_InputField),"GameObject/UI/Input Field - TextMeshPro"},
            {typeof(TMP_Dropdown),"GameObject/UI/Dropdown - TextMeshPro"},
            {typeof(Button),"GameObject/UI/Button - TextMeshPro"},
            {typeof(Toggle),"GameObject/UI/Toggle"},
            {typeof(Slider),"GameObject/UI/Slider"},
        };
        static EditorUI_UGUIHelper _instance;
        [MenuItem("{SorryLab}/Tools/UGUIHelper")]
        public static void ShowWindow() {
            if (_instance == null) {
                _instance = GetWindow<EditorUI_UGUIHelper>("UGUIHelper");
            }
            _instance.AutoFindCurrentGameObject();
            _instance.Focus();
        }
        GameObject _target;
        int _componentIndex;
        void OnGUI() {
            UILayout.ObjectField<GameObject>(_target, true, (go) => { _target = go; }).Draw();
            if (_target != null) { DrawGameObject(_target); }
        }
        void AutoFindCurrentGameObject() {
            if (Selection.activeGameObject == null) return;
            Component[] components = Selection.activeGameObject.GetComponents(typeof(Component));
            for (int i = 0; i < components.Length; i++) {
                if (HasEligibleField(components[i])) {
                    _target = Selection.activeGameObject;
                    _componentIndex = i;
                    return;
                }
            }

        }
        void DrawGameObject(GameObject go) {
            Component[] components = go.GetComponents(typeof(Component));
            Dictionary<string, Component> dic = new Dictionary<string, Component>();
            for (int i = 0; i < components.Length; i++) {
                string typeName = components[i].GetType().Name;
                if (!dic.ContainsKey(typeName)) {
                    dic[typeName] = components[i];
                }
            }
            string[] keys = dic.Keys.ToArray();
            if (_componentIndex >= keys.Length) { _componentIndex = 0; }
            _componentIndex = EditorGUILayout.Popup(_componentIndex, keys);
            Component component = dic[keys[_componentIndex]];
            List<FieldInfo> fieldInfos = FindPublicUIFields(component);
            foreach (FieldInfo i in fieldInfos) {
                DrawFields(component, i);
            }
            if (fieldInfos.Count > 0) {
                UILayout.Button("Print Script", () => {
                    PrintScript(component, fieldInfos);
                }).SetColor(Color.cyan)
                .Draw();
            }
        }
        void DrawFields(Component component, FieldInfo fieldInfo) {
            UILayout.Horizontal(() => {
                UILayout.Label(fieldInfo.name)
                .SetWidth(Mathf.Max(100f, position.width * 0.25f))
                .SetTextAnchor(TextAnchor.MiddleLeft)
                .Draw();
                if (fieldInfo.isNull()) {
                    UILayout.Button("Create", () => {
                        GameObject newObject = CreateUIObject(fieldInfo, component);
                        if (fieldInfo.type == typeof(TMP_Text)) { SetValue<TMP_Text>(component, newObject, fieldInfo.name); }
                        if (fieldInfo.type == typeof(TMP_InputField)) { SetValue<TMP_InputField>(component, newObject, fieldInfo.name); }
                        if (fieldInfo.type == typeof(TMP_Dropdown)) { SetValue<TMP_Dropdown>(component, newObject, fieldInfo.name); }
                        if (fieldInfo.type == typeof(Button)) { SetValue<Button>(component, newObject, fieldInfo.name); }
                        if (fieldInfo.type == typeof(Toggle)) { SetValue<Toggle>(component, newObject, fieldInfo.name); }
                        if (fieldInfo.type == typeof(Slider)) { SetValue<Slider>(component, newObject, fieldInfo.name); }
                    }).Draw();
                } else {
                    UILayout.ObjectField<UnityEngine.Object>(fieldInfo.value)
                    .SetEnable(false).Draw();
                }
            }).SetHeight(20).Draw();
        }
        GameObject CreateUIObject(FieldInfo fieldInfo, Component parent) {
            EditorApplication.ExecuteMenuItem(ExecuteMenuItemCommand[fieldInfo.type]);
            GameObject newObject = Selection.activeGameObject;
            newObject.name = fieldInfo.name;
            newObject.transform.SetParent(parent.transform);
            newObject.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            newObject.GetComponent<RectTransform>().localScale = Vector3.one;
            EditorUtility.SetDirty(newObject);
            EditorGUIUtility.PingObject(newObject);
            return newObject;
        }
        void SetValue<T>(Component component, GameObject gameObject, string fieldName) {
            SFieldInfo sFieldInfo = component.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);
            if (sFieldInfo != null) {
                sFieldInfo.SetValue(component, gameObject.GetComponentInChildren<T>());
            };
            PropertyInfo propertyInfo = component.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo != null) {
                propertyInfo.SetValue(component, gameObject.GetComponentInChildren<T>(), null);
            };
        }
        private List<FieldInfo> FindPublicUIFields(UnityEngine.Object obj) {
            List<FieldInfo> fieldInfos = new List<FieldInfo>();
            Type type = obj.GetType();
            SFieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (SFieldInfo field in fields) {
                if (_typeList.Contains(field.FieldType)) {
                    fieldInfos.Add(new FieldInfo(field.FieldType, field.Name, (UnityEngine.Object)field.GetValue(obj)));
                }
            }
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties) {
                if (_typeList.Contains(property.PropertyType) && property.CanRead) {
                    fieldInfos.Add(new FieldInfo(property.PropertyType, property.Name, (UnityEngine.Object)property.GetValue(obj, null)));
                }
            }
            return fieldInfos;
        }
        bool HasEligibleField(Component component) {
            return FindPublicUIFields(component).Count > 0;
        }
        void PrintScript(Component component, List<FieldInfo> fieldInfos) {
            Clipboard.Write($"//{component.GetType().Name}");
            Clipboard.Write("void Start() {");
            foreach (FieldInfo i in fieldInfos) {
                string capitalizeFirstLetter = StringUtils.CapitalizeFirstLetter(i.name);
                if (i.type == typeof(Button)) {
                    Clipboard.Write($"\t{i.name}.onClick.AddListener(OnClick_{capitalizeFirstLetter});");
                }
                if (i.type == typeof(TMP_InputField)) {
                    Clipboard.Write($"\t{i.name}.onEndEdit.AddListener(OnEndEdit_{capitalizeFirstLetter});");
                }
                if (i.type == typeof(TMP_Dropdown)) {
                    Clipboard.Write($"\t{i.name}.onValueChanged.AddListener(OnValueChanged_{capitalizeFirstLetter});");
                }
                if (i.type == typeof(Toggle)) {
                    Clipboard.Write($"\t{i.name}.onValueChanged.AddListener(OnValueChanged_{capitalizeFirstLetter});");
                }
                if (i.type == typeof(Slider)) {
                    Clipboard.Write($"\t{i.name}.onValueChanged.AddListener(OnValueChanged_{capitalizeFirstLetter});");
                }
            }
            Clipboard.Write("}");
            foreach (FieldInfo i in fieldInfos) {
                string capitalizeFirstLetter = StringUtils.CapitalizeFirstLetter(i.name);
                if (i.type == typeof(Button)) {
                    Clipboard.Write($"private void OnClick_{capitalizeFirstLetter}() {{ }}");
                }
                if (i.type == typeof(TMP_InputField)) {
                    Clipboard.Write($"private void OnEndEdit_{capitalizeFirstLetter}(string value) {{ }}");
                }
                if (i.type == typeof(TMP_Dropdown)) {
                    Clipboard.Write($"private void OnValueChanged_{capitalizeFirstLetter}(int value) {{ }}");
                }
                if (i.type == typeof(Toggle)) {
                    Clipboard.Write($"private void OnValueChanged_{capitalizeFirstLetter}(bool value) {{ }}");
                }
                if (i.type == typeof(Slider)) {
                    Clipboard.Write($"private void OnValueChanged_{capitalizeFirstLetter}(float value) {{ }}");
                }
            }
            Clipboard.Apply();
        }

        class FieldInfo {
            public string name;
            public Type type;
            public UnityEngine.Object value;
            public FieldInfo(Type type, string name, UnityEngine.Object value) {
                this.name = name;
                this.value = value;
                this.type = type;
            }
            public bool isNull() {
                return value == null;
            }
        }
    }
}
#endif