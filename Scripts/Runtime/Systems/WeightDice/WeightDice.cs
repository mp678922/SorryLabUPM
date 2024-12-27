using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SorryLab {
    public class WeightDice<T> where T : class {
        Dictionary<T, float> m_objects = new Dictionary<T, float>();
        float m_max = 0f;
        public int Count { get { return m_objects.Count; } }
        public WeightDice() { }
        public WeightDice(IEnumerable<IWeightDice<T>> list) { Add(list); }
        public void Add(IWeightDice<T> obj) {
            Add(obj.GetValue(), obj.GetWeight());
        }
        public void Add(IEnumerable<IWeightDice<T>> list) {
            foreach (IWeightDice<T> i in list) { Add(i); }
        }
        public void Add(T obj, float weight) {
            if (weight == 0f) { return; }
            m_objects[obj] = weight;
            m_max += weight;
        }
        public void Remove(T obj) {
            m_max -= m_objects[obj];
            m_objects.Remove(obj);
        }
        public bool TryCast(out T result) {
            result = Cast();
            return m_objects.Count > 0;
        }
        public T Cast() {
            float target = Random.value * m_max;
            float now = 0f;
            foreach (T i in m_objects.Keys) {
                now += m_objects[i];
                if (target <= now) { return i; }
            }
            return null;
        }
        public T CastAndRemove() {
            T v = Cast();
            if (v != null) { Remove(v); }
            return v;
        }
    }
}