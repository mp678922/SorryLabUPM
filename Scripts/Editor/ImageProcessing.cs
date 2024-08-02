using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

# if UNITY_EDITOR
/// <summary>
/// 圖像處理
/// </summary>
namespace SorryLab {
    public class ImageProcessing {

        /// <summary>
        /// 深階去背：以RGB/3為A去背值
        /// </summary>
        [MenuItem("{SorryLab}/Utility/Image Processing/Bright To Alpha", false)]
        static void BrightToAlpha() {
            Processing((color) => {
                float setAlpha = Mathf.Min((color.r + color.g + color.b) / 3f, color.a);
                return new Color(1f, 1f, 1f, setAlpha);
            }, "_a", "BrightToAlpha");
        }

        /// <summary>
        /// 灰階轉換：把圖片變成灰階圖
        /// </summary>
        [MenuItem("{SorryLab}/Utility/Image Processing/Desaturation", false)]
        static void Desaturation() {
            Processing((color) => {
                float rgb = (color.r + color.g + color.b) / 3f;
                return new Color(rgb, rgb, rgb, color.a);
            }, "_d", "Desaturation");
        }

        static void Processing(System.Func<Color, Color> colorProcessing, string additionalName, string newFolder = "") {

            List<string> NewFiles = new List<string>();
            Object[] selectObjects = Selection.objects;
            List<Object> selectNewObjects = new List<Object>();

            for (int i = 0; i < selectObjects.Length; i++) {
                if (selectObjects[i].GetType() == typeof(Texture2D)) {

                    Texture2D texture = (Texture2D)selectObjects[i];
                    Texture2D newTexture = new Texture2D(texture.width, texture.height);

                    string folder = newFolder.Length == 0 ? "/" : "/" + newFolder + "/";
                    string path = AssetDatabase.GetAssetPath(selectObjects[i]);
                    string fileName = System.IO.Path.GetDirectoryName(path) + folder + System.IO.Path.GetFileNameWithoutExtension(path) + additionalName + System.IO.Path.GetExtension(path);
                    folder = System.IO.Path.GetDirectoryName(fileName);

                    TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(path);

                    bool defaultReadable = textureImporter.isReadable;

                    textureImporter.isReadable = true;
                    textureImporter.SaveAndReimport();

                    for (int x = 0; x < texture.width; x++) {
                        for (int y = 0; y < texture.height; y++) {
                            Color color = colorProcessing(texture.GetPixel(x, y));
                            newTexture.SetPixel(x, y, color);
                        }
                    }

                    textureImporter.isReadable = defaultReadable;
                    textureImporter.SaveAndReimport();

                    if (!System.IO.Directory.Exists(folder)) { System.IO.Directory.CreateDirectory(folder); }
                    System.IO.File.WriteAllBytes(fileName, newTexture.EncodeToPNG());
                    NewFiles.Add(fileName);
                    Debug.Log("New texture [" + fileName + "] is Created.");

                }
            }

            AssetDatabase.Refresh();
            foreach (var i in NewFiles) {
                selectNewObjects.Add(AssetDatabase.LoadAssetAtPath<Texture2D>(i));
            }
            Selection.objects = selectNewObjects.ToArray();

        }
    }
}
#endif