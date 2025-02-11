using System;
using System.Collections.Generic;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
namespace SorryLab.Editor.UI {
    public class UIScrollView : UIElement {
        private Vector2 _position = Vector2.zero;
        private Action _content;
        private Action<Vector2> _onPositionChanged;
        public UIScrollView(Vector2 position, Action<Vector2> onPositionChanged = null, Action content = null) {
            _content = content;
            _onPositionChanged = onPositionChanged;
            _position = position;
        }
        protected override void OnDraw() {
            List<GUILayoutOption> options = new List<GUILayoutOption>();
            Vector2 position = EditorGUILayout.BeginScrollView(_position, _options.ToArray());
            if (position != _position) { _onPositionChanged?.Invoke(position); }
            _content?.Invoke();
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif