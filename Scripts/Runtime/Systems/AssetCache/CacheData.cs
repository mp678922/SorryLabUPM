using UnityEngine;

namespace SorryLab.Cache {
    public class CacheData<T> {
        public T data;
        public int useCount { get; private set; } = 1;
        public int useTime { get; private set; } = 1;
        public int memorySize { get; private set; }
        static public CacheData<T> Create(T data) {
            return new CacheData<T>() { data = data, useTime = (int)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds() };
        }
        public CacheData<T> SetMemory(Texture2D texture2D) {
            memorySize = EstimateMemorySize(texture2D);
            return this;
        }
        public T GetData() {
            useCount++;
            useTime = (int)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            return data;
        }
        static int EstimateMemorySize(Texture2D tex) {
            if (tex == null) return 0;
            int bitsPerPixel = GetBitsPerPixel(tex.format);
            int size = tex.width * tex.height * bitsPerPixel / 8; // bytes
            return size;
        }
        private static int GetBitsPerPixel(TextureFormat format) {
            switch (format) {
                case TextureFormat.RGBA32: return 32;
                case TextureFormat.ARGB32: return 32;
                case TextureFormat.RGB24: return 24;
                case TextureFormat.Alpha8: return 8;
                case TextureFormat.RG16: return 16;
                case TextureFormat.R16: return 16;
                // 壓縮格式（如 DXT, ETC）會更小
                case TextureFormat.DXT1: return 4;  // 0.5 byte/pixel
                case TextureFormat.DXT5: return 8;  // 1 byte/pixel
                case TextureFormat.ETC_RGB4: return 4;
                case TextureFormat.ETC2_RGBA8: return 8;
                default: return 32; // 預設粗估
            }
        }
    }

}
