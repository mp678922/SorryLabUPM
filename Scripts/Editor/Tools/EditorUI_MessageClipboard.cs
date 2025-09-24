#if UNITY_EDITOR
using System.Collections.Generic;
using SorryLab.Editor.UI;
using UnityEditor;
using UnityEngine;
namespace SorryLab.Editor {
    public class EditorUI_MessageClipboard : EditorWindow {
        static EditorUI_MessageClipboard _instance;
        static int _mode = 0;
        static int _systemIndex = 0;
        static int _noteIndex = 0;
        [MenuItem("{SorryLab}/Tools/MessageClipboard")]
        public static void ShowWindow() {
            if (_instance == null) {
                _instance = GetWindow<EditorUI_MessageClipboard>("MessageClipboard");
            }
        }
        public static void ShowNewClipboardInfo() {
            ShowWindow();
            _mode = 0;
            _systemIndex = MessageClipboard.GetSystemCount() - 1;
            _instance.Focus();
        }
        private List<string> ls = new List<string>();
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
            _systemIndex = Mathf.Clamp(_systemIndex, 0, MessageClipboard.GetSystemCount() - 1);
            if (MessageClipboard.GetSystemCount() == 0) { _systemIndex = -1; }

            Layout.TextArea(MessageClipboard.Get(_systemIndex))
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
                Layout.Label($"({_systemIndex + 1}/{MessageClipboard.GetSystemCount()})")
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
                .SetEnable(_systemIndex < MessageClipboard.GetSystemCount() - 1)
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
            MessageClipboard.AppendNote(MessageClipboard.Get(_systemIndex));
            MessageClipboard.Delete(_systemIndex);
        }
        void OnSystemDeleteClick() {
            MessageClipboard.Delete(_systemIndex);
        }

        void DrawNote() {
            float windowHeight = position.height;
            _noteIndex = Mathf.Clamp(_noteIndex, 0, MessageClipboard.GetNoteCount() - 1);
            if (MessageClipboard.GetNoteCount() == 0) { _noteIndex = -1; }

            Layout.TextArea(MessageClipboard.GetNote(_noteIndex), OnNoteContentUpdate)
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
                Layout.Label($"({_noteIndex + 1}/{MessageClipboard.GetNoteCount()})")
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
                .SetEnable(_noteIndex < MessageClipboard.GetNoteCount() - 1)
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
            MessageClipboard.AppendNote("");
            _noteIndex = MessageClipboard.GetNoteCount() - 1;
        }
        void OnNoteDeleteClick() {
            MessageClipboard.DeleteNote(_noteIndex);
        }
        void OnNoteContentUpdate(string text) {
            MessageClipboard.WriteNote(_noteIndex, text);
        }
    }
}
#endif