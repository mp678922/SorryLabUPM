using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
namespace SorryLab {
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : IEnumerable, IEnumerable<KeyValuePair<TKey, TValue>> {
        public List<TKey> keys = new List<TKey>();
        public List<TValue> values = new List<TValue>();
        public TValue this[TKey key] {
            get {
                if (keys.Contains(key)) {
                    int index = keys.IndexOf(key);
                    return values[index];
                }
                return default;
            }
            set {
                Add(key, value);
            }
        }
        public int Count { get { return keys.Count; } }
        public Dictionary<TKey, TValue> ToDictionary() {
            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
            for (int i = 0; i < Count; i++) {
                dic[keys[i]] = values[i];
            }
            return dic;
        }
        public void Add(TKey key, TValue value) {
            if (keys.Contains(key)) {
                values[keys.IndexOf(key)] = value;
            } else {
                keys.Add(key);
                values.Add(value);
            }
        }
        public bool ContainsKey(TKey key) {
            return keys.Contains(key);
        }
        public bool ContainsValue(TValue value) {
            return values.Contains(value);
        }
        public bool Remove(TKey key) {
            if (keys.Contains(key)) {
                int index = keys.IndexOf(key);
                keys.RemoveAt(index);
                values.RemoveAt(index);
                return true;
            }
            return false;
        }
        public void Clear() {
            keys.Clear();
            values.Clear();
        }
        public IEnumerable<TKey> Keys { get { return keys; } }
        public IEnumerable<TValue> Values { get { return values; } }
        public KeyValuePair<TKey, TValue> Get(int index) {
            return new KeyValuePair<TKey, TValue>(keys[index], values[index]);
        }
        public IEnumerator GetEnumerator() { return keys.GetEnumerator(); }
        public string ToJsonString() { return JsonUtility.ToJson(this); }
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() {
            List<KeyValuePair<TKey, TValue>> list = new List<KeyValuePair<TKey, TValue>>();
            for (int i = 0; i < keys.Count; i++) { list.Add(new KeyValuePair<TKey, TValue>(keys[i], values[i])); }
            return list.GetEnumerator();
        }
    }
}
