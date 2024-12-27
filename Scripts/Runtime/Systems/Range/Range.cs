using UnityEngine;

namespace SorryLab.Range {
    [System.Serializable]
    public class IntRange {
        public int min;
        public int max;
        public IntRange() { }
        public IntRange(int min, int max) { this.min = min; this.max = max; }
        public int GetRandom() { return Random.Range(min, max); }
        public int GetAverage() { return Mathf.RoundToInt(Mathf.Lerp(min, max, .5f)); }
        public int Evaluate(float time) {
            time = Mathf.Clamp(0f, 1f, time);
            return (int)Mathf.Lerp(min, max, time);
        }
        public static implicit operator int(IntRange from) { return from.GetRandom(); }
    }

    [System.Serializable]
    public class FloatRange {
        public float min;
        public float max;
        public FloatRange() { }
        public FloatRange(float min, float max) { this.min = min; this.max = max; }
        public float GetRandom() { return Random.Range(min, max); }
        public float GetAverage() { return Mathf.Lerp(min, max, .5f); }
        public float Evaluate(float time) {
            time = Mathf.Clamp(0f, 1f, time);
            return Mathf.Lerp(min, max, time);
        }
        public static implicit operator float(FloatRange from) { return from.GetRandom(); }
    }

    [System.Serializable]
    public class Vector2Range {
        public Vector2 min;
        public Vector2 max;
        public Vector2Range() { }
        public Vector2Range(Vector2 min, Vector2 max) { this.min = min; this.max = max; }
        public Vector2 GetRandom() { return Vector2.Lerp(min, max, Random.value); }
        public Vector2 GetAverage() { return Vector2.Lerp(min, max, .5f); }
        public Vector2 Evaluate(float time) {
            time = Mathf.Clamp(0f, 1f, time);
            return Vector2.Lerp(min, max, time);
        }
        public static implicit operator Vector2(Vector2Range from) { return from.GetRandom(); }
    }

    [System.Serializable]
    public class Vector3Range {
        public Vector3 min;
        public Vector3 max;
        public Vector3Range() { }
        public Vector3Range(Vector3 min, Vector3 max) { this.min = min; this.max = max; }
        public Vector3 GetRandom() { return Vector3.Lerp(min, max, Random.value); }
        public Vector3 GetAverage() { return Vector3.Lerp(min, max, .5f); }
        public Vector3 Evaluate(float time) {
            time = Mathf.Clamp(0f, 1f, time);
            return Vector3.Lerp(min, max, time);
        }
        public static implicit operator Vector3(Vector3Range from) { return from.GetRandom(); }
    }

    [System.Serializable]
    public class Vector4Range {
        public Vector4 min;
        public Vector4 max;
        public Vector4Range() { }
        public Vector4Range(Vector4 min, Vector4 max) { this.min = min; this.max = max; }
        public Vector4 GetRandom() { return Vector4.Lerp(min, max, Random.value); }
        public Vector4 GetAverage() { return Vector4.Lerp(min, max, .5f); }
        public Vector4 Evaluate(float time) {
            time = Mathf.Clamp(0f, 1f, time);
            return Vector4.Lerp(min, max, time);
        }
        public static implicit operator Vector4(Vector4Range from) { return from.GetRandom(); }
    }

    [System.Serializable]
    public class ColorRange {
        public Color min = Color.black;
        public Color max = Color.white;
        public ColorRange() { }
        public ColorRange(Color min, Color max) { this.min = min; this.max = max; }
        public Color GetRandom() { return Color.Lerp(min, max, Random.value); }
        public Color GetAverage() { return Color.Lerp(min, max, .5f); }
        public Color Evaluate(float time) {
            time = Mathf.Clamp(0f, 1f, time);
            return Color.Lerp(min, max, time);
        }
        public static implicit operator Color(ColorRange from) { return from.GetRandom(); }
    }

    [System.Serializable]
    public class Color32Range {
        public Color32 min = Color.black;
        public Color32 max = Color.white;
        public Color32Range() { }
        public Color32Range(Color32 min, Color32 max) { this.min = min; this.max = max; }
        public Color32 GetRandom() { return Color.Lerp(min, max, Random.value); }
        public Color32 GetAverage() { return Color.Lerp(min, max, .5f); }
        public Color32 Evaluate(float time) {
            time = Mathf.Clamp(0f, 1f, time);
            return Color32.Lerp(min, max, time);
        }
        public static implicit operator Color32(Color32Range from) { return from.GetRandom(); }
    }

    [System.Serializable]
    public class QuaternionRange {
        public Quaternion min;
        public Quaternion max;
        public QuaternionRange() { }
        public QuaternionRange(Quaternion min, Quaternion max) { this.min = min; this.max = max; }
        public Vector3 minEular {
            get { return min.eulerAngles; }
            set { min = Quaternion.Euler(value); }
        }
        public Vector3 maxEular {
            get { return max.eulerAngles; }
            set { max = Quaternion.Euler(value); }
        }
        public Quaternion GetRandom() { return Quaternion.Lerp(min, max, Random.value); }
        public Quaternion GetAverage() { return Quaternion.Lerp(min, max, .5f); }
        public Quaternion Evaluate(float time) {
            time = Mathf.Clamp(0f, 1f, time);
            return Quaternion.Lerp(min, max, time);
        }
        public static implicit operator Quaternion(QuaternionRange from) { return from.GetRandom(); }
    }

    [System.Serializable]
    public class Texture2DRange {
        public Texture2D[] textures;
        public Texture2D GetRandom() { return textures[Random.Range(0, textures.Length - 1)]; }
        public Texture2D GetAverage() { return textures[Mathf.RoundToInt(textures.Length * .5f)]; }
        public Texture2D Evaluate(float time) {
            time = Mathf.Clamp(0f, 1f, time);
            return textures[Mathf.RoundToInt(textures.Length * time)];
        }
        public static implicit operator Texture2D(Texture2DRange from) { return from.GetRandom(); }
    }

    [System.Serializable]
    public class SpriteRange {
        public Sprite[] sprites;
        public Sprite GetRandom() { return sprites[Random.Range(0, sprites.Length - 1)]; }
        public Sprite GetAverage() { return sprites[Mathf.RoundToInt(sprites.Length * .5f)]; }
        public Sprite Evaluate(float time) {
            time = Mathf.Clamp(0f, 1f, time);
            return sprites[Mathf.RoundToInt(sprites.Length * time)];
        }
        public static implicit operator Sprite(SpriteRange from) { return from.GetRandom(); }
    }

}