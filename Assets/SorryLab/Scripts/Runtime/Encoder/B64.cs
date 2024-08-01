namespace SorryLab.Encoder {
    static public class B64 {
        static public string Encode(string t) {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(t));
        }
        static public string Decode(string t) {
            return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(t));
        }
    }
}
