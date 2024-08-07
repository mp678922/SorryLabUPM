using System.Collections.Generic;
using UnityEngine;
namespace SorryLab {
    static public class StringUtils {

        static public readonly string EnglishStr = "abcdefghijklmnopqrstubwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static public readonly string NumStr = "0123456789";

        static public string RandomCode(string CodeList, int length) {
            string result = "";
            for (int i = 0; i < length; i++) { result += CodeList[Random.Range(0, CodeList.Length)]; }
            return result;
        }

        static public string RandomCode(string CodeList, int length, int seed) {
            Random.InitState(seed);
            string data = RandomCode(CodeList, length);
            return data;
        }

        static public char[] Flip(char[] s) {
            char[] charArr = new char[s.Length];
            for (int i = 0; i < s.Length; i += 2) {
                charArr[i] = s[i + 1];
                charArr[i + 1] = s[i];
            }
            if (s.Length % 2 > 0) { charArr[s.Length - 1] = s[s.Length - 1]; }
            return charArr;
        }
        static public string CapitalizeFirstLetter(string input) {
            if (string.IsNullOrEmpty(input)) { return input; }
            if (input.Length == 1) { return input.ToUpper(); }
            return char.ToUpper(input[0]) + input.Substring(1);
        }
        static public string RichTextColorRGB(string text, Color color) {
            string htmlColor = ColorUtility.ToHtmlStringRGB(color);
            return $"<color=#{htmlColor}>{text}</color>";
        }
        static public string RichTextColorRGBA(string text, Color color) {
            string htmlColor = ColorUtility.ToHtmlStringRGBA(color);
            return $"<color=#{htmlColor}>{text}</color>";
        }
    }
}