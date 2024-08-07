using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using SorryLab.Editor.UI;
namespace SorryLab.Editor {
    public class EditorUIClipboard : EditorWindow {
        static EditorUIClipboard _instance;
        static int _mode = 0;
        static int _systemIndex = 0;
        static int _noteIndex = 0;
        [MenuItem("{SorryLab}/Tools/Clipboard")]
        public static void ShowWindow() {
            if (_instance == null) {
                _instance = GetWindow<EditorUIClipboard>("Clipboard");
            }
        }
        public static void ShowNewClipboardInfo() {
            ShowWindow();
            _mode = 0;
            _systemIndex = Clipboard.GetSystemCount() - 1;
            _instance.Focus();
        }
        private void OnGUI() {
            _mode = EditorGUILayout.Popup(_mode, new string[] { "System", "Note" });
            if (_mode == 0) {
                DrawSystem();
            } else {
                DrawNote();
            }
        }
        void DrawSystem() {
            float windowHeight = position.height;
            _systemIndex = Mathf.Clamp(_systemIndex, 0, Clipboard.GetSystemCount() - 1);
            if (Clipboard.GetSystemCount() == 0) { _systemIndex = -1; }

            Layout.TextArea(Clipboard.Get(_systemIndex))
            .SetHeight(windowHeight - 68)
            .SetEnable(_systemIndex != -1)
            .Draw();

            int height = 35;

            Layout.Horizontal(() => {
                //NextButton
                UI.Layout.Button("<", OnSystemPrevButtonClick)
                .SetHeight(height)
                .SetWidth(height)
                .SetEnable(_systemIndex > 0)
                .Draw();

                //DeleteButton
                Layout.Button("Delete", OnSystemDeleteClick)
                .SetWidth(100)
                .SetHeight(height)
                .SetColor(Color.red)
                .SetEnable(_systemIndex >= 0)
                .Draw();

                //LabelCount
                Layout.Label($"({_systemIndex + 1}/{Clipboard.GetSystemCount()})")
                .SetTextAnchor(TextAnchor.MiddleCenter)
                .SetHeight(height)
                .Draw();

                //SaveButton
                Layout.Button("Move to note", OnMoveToNoteClick)
                .SetWidth(100)
                .SetHeight(height)
                .SetColor(Color.green)
                .SetEnable(_systemIndex >= 0)
                .Draw();

                //NextButton
                Layout.Button(">", OnSystemNextButtonClick)
                .SetHeight(height)
                .SetWidth(height)
                .SetEnable(_systemIndex < Clipboard.GetSystemCount() - 1)
                .Draw();
            }).SetHeight(height)
            .Draw();
        }
        void OnSystemNextButtonClick() {
            _systemIndex++;
        }
        void OnSystemPrevButtonClick() {
            _systemIndex--;
        }
        void OnMoveToNoteClick() {
            Clipboard.AppendNote(Clipboard.Get(_systemIndex));
            Clipboard.Delete(_systemIndex);
        }
        void OnSystemDeleteClick() {
            Clipboard.Delete(_systemIndex);
        }

        void DrawNote() {
            float windowHeight = position.height;
            _noteIndex = Mathf.Clamp(_noteIndex, 0, Clipboard.GetNoteCount() - 1);
            if (Clipboard.GetNoteCount() == 0) { _noteIndex = -1; }

            Layout.TextArea(Clipboard.GetNote(_noteIndex), OnNoteContentUpdate)
            .SetHeight(windowHeight - 68)
            .SetEnable(_noteIndex >= 0)
            .Draw();

            int height = 35;

            Layout.Horizontal(() => {
                //PrevButton
                Layout.Button("<", OnNotePrevButtonClick)
                .SetWidth(height)
                .SetHeight(height)
                .SetEnable(_noteIndex > 0)
                .Draw();

                //DeleteButton
                Layout.Button("Delete", OnNoteDeleteClick)
                .SetWidth(100)
                .SetHeight(height)
                .SetColor(Color.red)
                .SetEnable(_noteIndex >= 0)
                .Draw();

                //LabelCount
                Layout.Label($"({_noteIndex + 1}/{Clipboard.GetNoteCount()})")
                .SetTextAnchor(TextAnchor.MiddleCenter)
                .SetHeight(height)
                .Draw();

                //NewButton
                Layout.Button("New note", OnNoteNewNoteClick)
                .SetWidth(100)
                .SetHeight(height)
                .SetColor(Color.green)
                .Draw();

                //NextButton
                Layout.Button(">", OnNoteNextButtonClick)
                .SetHeight(height)
                .SetWidth(height)
                .SetEnable(_noteIndex < Clipboard.GetNoteCount() - 1)
                .Draw();
            }).SetHeight(height)
            .Draw();
        }
        void OnNoteNextButtonClick() {
            _noteIndex++;
        }
        void OnNotePrevButtonClick() {
            _noteIndex--;
        }
        void OnNoteNewNoteClick() {
            Clipboard.AppendNote("");
            _noteIndex = Clipboard.GetNoteCount() - 1;
        }
        void OnNoteDeleteClick() {
            Clipboard.DeleteNote(_noteIndex);
        }
        void OnNoteContentUpdate(string text) {
            Clipboard.WriteNote(_noteIndex, text);
        }
    }
}
#endif