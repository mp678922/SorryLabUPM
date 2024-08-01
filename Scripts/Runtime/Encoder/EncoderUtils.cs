using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SorryLab.Encoder {
    public static class EncoderUtils {
        static private System.Text.RegularExpressions.Regex NumCheck = new System.Text.RegularExpressions.Regex("^[0-9]*$");
        //重碼壓縮，壓縮原字串必須符合沒有數字[0-9]的規則
        //("aaaa","a")->"a4"
        static public string DuplicateZip(string data, char zipChar) {
            string Data = "";
            int Current = 0;
            for (int i = 0; i < data.Length; i++) {
                if (data[i] == zipChar) {
                    Data += data.Substring(Current, i - Current);
                    int num = 0;
                    for (int j = 0; j <= 8; num++, j++) {
                        if (i + j >= data.Length || data[i + j] != zipChar) { break; }
                    }
                    i += num - 1;
                    Current = i + 1;
                    Data += "" + zipChar + num;
                }
            }
            Data += data.Substring(Current);
            return Data;
        }
        //("a4","a")->"aaaa"
        static public string DuplicateUnzip(string data, char zipChar) {
            string Data = "";
            int Current = 0;
            for (int i = 0; i < data.Length; i++) {
                if (data[i] == zipChar && i + 1 < data.Length) {
                    string num = data[i + 1] + "";
                    if (NumCheck.IsMatch(num)) {
                        string unzip = "";
                        for (int j = 0; j < System.Int32.Parse(num); j++) { unzip += zipChar; }
                        Data += data.Substring(Current, i - Current) + unzip;
                        Current = i + 2;
                        i++;
                    }
                }
            }
            Data += data.Substring(Current);
            return Data;
        }
    }
}