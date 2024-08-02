using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SorryLab.Expansion {
    static public class ComponentExpansion {
        static public RectTransform GetRectTransform(this Component self) { return self.GetComponent<RectTransform>(); }

    }
}