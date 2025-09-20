using TMPro;
using UnityEngine;
namespace SorryLab {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TMP_Calculator : MonoBehaviour {
        static TMP_Calculator _instance;
        static TMP_Text _tmpText;
        void Awake() {
            if (_instance == null) { Init(this); }
        }
        static void Init(TMP_Calculator instance = null) {
            if (instance != null) {
                _instance = instance;
                _tmpText = instance.GetComponent<TMP_Text>();
            } else if (_instance == null) {
                _instance = FindFirstObjectByType<TMP_Calculator>(FindObjectsInactive.Include);
                if (_instance != null) {
                    Init(_instance);
                } else {
                    Debug.LogWarning("還沒設置好TMP_Calculator，至少要實例一個在場景上。");
                }
            }
        }
        static public float GetWidth(float size, string text) {
            Init();
            _tmpText.fontSize = size;
            _tmpText.text = text;
            return _tmpText.GetPreferredValues(text).x;
        }
        static public float GetWidth(TMP_Text tmpText, string text) {
            Init();
            _tmpText.font = tmpText.font;
            _tmpText.fontSize = tmpText.fontSize;
            _tmpText.text = text;
            return _tmpText.GetPreferredValues(text).x;
        }
        static public string WrapTextByWidth(float size, string text, float maxWidth) {
            Init();
            string outText = "";
            for (int i = 0; i < text.Length; i++) {
                char next = text[i];
                if (GetWidth(size, outText + next) > maxWidth) {
                    outText += '\n';
                }
                outText += next;
            }
            return outText;
        }
        static public string WrapTextByWidth(TMP_Text tmpText, string text, float maxWidth) {
            Init();
            string result = "";
            for (int i = 0; i < text.Length; i++) {
                char next = text[i];
                if (GetWidth(tmpText, result + next) > maxWidth) {
                    result += '\n';
                }
                result += next;
            }
            return result;
        }
    }
}