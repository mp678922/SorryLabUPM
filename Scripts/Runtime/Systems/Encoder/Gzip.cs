
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

public static class Gzip {
    /// <summary>
    /// 壓縮
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    static public byte[] CompressString(string str) {
        byte[] inputBytes = Encoding.UTF8.GetBytes(str);
        using (var outputStream = new MemoryStream()) {
            using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress)) {
                gzipStream.Write(inputBytes, 0, inputBytes.Length);
            }
            return outputStream.ToArray();
        }
    }
    /// <summary>
    /// 解壓縮
    /// </summary>
    /// <param name="compressedData"></param>
    /// <returns></returns>
    static public string DecompressString(byte[] compressedData) {
        using (var inputStream = new MemoryStream(compressedData))
        using (var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
        using (var outputStream = new MemoryStream()) {
            gzipStream.CopyTo(outputStream);
            byte[] outputBytes = outputStream.ToArray();
            return Encoding.UTF8.GetString(outputBytes);
        }
    }
}
