using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SorryLab.Expansion {
    static public class ComponentExpansion {
        static public RectTransform GetRectTransform(this Component self) { return self.GetComponent<RectTransform>(); }
        public static T GetOrAddComponent<T>(this GameObject self) where T : Component {
            return self.GetComponent<T>() ?? self.AddComponent<T>();
        }

    }
}