using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SorryLab.Expansion {
    static class StringExpansion {
        static public string GetRandomSort(this string data, int randomSeed) {
            Random.InitState(randomSeed);
            return data.GetRandomSort();
        }
        static public string GetRandomSort(this string data) {
            string sort = "";
            List<char> surplus = new List<char>(data.ToCharArray());
            while (surplus.Count > 0) {
                int num = Random.Range(0, surplus.Count);
                sort += surplus[num];
                surplus.RemoveAt(num);
            }
            return sort;
        }
    }

}
