using System.Collections.Generic;
using System;
namespace SorryLab.Expansion {
    static public class ListExpansion {
        public static T Dequeue<T>(this List<T> list) {
            if (list.Count == 0) { return default; }
            T first = list[0];
            list.RemoveAt(0);
            return first;
        }
        public static T Pop<T>(this List<T> list) {
            if (list.Count == 0) { return default; }
            T final = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return final;
        }
        public static void RandomSort<T>(this List<T> list) {
            Queue<T> temp = new Queue<T>();
            while (list.Count > 0) {
                int index = UnityEngine.Random.Range(0, list.Count);
                temp.Enqueue(list[index]);
                list.RemoveAt(index);
            }
            while (temp.Count > 0) {
                list.Add(temp.Dequeue());
            }
        }
        public static T GetRandomElement<T>(this List<T> list) {
            return list[SystemRandom().Next(list.Count)];
        }
        public static T GetRandomElementAndRemove<T>(this List<T> list) {
            int index = SystemRandom().Next(list.Count);
            T obj = list[index];
            list.RemoveAt(index);
            return obj;
        }
        static private Random SystemRandom() {
            long seed = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            return new Random((int)(seed & 0x00000000FFFFFFFF));
        }
    }
}

