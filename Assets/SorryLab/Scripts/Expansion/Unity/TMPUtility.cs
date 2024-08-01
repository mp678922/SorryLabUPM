using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SorryLab.Expansion {
    static public class TMPUtility {
        static public int GetIntValue(this TMP_InputField inputField) {
            int.TryParse(inputField.text, out int i);
            return i;
        }
        static public float GetFloatValue(this TMP_InputField inputField) {
            float.TryParse(inputField.text, out float i);
            return i;
        }
    }
}
