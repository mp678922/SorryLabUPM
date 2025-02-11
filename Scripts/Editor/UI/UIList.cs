using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor.UI {
    public class UIList : UIElement {
        private List<string> _list = new List<string>();
        public UIList(string label, List<string> list) {
            _label = label;
            _list = list;
        }
        protected override void OnDraw() {

        }
    }

}
#endif