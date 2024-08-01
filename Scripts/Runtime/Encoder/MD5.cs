using System.Text;
namespace SorryLab.Encoder {
    //不可逆編碼
    public class MD5 {
        static public string Encode(string code) {
            using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create()) {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(code));
                StringBuilder Builder = new StringBuilder();
                for (int i = 0; i < data.Length; i++) { Builder.Append(data[i].ToString("x2")); }
                return Builder.ToString();
            }
        }
    }
}