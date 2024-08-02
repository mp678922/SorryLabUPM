using UnityEngine;
namespace SorryLab.Expansion {
    static public class NumberExpansion {
        //int
        public static int ToInt(this float number) {
            return (int)number;
        }
        //float
        public static float ToFloat(this int number) {
            return (float)number;
        }
        public static float GetRound(this float value, int roundTo = 0) {
            float m = Mathf.Pow(10f, roundTo);
            return Mathf.Round(value * m) / m;
        }
        //string
        public static float ToFloat(this string text) {
            float.TryParse(text, out float number);
            return number;
        }
        public static float ToInt(this string text) {
            int.TryParse(text, out int number);
            return number;
        }
    }
}
