using System;
using Newtonsoft.Json;
using UnityEngine;
namespace SorryLab {
    [Serializable]
    public struct SVector3 {
        public float x;
        public float y;
        public float z;

        public static implicit operator Vector2(SVector3 obj) {
            return new Vector2(obj.x, obj.y);
        }
        public static implicit operator SVector3(Vector2 obj) {
            return new Vector3 { x = obj.x, y = obj.y, z = 0f };
        }


        public static implicit operator Vector3(SVector3 obj) {
            return new Vector3(obj.x, obj.y, 0f);
        }
        public static implicit operator SVector3(Vector3 obj) {
            return new SVector3 { x = obj.x, y = obj.y, z = obj.z };
        }

        [JsonIgnore]
        public Vector3 vector3 => new Vector3(x, y, z);
    }
}
