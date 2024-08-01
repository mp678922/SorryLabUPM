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

        static char[] Flip(char[] s) {
            char[] charArr = new char[s.Length];
            for (int i = 0; i < s.Length; i += 2) {
                charArr[i] = s[i + 1];
                charArr[i + 1] = s[i];
            }
            if (s.Length % 2 > 0) { charArr[s.Length - 1] = s[s.Length - 1]; }
            return charArr;
        }

    }
}