using Newtonsoft.Json;
using UnityEngine;

namespace SorryLab {
    public struct SVector2 {
        public float x;
        public float y;
        public static implicit operator Vector2(SVector2 obj) {
            return obj.vector2;
        }
        public static implicit operator Vector3(SVector2 obj) {
            return new Vector3(obj.x, obj.y, 0f);
        }
        [JsonIgnore]
        public Vector2 vector2 => new Vector2(x, y);
    }
}
