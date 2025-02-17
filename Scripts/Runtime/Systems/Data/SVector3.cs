using Newtonsoft.Json;
using UnityEngine;

namespace SorryLab {
    public struct SVector3 {
        public float x;
        public float y;
        public float z;
        public static implicit operator Vector2(SVector3 obj) {
            return new Vector2(obj.x, obj.y);
        }
        public static implicit operator Vector3(SVector3 obj) {
            return obj.vector3;
        }
        [JsonIgnore]
        public Vector3 vector3 => new Vector3(x, y, z);
    }
}
