using Newtonsoft.Json;
using UnityEngine;

namespace SorryLab {
    public struct SColor {
        public float r;
        public float g;
        public float b;
        public float a;

        public static implicit operator Color(SColor obj) {
            return obj.color;
        }
        public static implicit operator SColor(Color obj) {
            return new Color(obj.r, obj.g, obj.b, obj.a);
        }

        [JsonIgnore]
        public Color color => new Color(r, g, b, a);
    }
}
