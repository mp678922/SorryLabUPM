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
    public class JList<T> : IEnumerable<T> where T : class {
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
        public void Remove(T obj) {
            for (int i = 0; i < contents.Count; i++) {
                if (contents[i].GetObject() == obj) {
                    contents.RemoveAt(i);
                    break;
                }
            }
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
                return (T)JsonUtility.FromJson(json, JListUtility.onGetType(type));
            }
        }

    }
}