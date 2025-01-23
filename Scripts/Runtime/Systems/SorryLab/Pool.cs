using System.Collections.Generic;
using UnityEngine;
namespace SorryLab {
    public class Pool<T> where T : Component {
        T _object;
        Transform _returnParent;
        Queue<T> queue = new();
        public Pool(T obj, int defaultInstance = 0) {
            _object = obj;
            for (int i = 0; i < defaultInstance; i++) {
                T newObj = Object.Instantiate<T>(_object);
                newObj.transform.SetParent(_object.transform.parent);
                newObj.gameObject.SetActive(false);
                queue.Enqueue(newObj);
            }
        }
        public Pool<T> SetReturnParent(Transform folder) {
            _returnParent = folder;
            return this;
        }
        public T Create(Transform parent = null) {
            T obj = null;
            if (obj == null) {
                obj = queue.Count == 0 ? Object.Instantiate<T>(_object) : queue.Dequeue();
            }
            obj.transform.SetParent(parent == null ? _object.transform.parent : parent);
            if (obj.gameObject.TryGetComponent<RectTransform>(out RectTransform rect)) {
                Vector3 pos = rect.anchoredPosition3D;
                pos.z = 0f;
                obj.GetComponent<RectTransform>().anchoredPosition3D = pos;
            } else {
                obj.transform.position = _object.transform.position;
            }
            obj.transform.localScale = _object.transform.localScale;
            obj.transform.SetAsLastSibling();
            obj.gameObject.SetActive(true);
            return obj;
        }
        public T Return(T obj) {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(GetFolder());
            queue.Enqueue(obj);
            return obj;
        }
        public void Release() {
            while (queue.Count > 0) {
                Object.Destroy(queue.Dequeue());
            }
        }
        private Transform GetFolder() {
            if (_returnParent != null) { return _returnParent; }
            return _object.transform.parent;
        }
    }
}