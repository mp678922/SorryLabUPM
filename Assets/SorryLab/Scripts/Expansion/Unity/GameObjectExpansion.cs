using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SorryLab {
    static class GameObjectExpansion {

        public static T GetOrAddComponent<T>(this GameObject self) where T : Component {
            return self.GetComponent<T>() ?? self.AddComponent<T>();
        }
        public static GameObject[] GetChildren(this GameObject self, bool includeInactive = false) {
            return self
                .GetComponentsInChildren<Transform>(includeInactive)
                .Where(c => c != self.transform)
                .Select(c => c.gameObject)
                .ToArray()
            ;
        }
        public static void RemoveComponent<T>(this GameObject self) where T : Component {
            GameObject.Destroy(self.GetComponent<T>());
        }
        public static GameObject Find(this GameObject self, string name, bool includeInactive = false) {
            var children = self.GetComponentsInChildren<Transform>(includeInactive);
            foreach (var transform in children) {
                if (transform.name == name) {
                    return transform.gameObject;
                }
            }
            return null;
        }

    }
}
