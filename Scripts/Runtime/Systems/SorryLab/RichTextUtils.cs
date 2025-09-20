using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SorryLab {
    public static class RichTextUtils {
        /// <summary>
        /// 顏色RGB
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        static public string ColorRGB(string text, Color color) {
            string htmlColor = ColorUtility.ToHtmlStringRGB(color);
            return $"<color=#{htmlColor}>{text}</color>";
        }
        /// <summary>
        /// 顏色RGBA
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        static public string ColorRGBA(string text, Color color) {
            string htmlColor = ColorUtility.ToHtmlStringRGBA(color);
            return $"<color=#{htmlColor}>{text}</color>";
        }
        /// <summary>
        /// 粗體
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static public string Boldface(string text) { return $"<b>{text}</b>"; }
        /// <summary>
        /// 斜體
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static public string Italics(string text) { return $"<b>{text}</b>"; }
        /// <summary>
        /// 字級
        /// </summary>
        /// <param name="text"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        static public string Size(string text, int size) { return $"<size={size}>{text}</size>"; }
        /// <summary>
        /// 連結
        /// </summary>
        /// <param name="text"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        static public string Link(string text, string action) { return $"<link={action}>{text}</link>"; }
    }
}
