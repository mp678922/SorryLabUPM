using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SorryLab.Expansion {
    static public class TMPExpansion {
        static public int GetIntValue(this TMP_InputField inputField) {
            int.TryParse(inputField.text, out int i);
            return i;
        }
        static public float GetFloatValue(this TMP_InputField inputField) {
            float.TryParse(inputField.text, out float i);
            return i;
        }
        static public Button SetText(this Button button, string text) {
            TMP_Text tmp_text = button.GetComponentInChildren<TMP_Text>();
            if (tmp_text != null) { tmp_text.text = text; }
            return button;
        }
        static public Button SetColor(this Button button, Color text, Color background) {
            button.SetBackgroundColor(background);
            button.SetTextColor(text);
            return button;
        }
        static public Button SetBackgroundColor(this Button button, Color color) {
            Image image = button.GetComponent<Image>();
            if (image != null) { image.color = color; }
            return button;
        }
        static public Button SetTextColor(this Button button, Color color) {
            TMP_Text tmp_text = button.GetComponentInChildren<TMP_Text>();
            if (tmp_text != null) { tmp_text.color = color; }
            return button;
        }
    }
}
