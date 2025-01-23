using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SorryLab.Expansion {
    static public class ComponentExpansion {
        static public RectTransform GetRectTransform(this Component self) { return self.GetComponent<RectTransform>(); }
        public static T GetOrAddComponent<T>(this Component self) where T : Component {
            return self.GetComponent<T>() ?? self.gameObject.AddComponent<T>();
        }
        public static bool TryGetChildHierarchyPath(this Component self, Component child, out string path) {
            Transform current = child.transform;
            Transform root = self.transform;
            path = current.name;
            while (current.parent != null) {
                if (current.parent == root) { return true; }
                current = current.parent;
                path = $"{current.name}/{path}";
            }
            return false;
        }

    }
}