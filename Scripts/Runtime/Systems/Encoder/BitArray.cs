using System.Collections.Generic;
namespace SorryLab.Encoder {
    public class BitArray {
        Dictionary<int, bool> content = new Dictionary<int, bool>();
        int max = int.MinValue, min = int.MaxValue;
        public string data {
            get {
                if (max == int.MinValue || min == int.MaxValue) { return ""; }
                string Out = "";
                int start = -1, end = -1;
                for (var i = min; i <= max + 1; i++) {
                    if (content.ContainsKey(i)) {
                        if (start == -1) { start = i; }
                        end = i;
                    } else {
                        if (start != -1) {
                            if (start == end) { Out += start; } else { Out += start + "~" + end; }
                            if (i != max + 1) { Out += ","; }
                            start = end = -1;
                        }
                    }
                }
                return Out;
            }
        }
        public System.Collections.IEnumerator GetEnumerator() { return content.Keys.GetEnumerator(); }
        public BitArray() { }
        public BitArray(params int[] data) { foreach (var i in data) { this[i] = true; } }
        public BitArray(string data) {
            foreach (var i in data.Split(","[0])) {
                if (i.Contains("~")) {
                    string[] splitData = i.Split("~"[0]);
                    int num1, num2;
                    if (splitData.Length == 2) {
                        if (int.TryParse(splitData[0], out num1) && int.TryParse(splitData[1], out num2)) {
                            for (int j = num1; j <= num2; j++) { this[j] = true; }
                        }
                    }
                } else {
                    int num;
                    if (int.TryParse(i, out num)) { this[num] = true; }
                }
            }
        }
        public bool this[int key] {
            set {
                if (value) {
                    if (key > max) max = key;
                    if (key < min) min = key;
                    content[key] = value;
                } else content.Remove(key);
            }
            get {
                if (content.ContainsKey(key)) return content[key];
                else return false;
            }
        }
        public void Remove(int item) {
            content.Remove(item);
            if (item == max) {
                max = 0;
                foreach (var i in content.Keys) { if (i > max) max = i; }
            }
            if (item == min) {
                min = 0;
                foreach (var i in content.Keys) { if (i < min) max = i; }
            }
        }
        public static implicit operator string(BitArray d) { return d.data; }
        public int[] ToArray() {
            List<int> data = new List<int>();
            foreach (var i in content.Keys) { data.Add(i); }
            return data.ToArray();
        }
        public bool Contains(int key) {
            return content.ContainsKey(key);
        }
        public static string IntToString(params int[] values) {
            return (new BitArray(values)).data;
        }
        public static int[] StringToInt(string values) {
            return (new BitArray(values)).ToArray();
        }
    }
}