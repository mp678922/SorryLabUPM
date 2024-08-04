using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
namespace SorryLab.Editor {
    public class CustomMenu {
        List<Item> options = new List<Item>();
        public void AddDivider() {
            options.Add(new Item { isDivider = true });
        }
        public void AddItem(string text, Action action, bool isEnable = true) {
            options.Add(new Item { text = text, action = action, isEnable = isEnable });
        }
        public void Show() {
            GenericMenu menu = new GenericMenu();
            for (int i = 0; i < options.Count; i++) {
                Item item = options[i];
                if (item.isDivider) {
                    menu.AddSeparator("");
                } else {
                    if (item.isEnable) {
                        menu.AddItem(new GUIContent(item.text), false, (o) => { item.action.Invoke(); }, item.text);
                    } else {
                        menu.AddDisabledItem(new GUIContent(item.text));
                    }
                }
            }
            menu.ShowAsContext();
        }
        class Item {
            public string text;
            public Action action;
            public bool isEnable;
            public bool isDivider = false;
        }
    }
}
#endif