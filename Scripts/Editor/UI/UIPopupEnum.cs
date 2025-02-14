#if UNITY_EDITOR
using System;
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIPopupEnum<T> : UIElement where T : Enum {
        Action<int> _onIndexChanged;
        string[] enumNames;
        int _index;
        public UIPopupEnum(string label, int index, Action<int> onIndexChange = null) {
            _index = index;
            _onIndexChanged = onIndexChange;
            _label = label;
            enumNames = Enum.GetNames(typeof(T));
        }
        protected override void OnDraw() {
            int index = EditorGUILayout.Popup(_label, _index, enumNames);
            if (index != _index) { _onIndexChanged?.Invoke(index); }
        }
        public UIPopupEnum<T> OnIndexChanged(Action<int> action) {
            _onIndexChanged = action;
            return this;
        }
    }
}
#endif