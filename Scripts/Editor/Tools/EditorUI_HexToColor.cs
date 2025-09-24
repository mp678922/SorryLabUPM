using UnityEditor;
using UnityEngine;
namespace SorryLab.Editor {
    public class EditorUI_HexToColor : EditorWindow {
        string hexInput = "#FFFFFFFF"; // 預設輸入
        Color colorResult = Color.white;
        string colorCodeString = "new Color(1f, 1f, 1f, 1f)";

        [MenuItem("{SorryLab}/Tools/Hex To RGBA Converter")]
        public static void ShowWindow() {
            GetWindow<EditorUI_HexToColor>("Hex To RGBA");
        }

        void OnGUI() {
            GUILayout.Label("Hex ↔ Color 轉換工具", EditorStyles.boldLabel);

            // --- 輸入 HEX ---
            EditorGUI.BeginChangeCheck();
            hexInput = EditorGUILayout.TextField("Hex Input", hexInput);
            if (EditorGUI.EndChangeCheck()) {
                string hex = hexInput.Trim();

                if (!hex.StartsWith("#"))
                    hex = "#" + hex;

                // 六碼自動補 FF
                if (hex.Length == 7) // #RRGGBB
                    hex += "FF";     // → #RRGGBBFF

                if (ColorUtility.TryParseHtmlString(hex, out Color c)) {
                    colorResult = c;
                    UpdateColorCodeString();
                }
            }

            // --- Color 欄位 ---
            EditorGUI.BeginChangeCheck();
            colorResult = EditorGUILayout.ColorField("Color 欄位", colorResult);
            if (EditorGUI.EndChangeCheck()) {
                // 轉回 HEX（帶 Alpha）
                Color32 c32 = colorResult;
                hexInput = $"#{c32.r:X2}{c32.g:X2}{c32.b:X2}{c32.a:X2}";
                UpdateColorCodeString();
            }

            // --- 顯示 RGBA ---
            EditorGUILayout.Space();
            GUILayout.Label("結果：", EditorStyles.boldLabel);

            EditorGUILayout.LabelField("R", (colorResult.r * 255f).ToString("F0"));
            EditorGUILayout.LabelField("G", (colorResult.g * 255f).ToString("F0"));
            EditorGUILayout.LabelField("B", (colorResult.b * 255f).ToString("F0"));
            EditorGUILayout.LabelField("A", (colorResult.a * 255f).ToString("F0"));

            // --- 顯示程式碼字串 ---
            EditorGUILayout.Space();
            GUILayout.Label("程式碼用字串：", EditorStyles.boldLabel);
            EditorGUILayout.TextField(colorCodeString);

            if (GUILayout.Button("複製到剪貼簿")) {
                EditorGUIUtility.systemCopyBuffer = colorCodeString;
                Debug.Log("已複製：" + colorCodeString);
            }
        }

        void UpdateColorCodeString() {
            colorCodeString = $"new Color({colorResult.r:F3}f, {colorResult.g:F3}f, {colorResult.b:F3}f, {colorResult.a:F3}f)";
        }
    }
}