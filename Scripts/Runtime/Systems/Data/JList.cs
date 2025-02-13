using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace SorryLab {
    /// <summary>
    /// 這工具是讓內容被JsonUtility洗成字串時可保留繼承類別的格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class JList<T> : IEnumerable<T>, IList<T> where T : class {
        public List<TypeJson> contents = new List<TypeJson>();
        public T this[int index] {
            get { return contents[index].GetObject(); }
            set { contents[index] = new TypeJson(value); }
        }
        public void Add(T obj) { contents.Add(new TypeJson(obj)); }
        public void AddRange(IEnumerable<T> values) {
            for (int i = 0; i < values.Count(); i++) {
                contents.Add(new TypeJson(values.ElementAt(i)));
            }
        }
        public int IndexOf(T obj) { return ToList().IndexOf(obj); }
        public void Insert(int index, T obj) { contents.Insert(index, new TypeJson(obj)); }
        public void Clear() { contents.Clear(); }
        public bool Contains(T obj) { return ToList().Contains(obj); }
        public void CopyTo(T[] objs, int arrayIndex) {
            List<TypeJson> ls = new List<TypeJson>();
            for (int i = 0; i < objs.Length; i++) { ls.Add(new TypeJson(objs[i])); }
            contents.CopyTo(ls.ToArray(), arrayIndex);
        }
        public bool Remove(T obj) {
            for (int i = 0; i < contents.Count; i++) {
                if (contents[i].GetObject() == obj) {
                    contents.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public void RemoveAt(int index) { contents.RemoveAt(index); }
        public void Update() {
            for (int i = 0; i < contents.Count; i++) { contents[i].Update(); }
        }
        public List<T> ToList() {
            List<T> list = new List<T>();
            for (int i = 0; i < contents.Count; i++) {
                list.Add(contents[i].GetObject());
            }
            return list;
        }

        bool ICollection<T>.IsReadOnly => throw new NotImplementedException();
        public int Count => contents.Count;

        public IEnumerator<T> GetEnumerator() {
            return ToList().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        [Serializable]
        public class TypeJson {
            public string type;
            public string json;
            private T obj;
            public TypeJson(T obj) {
                this.obj = obj;
                type = obj.GetType().FullName;
                Update();
            }
            public T GetObject() {
                if (obj == null) {
                    obj = ToObject();
                }
                return obj;
            }
            public void Update() {
                json = JsonUtility.ToJson(obj);
            }
            private T ToObject() {
                if (JListUtility.onGetType == null) { Debug.LogError("[JListUtility.onGetType]沒設定過。"); }
                try {
                    return (T)JsonUtility.FromJson(json, JListUtility.onGetType(type));
                } catch {
                    Debug.LogWarning($"[JList]JList<{typeof(T).Name}>之中遇到反序列化失敗，該物件紀錄之類別為「{type}」。");
                    return default;
                }
            }
        }

    }
}