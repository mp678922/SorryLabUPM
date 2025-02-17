using System;
using Newtonsoft.Json;
using UnityEngine;
namespace SorryLab {
    [Serializable]
    public struct SColor {
        public float r;
        public float g;
        public float b;
        public float a;

        public static implicit operator Color(SColor obj) {
            return obj.color;
        }
        public static implicit operator SColor(Color obj) {
            return new SColor { r = obj.r, g = obj.g, b = obj.b, a = obj.a };
        }

        [JsonIgnore]
        public Color color => new Color(r, g, b, a);
    }
}
