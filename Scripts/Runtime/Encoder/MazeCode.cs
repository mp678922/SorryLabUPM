using UnityEngine;
using System.Collections.Generic;
namespace SorryLab.Encoder {

    //耍花招用的編碼器，讓別人看不懂無法解析
    //無法驗證解析成敗
    //加解密需要鑰匙
    public class MazeCode {

        static readonly string B64_1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 /+=";
        static readonly string B64_2 = "KyAp3UJ91=wjvma8rR+ xnHG6hlCq/VZXbSO20gDsoNtfPBudLQ5WE7ki4TeYFIzMc";
        static public string Encode(string data, string pwd) {
            data = B64.Encode(data);
            char[] Out = data.ToCharArray();
            Out = E_Table(Out, pwd);
            return new string(Out);
        }
        static public string Decode(string data, string pwd) {
            try {
                char[] Out = data.ToCharArray();
                Out = D_Table(Out, pwd);
                data = B64.Decode(new string(Out));
            } catch {
                data = "";
            }
            return data;
        }
        static char[] E_Table(char[] s, string pwd) {
            int Hash = (pwd + "Lock").GetHashCode();
            char[] charArr = new char[s.Length];
            for (int i = 0; i < s.Length; i++) {
                int Lock = (Hash * i) - i;
                int index = IndexClamp(B64_1.IndexOf(s[i]) + Lock);
                charArr[i] = B64_2[index];
            }
            s = charArr;
            return s;
        }
        static char[] D_Table(char[] s, string pwd) {
            int Hash = (pwd + "Lock").GetHashCode();
            char[] charArr = new char[s.Length];
            for (int i = 0; i < s.Length; i++) {
                int Lock = (Hash * i) - i;
                int index = IndexClamp(B64_2.IndexOf(s[i]) - Lock);
                charArr[i] = B64_1[index];
            }
            s = charArr;
            return s;
        }
        static int IndexClamp(int num) {
            num = num % B64_1.Length;
            return num < 0 ? num + B64_1.Length : num;
        }
    }
}